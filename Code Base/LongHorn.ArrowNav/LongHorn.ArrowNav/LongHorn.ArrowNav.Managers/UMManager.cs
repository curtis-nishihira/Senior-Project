using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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

        public string Update(User user)
        {
            UpdateService createService = new UpdateService();
            var result = createService.UpdateAccount(user);
            LogManager logManager = new LogManager();
            Log entry = new Log(result, "Info", "Data Layer", user.email);
            logManager.Log(entry);
            return result;
        }

        public List<User> getAllUsers()
        {
            UpdateService updateService = new UpdateService();
            List<User> result = updateService.getAllUsers();
            return result;
        }

        public string AuthenAccount(LoginModel model)
        {
            AuthnService authnService = new AuthnService();
            var result = authnService.ApplyAuthn(model);
            return result;
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

        public string sendConfirmationEmail(string email)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.sendgrid.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "apikey",
                    Password = "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"
                }
            };
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("longhornarrownav@gmail.com");
            mail.Subject = "Confirmation Email";
            mail.Body = string.Format("Please click the following link to confirm your email which will fully activate you account.\n" +
                "Click <a href=\"https://longhorntest.azurewebsites.net/registerform/confirmemail?email={0}\">here</a>.", email);
            mail.IsBodyHtml = true;

            try
            {
                client.Send(mail);
                return "sent";
            }
            catch (Exception ex)
            {
                return "idk";

            }
        }

    }
}