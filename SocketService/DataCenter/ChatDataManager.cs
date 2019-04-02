using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService.DataCenter
{
    public class ChatDataManager
    {
        /// <summary>
        /// key是用户id
        /// List 这个用户的全部消息
        /// </summary>
        private static Dictionary<string, List<ChatModel>> dictionary = new Dictionary<string, List<ChatModel>>();

        public static void Add(string userId, ChatModel model)
        {
            if (dictionary.ContainsKey(userId))
            {
                dictionary[userId].Add(model);
            }
            else
            {
                dictionary[userId] = new List<ChatModel>() { model };
            }
        }
        public static void Remove(string userId, string modelId)
        {
            if (dictionary.ContainsKey(userId))
            {
                dictionary[userId] = dictionary[userId].Where(m => m.Id != modelId).ToList();
            }
        }

        public static void SendLogin(string userId, Action<ChatModel> action)
        {
            if (dictionary.ContainsKey(userId))
            {
                foreach (var item in dictionary[userId])
                {
                    action.Invoke(item);
                    item.State = 1;
                }
            }
        }

        public static List<ChatModel> Read(string userId)
        {
            if (dictionary.ContainsKey(userId))
            {
                return dictionary[userId];
            }
            return null;
        }
    }
}
