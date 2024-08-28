using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SignalR
{
    public class ChatHubTest : Hub
    {
        private static Dictionary<string, string> dicUsers = new Dictionary<string, string>();
        public override Task OnConnectedAsync()    //登录
        {
            Console.WriteLine($"ID:{Context.ConnectionId} 已连接");   //控制台记录
            var cid = Context.ConnectionId;
            //根据id获取指定客户端
            var client = Clients.Client(cid);

            //向指定用户发送消息
            //client.SendAsync("Self", cid);

            //像所有用户发送消息
            Clients.All.SendAsync("ReceivePublicMessageLogin", $"{cid}加入了聊天室");        //界面显示登录
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)       //退出的时候
        {
            Console.WriteLine($"ID:{Context.ConnectionId} 已断开");
            var cid = Context.ConnectionId;
            //根据id获取指定客户端
            var client = Clients.Client(cid);

            //向指定用户发送消息
            //client.SendAsync("Self", cid);

            //像所有用户发送消息
            Clients.All.SendAsync("ReceivePublicMessageLogin", $"{cid}离开了聊天室");        //界面显示登录
            return base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendPublicMessage(string user, string message)
        {                                                     //string user,
            await Clients.All.SendAsync("ReceivePublicMessage", user, message);   //ReceiveMessage 提供给客户端使用
        }

        /// <summary>
        /// 用户登录，密码就不判断了
        /// </summary>
        /// <param name="userId"></param>
        public void Login(string userId)     //对应前端的invoke
        {
            if (!dicUsers.ContainsKey(userId))
            {
                dicUsers[userId] = Context.ConnectionId;
            }
            Console.WriteLine($"{userId}登录成功，ConnectionId={Context.ConnectionId}");
            //向所有用户发送当前在线的用户列表
            Clients.All.SendAsync("dicUsers", dicUsers.Keys.ToList());   //对应前端的on
        }

        public void ChatOne(string userId, string toUserId, string msg)     //用户  发送到的用户      发送的消息
        {
            string newMsg = $"{userId}对你说{msg}";//组装后的消息体
                                                //如果当前用户在线
            if (dicUsers.ContainsKey(toUserId))
            {
                Clients.Client(dicUsers[toUserId]).SendAsync("ChatInfo", newMsg);
            }
            else
            {
                //如果当前用户不在线，正常是保存数据库，等上线时加载，暂时不做处理
            }
        }

    }
}
