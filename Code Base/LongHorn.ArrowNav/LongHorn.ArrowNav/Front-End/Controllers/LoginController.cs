﻿using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;

/*
 * TODO:
 * 1) fix the http post so it works as intended
 * 2) get the otp to work as intended as well
 * 3) Continue on the routing functionality again. 
 * 
 * 
 */
namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public string get()
        {
            return "default get";
        }
        [HttpPost]
        public string Login(LoginModel login)
        {
            UMManager umManager = new UMManager();
            var result = umManager.AuthenAccount(login);
            return result;
        }



    }
}