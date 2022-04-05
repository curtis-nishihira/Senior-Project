﻿using Microsoft.AspNetCore.Mvc;
using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Managers;
using System.Text.Json;

/*
 * TODO:
 * 1) fix the http post so it works as intended
 * 2) get the otp to work as intended as well
 * 3) Continue on the routing functionality again. 
 * 
 */
namespace Front_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        //comment 
        [HttpPost]
        public string Login(LoginModel login)
        {
            UMManager umManager = new UMManager();
            var result = umManager.AuthenAccount(login);
            return result;
        }

        public List<LoginModel>accounts = new List<LoginModel>();

        public string key = "arrownav";

        [HttpPost]
        [Route("createcookie")]
        public string CreateCookie(LoginModel model)
        {
            LoginModel loginModel = new LoginModel()
            {
                _Username = model._Username
            };

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(2)
            };
            Response.Cookies.Append(key, JsonSerializer.Serialize(loginModel), cookieOptions);
            return "cookie created";

        }




        public void RemoveCookie()
        {
            if (Request.Cookies[key] != null)
            {
                Response.Cookies.Delete(key);
            }
        }


    }
}