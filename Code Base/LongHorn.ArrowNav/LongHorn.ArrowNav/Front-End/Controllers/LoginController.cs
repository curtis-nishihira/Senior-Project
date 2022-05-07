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
        public string Login(LoginModel login)
        {
            UMManager umManager = new UMManager();
            var result = umManager.AuthenAccount(login);
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
        public string CreateCookie(LoginModel model)
        {
            LoginModel loginModel = new LoginModel()
            {
                Username = model.Username
            };
            
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(2)
            };

            UMManager uMManager = new UMManager();
            var isAdmin = uMManager.GetAuthorizationLevel(loginModel);

            loginModel.IsAuthorized = isAdmin;

            Response.Cookies.Append(key, JsonSerializer.Serialize(loginModel), cookieOptions);
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