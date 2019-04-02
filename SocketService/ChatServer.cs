using SocketService.Filters;
using SocketService.Session;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService
{
    [AuthorisizeFilter]
    public class ChatServer : AppServer<ChatSession>
    {
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Console.WriteLine("准备读取配置文件。。。。");
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            Console.WriteLine("Chat服务启动。。。");
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            Console.WriteLine("Chat服务停止。。。");
            base.OnStopped();
        }

        /// <summary>
        /// 新的连接
        /// </summary>
        /// <param name="session"></param>
        protected override void OnNewSessionConnected(ChatSession session)
        {
            Console.WriteLine("Chat服务新加入的连接:" + session.LocalEndPoint.Address.ToString());
            base.OnNewSessionConnected(session);
        }


    }
}
