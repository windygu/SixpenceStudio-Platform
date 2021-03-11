using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace SixpenceStudio.Core.Socket
{
    public class SocketController : BaseController
    {
        private static List<WebSocket> _sockets = new List<WebSocket>();

        [HttpGet]
        public HttpResponseMessage Connect()
        {
            HttpContext.Current.AcceptWebSocketRequest(ProcessRequest);
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        public async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;
            _sockets.Add(socket);

            //进入一个无限循环，当web socket close是循环结束
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[1024]);
                var receivedResult = await socket.ReceiveAsync(buffer, CancellationToken.None); // 对web socket进行异步接收数据
                if (receivedResult.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None); // 如果client发起close请求，对client进行ack
                    _sockets.Remove(socket);
                    break;
                }

                if (socket.State == WebSocketState.Open)
                {
                    string recvMsg = Encoding.UTF8.GetString(buffer.Array, 0, receivedResult.Count);
                    var recvBytes = Encoding.UTF8.GetBytes(recvMsg);
                    var sendBuffer = new ArraySegment<byte>(buffer.Array);
                    foreach (var innerSocket in _sockets) // 当接收到文本消息时，对当前服务器上所有web socket连接进行广播
                    {
                        if (innerSocket != socket)
                        {
                            await innerSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
            }
        }
    }
}
