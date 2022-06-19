﻿using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.models.Models
{
    public class LoginModel
    {
        public LoginModel() { }
        public LoginModel(User user)
        {
            Email = user.Email;
            Password = user.Password;
        }
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}
