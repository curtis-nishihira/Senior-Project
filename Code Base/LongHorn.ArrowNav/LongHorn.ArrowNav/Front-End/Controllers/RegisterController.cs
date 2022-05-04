using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Front_End.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        //register/confirmemail?email=whateverstring
        [HttpGet("confirmEmail")]
        public void ConfirmEmail(string email)
        {
            UMManager manager = new UMManager();
            var result = manager.confirmEmail(email);
            if (result == "confirmed")
            {
                Response.Redirect("https://arrownav2.azurewebsites.net/account/confirmationpage");
            }
            else
            {
                Console.WriteLine(result);
            }

        }


        [HttpPost]
        [Route("createaccount")]
        //have to add something to say if something went wrong 
        public string CreateAccount(AccountInfo newAccount)
        {
            UMManager manager = new UMManager();
            var result = manager.Create(newAccount);
            if (result == "Successful Account Creation")
            {
                var response = manager.sendConfirmationEmailAsync(newAccount._email).Result;
                if(response == true)
                {
                    return result;
                }
                else
                {
                    return "Invalid Email";
                }
            }
            else
            {
                return result;
            }

        }

    }
}
