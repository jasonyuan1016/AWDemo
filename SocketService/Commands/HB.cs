using SocketService.DataCenter;
using SocketService.Session;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService.Commands
{
    /// <summary>
    /// HeartBeat
    /// </summary>
    public class HB : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 1)
            {
                if ("$".Equals(requestInfo.Parameters[0]))
                {
                    session.LastHBTime = DateTime.Now;
                    session.Send("$");
                }
                else
                {
                    session.Send("Wrong Parameter");
                }
            }
            else
            {
                session.Send("Wrong Parameter");
            }
        }
    }
}
