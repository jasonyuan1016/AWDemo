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
    public class Chat: CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            //ToId  Message  
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 2)
            {
                string toId = requestInfo.Parameters[0];
                string message = requestInfo.Parameters[1];

                ChatSession toSession = session.AppServer.GetAllSessions().FirstOrDefault(s => toId.Equals(s.Id));
                //用户在线
                string modelId = Guid.NewGuid().ToString();
                if (toSession != null)
                {
                    toSession.Send($"{session.Id}给你发消息：{message} {modelId}");
                    //只能保证把数据发出去了，但是不保证目标一定收到
                    //需要客户端回发确认！
                    ChatDataManager.Add(toId, new ChatModel()
                    {
                        FromId = session.Id,
                        ToId = toSession.Id,
                        Message = message,
                        Id = modelId,
                        State = 1,//待确认
                        CreateTime = DateTime.Now
                    });
                }
                else
                {
                    ChatDataManager.Add(toId, new ChatModel()
                    {
                        FromId = session.Id,
                        ToId = toId,
                        Message = message,
                        Id = modelId,
                        State = 0,//未发送
                        CreateTime = DateTime.Now
                    });
                    Console.WriteLine($"{toId}不在线，消息暂时没能送达！");
                }
            }
            else
            {
                session.Send("Wrong Parameter");
            }
        }
    }
}
