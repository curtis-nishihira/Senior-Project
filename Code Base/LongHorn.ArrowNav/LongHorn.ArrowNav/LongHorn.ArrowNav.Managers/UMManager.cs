using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LongHorn.ArrowNav.Managers
{
    public class UMManager
    {

        public UMManager()
        {

        }

        public string Create(AccountInfo accountInfo)
        {
            CreateService createService = new CreateService();
            var result = createService.CreateAccount(accountInfo);
            Log entry = new Log(result, "Info", "Data Layer", accountInfo._email);
            LogManager logManager = new LogManager();
            logManager.Log(entry);
            return result;
        }

        public string Delete(string email)
        {
            DeleteService createService = new DeleteService();
            var result = createService.DeleteAccount(email);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", email);
            logManager.Log(entry);
            return result;
        }
        public string Disable(AccountInfo account)
        {
            DisableService createService = new DisableService();
            var result = createService.DisableAccount(account);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", account._email);
            logManager.Log(entry);
            return result;
        }
        public string Enable(AccountInfo account)
        {
            EnableService createService = new EnableService();
            var result = createService.EnableAccount(account);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", account._email);
            logManager.Log(entry);
            return result;
        }

        public string Update(AccountInfo account)
        {
            UpdateService createService = new UpdateService();
            var result = createService.UpdateAccount(account);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", account._email);
            logManager.Log(entry);
            return result;
        }
        public string AuthenAccount(LoginModel model)
        {
            AuthnService authnService = new AuthnService();
            var result = authnService.ApplyAuthn(model);
            return result;
        }

        public async Task<string> OtpRequestAsync(string email)
        {
            AuthnService authnService = new AuthnService();
            var otp = authnService.OTPGenerator();
            await authnService.sendEmailAsync(email, otp);
           
            return otp;
        }

        public string AuthzAccount(AccountInfo account)
        {
            AuthzService authzService = new AuthzService();
            var result = authzService.ApplyAuthz(account);
            return result;
        }

        public string confirmEmail(string email)
        {
            AuthnService authnService = new AuthnService();
            var result = authnService.confirmEmail(email);
            return result;
        }

        public AccountInfo getProfile(string email)
        {
            EnableService enableService = new EnableService();
            var result = enableService.getProfileByEmail(email);
            return result;
        }

        public async Task<bool> sendConfirmationEmailAsync(string email)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("longhornarrownav@gmail.com", "ArrowNav");
            var subject = "Confirmation Email";
            var to = new EmailAddress(email);
            var plainTextContent = "";
            var htmlContent = string.Format("Please click the following link to confirm your email which will fully activate you account.\n" +
                "Click <a href=\"https://arrownav2.azurewebsites.net/register/confirmemail?email={0}\">here</a>.", email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
            
        }

    }
}