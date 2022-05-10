using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public LoginResponse Login(LoginModel login)
        {
            UMManager umManager = new UMManager();
            var result = umManager.AuthenAccount(login);
            return result;
        }
        [HttpPost]
        [Route("updateSuccessfulAttempt")]
        public string UpdateSuccessfulAttempt(string email)
        {

            UMManager uMManager = new UMManager();
            var result = uMManager.UpdatingSuccessfulAttempts(email);
            return result;
        }

        [HttpPost]
        [Route("updateFailedAttempts")]
        public string UpdateFailedAttempts(string email)
        {
            
            UMManager uMManager = new UMManager();
            var result = uMManager.UpdatingFailedAttempts(email);
            if (result.Contains("disabled"))
            {
                AccountInfo accountInfo = new AccountInfo();
                accountInfo._email = email;
                var response = uMManager.Disable(accountInfo);
            }
            return result;
        }

        public List<LoginModel>accounts = new List<LoginModel>();

        public string key = "arrownav";


        [HttpPost]
        [Route("getProfile")]
        public AccountInfo getProfile(string email)
        {
            UMManager umManager = new UMManager();
            var result = umManager.getProfile(email);
            return result;
        }

        [HttpGet]
        [Route("getOTP")]
        public string getOTP(string email)
        {
            UMManager umManager = new UMManager();
            var result = umManager.OtpRequestAsync(email).Result;
            return result;
        }


        [HttpPost]
        [Route("createcookie")]
        public string CreateCookie(CookieModel model)
        {

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(2)
            };

            Response.Cookies.Append(key, JsonSerializer.Serialize(model), cookieOptions);
            return "cookie created";

        }

        [HttpPost]
        [Route("deleteAccount")]
        public string deleteAccount(string email)
        {
            UMManager umManager = new UMManager();
            var result = umManager.Delete(email);
            return result;
        }


        [HttpGet]
        [Route("removecookie")]
        public string RemoveCookie()
        {
            Response.Cookies.Delete(key);
            return "cookie removed";
        }

        [HttpGet]
        [Route("getAllUsers")]
        public List<User> getAllUsers()
        {
            UMManager umManager = new UMManager();
            var result = umManager.getAllUsers();
            return result;
        }

        [HttpPost]
        [Route("deleteUser")]
        public string deleteUser(string email)
        {
            UMManager umManager = new UMManager();
            var result = umManager.Delete(email);
            return result;
        }

        [HttpPost]
        [Route("updateUser")]
        public string updateUser(User user)
        {
            UMManager umManager = new UMManager();
            var result = umManager.Update(user);
            return result;
        }
    }
}