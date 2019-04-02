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
    public class Login : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 2)
            {
                var sesssionList = session.AppServer.GetAllSessions();
                if (sesssionList != null)
                {
                    ChatSession oldSession = sesssionList.FirstOrDefault(s => requestInfo.Parameters[0].Equals(s.Id));
                    if (oldSession != null)
                    {
                        oldSession.Send("login other computer，you kick off！");
                        oldSession.Close();
                    }
                }

                //不去数据库查询了
                session.Id = requestInfo.Parameters[0];
                session.Password = requestInfo.Parameters[1];
                session.IsLogin = true;
                session.LoginTime = DateTime.Now;

                session.Send("Login Success");
                {
                    //登陆获取离线消息
                    var UnReadData = ChatDataManager.Read(session.Id);
                    if (UnReadData != null)
                    {
                        foreach (var c in UnReadData)
                        {
                            session.Send($"{c.FromId}给你发消息：{c.Message} {c.Id}");
                            c.State = 1;
                        }
                    }
                    
                    //ChatDataManager.SendLogin(session.Id, c =>
                    // {
                    //     session.Send($"{c.FromId}给你发消息：{c.Message} {c.Id}");
                    // });
                }
            }
            else//能进入这个方法，说明已经是Check
            {
                session.Send("Wrong Parameter");
            }
        }
    }
}
