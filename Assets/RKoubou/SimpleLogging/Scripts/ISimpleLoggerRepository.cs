/* =========================================================================

    ISimpleLoggerRepository.cs
    Copyright(c) R-Koubou

============================================================================ */

namespace RKoubou.SimpleLogging
{
    public interface ISimpleLoggerRepository<TKey> : System.IDisposable
    {
        int Count { get; }

        void Add<TLogger>( TKey key, TLogger logger ) where TLogger : ISimpleLogger;

        void Remove( TKey key );

        TLogger Query<TLogger>( TKey key ) where TLogger : ISimpleLogger;
    }
}
