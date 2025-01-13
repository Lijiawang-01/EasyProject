using AutoMapper;
using CommonManager.Helper;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonManager.SignalR
{
    /// <summary>
    /// 聊天室
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// 客户端连接数
        /// </summary>
        public static IList<string> ConnectionIds = new List<string>();
        /// <summary>
        /// 竞价室列表
        /// </summary>
        public static SortedHashtable ChatRooms = new SortedHashtable();

        public IMapper _mapper { get; set; }
        public ISqlSugarClient _db { get; set; }
        public ChatHub(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db= db;
        }
        private BaseUsersRes GetUser()
        {
            var user = _db.Queryable<BaseUsers>().ToList().First();
            if (user != null)
            {
                return _mapper.Map<BaseUsersRes>(user);
            }
            return new BaseUsersRes();
        }
        public override Task OnConnectedAsync()
        {
            var clientId = Context.ConnectionId;

            if (!ConnectionIds.Contains(clientId))
            {
                ConnectionIds.Add(clientId);
            }
            var userName = Context.GetHttpContext().Request.Query["userName"] + "";
            var userObj = GetUser();
            Console.WriteLine("BiddingHub_开始连接clientId:" + clientId);
            if (!ChatRooms.ContainsKey(clientId))
            {
                ChatRooms.Add(clientId, userObj);
            }
            else
            {
                ChatRooms[clientId] = userObj;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var clientId = Context.ConnectionId;
            if (ConnectionIds.Any(x => x == clientId))
            {
                ConnectionIds.Remove(clientId);
            }
            Console.WriteLine("BiddingHub_断开连接clientId:" + clientId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAllMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user + "says", message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 采购员在聊天室点击【发送】按钮，将聊天信息发送给vue所有监听事件ReceiveMessage
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="message"></param>
        /// <param name="senderId"></param>
        /// <param name="senderName"></param>
        /// <param name="instantMsgId"></param>
        /// <returns></returns>
        public async Task SendMessageToGroup(string groupName, string message, string senderId, string senderName, string instantMsgId)
        {
            if (!string.IsNullOrEmpty(groupName))
            {
                var chatLogSendTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
                var groupNames = groupName.Split(',');
                foreach (var item in groupNames)
                {
                    await Clients.Group(item).SendAsync("ReceiveMessage", new { message, senderId, senderName, instantMsgId, chatLogSendTime });
                }
            }
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", user, message);
        }
    }

}
