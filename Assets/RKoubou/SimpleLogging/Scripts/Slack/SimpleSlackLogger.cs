/* =========================================================================

    SimpleSlackLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// A typical ISimpleLogger implemetation with Slack WebAPI (files.upload).
    /// </summary>
    /// <seealso cref="https://api.slack.com/methods/files.upload"/>
    /// <seealso cref="https://qiita.com/ykhirao/items/3b19ee6a1458cfb4ba21"/>
    public class SimpleSlackLogger : SimpleStringLogger
    {

        public struct UploadParameter
        {
            public string channel;
            public string title;
            public string fileName;
            public string fileType;
        }

        public override bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return true;
        }

        public SimpleSlackLogger( ISimpleLogFormatter formatter ) : base( formatter )
        {
        }

        public void SendToSlack( string token, UploadParameter parameter )
        {
            using( var client = new WebClient { Encoding = Encoding.UTF8 } )
            {
                var sendData = new NameValueCollection();

                sendData.Add( "token", token );
                sendData.Add( "channels", parameter.channel );
                sendData.Add( "filename", parameter.fileName );
                sendData.Add( "filetype", parameter.fileType );
                sendData.Add( "title", parameter.title );

                lock( stringBuilder )
                {
                    sendData.Add( "content", stringBuilder.ToString() );
                }

                client.Headers.Add( HttpRequestHeader.ContentType, "application/x-www-form-urlencoded" );
                var resultData = client.UploadValues( "https://slack.com/api/files.upload", sendData );
                var response = Encoding.UTF8.GetString( resultData );
            }
        }

        public void SendToSlackAsync( string token, UploadParameter parameter )
        {
            Task.Run( () =>
            {
                SendToSlack( token, parameter );
            });
        }
    }
}
