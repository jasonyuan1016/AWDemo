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
    /// 客户端回发确认
    /// </summary>
    public class Confirm : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            //ToId  Message  
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 1)
            {
                string modelId = requestInfo.Parameters[0];//确认时  把消息ID发回去
                Console.WriteLine($"用户{session.Id} 已确认 收到 消息{modelId}");
                ChatDataManager.Remove(session.Id, modelId);//改状态或者删除
            }
            else
            {
                session.Send("Wrong Parameter");
            }
        }
    }
}
