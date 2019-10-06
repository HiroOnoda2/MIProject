﻿using MIPChat.DAL;
using MIPChat.DAL.Repository;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MIPChat.Controllers
{
    public class MessangerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserMessAndChatsAsync(User user)
        {
            var chatRepository = new ChatRepository(new ChatDBContext());
            var allExistingChats = chatRepository.FindAllChatsForUser(user).Result;

            return PartialView(allExistingChats);
        }

        [HttpPost]
        public ActionResult AvailNewMesssges(User user)
        {
            var chatRepository = new ChatRepository(new ChatDBContext());
            var allExistingChats = chatRepository.FindAllChatsForUser(user).Result.ToList();

            //var userRepository = new UserRepository(new ChatDBContext());
            //var allAvailableUsers = userRepository.FindAllAvailable().Result;


            return PartialView();//allAvailableUsers);
        }






        [HttpPost]
        public ActionResult UserConcreteChat(Guid ChatID)
        {
            var chatRepository = new ChatRepository(new ChatDBContext());
            var chatInst = chatRepository.FindById(ChatID).Result;

            return PartialView(ChatID);
        }


        [HttpPost]
        public ActionResult FindChat(string name)
        {
            var chatRepository = new ChatRepository(new ChatDBContext());
            ChatModel chatModel = new ChatModel();// = chatRepository.FindByName(name);

            return PartialView(chatModel);
        }



    }
}