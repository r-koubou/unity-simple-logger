/* =========================================================================

    SimpleLoggerRepository.cs
    Copyright(c) R-Koubou

============================================================================ */


using System.Collections.Generic;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// An implementation example of <see cref="ISimpleLoggerRepository"/>
    /// </summary>
    public class SimpleLoggerRepository<TKey> : ISimpleLoggerRepository<TKey>
    {
        protected object lockObject = new object();
        protected readonly Dictionary<TKey, ISimpleLogger> loggers = new Dictionary<TKey, ISimpleLogger>();

        public int Count => loggers.Count;

        public Dictionary<TKey, ISimpleLogger>.KeyCollection Keys => loggers.Keys;

        public void Dispose()
        {
            lock( lockObject )
            {
                foreach( var logger in loggers.Values )
                {
                    logger.Dispose();
                }
                loggers.Clear();
            }
        }

        public void Add<TLogger>( TKey key, TLogger logger ) where TLogger : ISimpleLogger
        {
            lock( lockObject )
            {
                if( loggers.ContainsKey( key ) )
                {
                    throw new System.ArgumentException( $"{key} has already added");
                }
                loggers.Add( key, logger );
            }
        }

        public void Remove( TKey key )
        {
            lock( lockObject )
            {
                loggers.Remove( key );
            }
        }

        public TLogger Query<TLogger>( TKey key ) where TLogger : ISimpleLogger
        {
            lock( lockObject )
            {
                return (TLogger)loggers[key];
            }
        }

    }
}
