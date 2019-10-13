﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class ChatModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "У чата должно быть имя")]
        public string Name { get; set; }
        public bool IsLocal { get { return (Users.Count == 2); } set {;} }
        public byte[] Icon { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public ChatModel()
        {
            Messages = new List<Message>();
            Users = new List<User>();
        }

    }
}