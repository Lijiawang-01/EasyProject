using AutoMapper;
using CommonManager.Helper;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SignalR
{
    /// <summary>
    /// SignalR聊天帮助类
    /// </summary>
    public class ChatHelper : IHostedService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private Timer _timer;
        private DateTime _serverTime;
        public ChatHelper(IHubContext<ChatHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            _serverTime = DateTime.Now;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //注册定时器
            _timer = new Timer(UpdateServerTime, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 定时推送服务器时间
        /// </summary>
        /// <param name="state"></param>
        private async void UpdateServerTime(object state)
        {
            _serverTime = DateTime.Now;

            // 检查是否仍然处于活动状态
            if (_hubContext != null
                && _hubContext.Clients != null)
            {
                // 获取连接biddingHub的客户端数量
                var connectionCount = ChatHub.ConnectionIds.Count;
                if (connectionCount > 0)
                {
                    //获取tenderBiddingId，分组推送倒计时
                    foreach (var roomId in ChatHub.ChatRooms.KeyToArray())
                    {
                        var roomObj = (BaseUsersRes)ChatHub.ChatRooms[roomId];
                        await _hubContext.Clients.Client(roomId).SendAsync("ReceiveBiddingServerTime", roomObj);

                    }
                }
                else
                {
                    //无客户端连接，不推送
                }
            }
            else
            {
                //无客户端连接，不推送
            }
        }

    }
}
