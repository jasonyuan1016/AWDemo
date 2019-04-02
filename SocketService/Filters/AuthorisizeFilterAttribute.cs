using SocketService.Session;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService.Filters
{
    class AuthorisizeFilterAttribute: CommandFilterAttribute
    {
        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            ChatSession session = (ChatSession)commandContext.Session;
            string command = commandContext.CurrentCommand.Name;
            if (!session.IsLogin)
            {
                if (!command.Equals("Login"))
                {
                    session.Send($"请先完成登陆，在做别的操作");
                    session.Close();
                    commandContext.Cancel = true;//取消命令，不再继续
                }
                else
                {

                }
            }
            else if (!session.IsOnLine)
            {
                session.LastHBTime = DateTime.Now;//客户端给我发消息，证明在线
            }
            else
            {

            }
        }

        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            
        }
    }
}
