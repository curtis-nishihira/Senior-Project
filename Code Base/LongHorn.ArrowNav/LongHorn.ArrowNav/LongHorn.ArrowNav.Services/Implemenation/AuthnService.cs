using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LongHorn.ArrowNav.Services
{
    public class AuthnService : IAuthnService<LoginModel>
    {
        public string ApplyAuthn(LoginModel model)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.AuthnAccount(model);
            return result;
        }
        public string confirmEmail(string email)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.confirmUserEmail(email);
            return result;
        }


        // this generates the otp 
        public string OTPGenerator()
        {
            string OTP = "";
            List<char> otp = new List<char>();
            var random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < 8; i++)
            {
                otp.Add(characters[random.Next(characters.Length)]);
            }
            OTP = String.Join("", otp);
            return OTP;
        }
        // sends email with otp to the user. Later to be implemented into the main repo
        public async Task sendEmailAsync(string email, string otp)
        {
            var apiKey = ConfigurationManager.AppSettings.Get("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("longhornarrownav@gmail.com", "ArrowNav");
            var subject = "One Time Password";
            var to = new EmailAddress(email);
            var plainTextContent = string.Format("This password will expire in 2 minutes.\n {0}", otp);
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
        }
        public bool isAdmin(LoginModel model)
        {
            UMRepository umRepository = new UMRepository();
            var result = umRepository.AuthorizationLevel(model);
            return result;
        }
    }
}