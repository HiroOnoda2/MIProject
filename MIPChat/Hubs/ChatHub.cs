﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MIPChat.Models;

namespace MIPChat.Hubs
{
    public class ChatHub : Hub
    {
        private static List<UserViewModel> Users { get; set; }

        public ChatHub()
        {

        }




        public void SendUserMessage(Guid userId, string messageValue)
        {
            Clients.User(userId.ToString()).addMessage(messageValue);
        }

        public void SendChatMessage(Guid chatID, string messageValue)
        {
            Clients.Group(chatID.ToString()).addMessage(messageValue);
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.UserId.ToString() == id))
            {
                Users.Add(new UserViewModel { UserId = Guid.Parse(id), Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.UserId.ToString() == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
