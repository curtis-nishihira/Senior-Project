using LongHorn.ArrowNav.Managers;
using LongHorn.ArrowNav.Models;
using System;

namespace LongHorn.ArrowNav.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool loop = true;
            Program p = new Program();
            while (loop)
            {
                p.getLoginMenu();
                var x = Console.ReadLine();
                if (x.Equals("1"))
                {
                    Console.WriteLine("Email");
                    var email = Console.ReadLine();
                    Console.WriteLine("Password");
                    var passphrase = Console.ReadLine();
                    AuthorizationManager authorizationManager = new AuthorizationManager();
                    AccountInfo loginAccount = new AccountInfo(email, passphrase);
                    var response = authorizationManager.AuthzAccount(loginAccount);
                    if (response.Equals("admin"))
                    {
                        bool adminLoop = true;
                        while (adminLoop)
                        {
                            p.getAdminMenu();
                            var choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":
                                    UserCreateManager createManager = new UserCreateManager();
                                    AccountInfo account = p.newAccountInfo();
                                    var message = createManager.SaveChanges(account);
                                    break;
                                case "2":
                                    UserUpdateManager updateManager = new UserUpdateManager();
                                    Console.WriteLine("Please enter an account email that you are trying to update");
                                    var email2 = Console.ReadLine();
                                    Console.WriteLine("Please enter the updated password");
                                    var passphrase2 = Console.ReadLine();
                                    Console.WriteLine("Please enter the account type(admin or user) that you want to change it to");
                                    var accountType2 = Console.ReadLine();
                                    AccountInfo newAccount = new AccountInfo(email, passphrase, accountType2);
                                    var message2 = updateManager.SaveChanges(newAccount);
                                    break;
                                case "3":
                                    UserDeleteManager deleteManager = new UserDeleteManager();
                                    Console.WriteLine("Enter the email of the account you would want to delete");
                                    var requstedAccountDeletion = Console.ReadLine();
                                    AccountInfo deleteAccount = new AccountInfo(requstedAccountDeletion);
                                    var message3 = deleteManager.SaveChanges(deleteAccount);
                                    break;
                                case "4":
                                    UserDisableManager disableManager = new UserDisableManager();
                                    Console.WriteLine("Enter the email of the account you would want to disable");
                                    var requstedAccountDisable = Console.ReadLine();
                                    AccountInfo disableAccount = new AccountInfo(requstedAccountDisable);
                                    var message4 = disableManager.SaveChanges(disableAccount);
                                    break;
                                case "5":
                                    UserEnableManager enableManager = new UserEnableManager();
                                    Console.WriteLine("Enter the email of the account you would want to enable");
                                    var requstedAccountEnable = Console.ReadLine();
                                    AccountInfo enableAccount = new AccountInfo(requstedAccountEnable);
                                    var message5 = enableManager.SaveChanges(enableAccount);
                                    break;
                                case "e":
                                    adminLoop = false;
                                    break;
                                case "E":
                                    adminLoop = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid input");
                                    break;
                            }
                        }


                    }
                    else if (response.Equals("user"))
                    {

                        bool userLoop = true;
                        while (userLoop)
                        {
                            p.getUserMenu();
                            var choice = Console.ReadLine();
                            if (choice.Equals("1"))
                            {
                                Console.WriteLine("Are you sure you would like to delete your account(y/n)");
                                var delete = Console.ReadLine();
                                if (delete.Equals("Y") || delete.Equals("y"))
                                {
                                    UserDeleteManager deleteManager = new UserDeleteManager();
                                    var message = deleteManager.SaveChanges(loginAccount);
                                    Console.WriteLine(message + "\n");
                                }
                                else if (delete.Equals("N") || delete.Equals("n"))
                                {
                                    break;
                                }
                            }
                            else if (choice.Equals("e") || choice.Equals("E"))
                            {
                                userLoop = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input. Try Again.");
                            }
                        }


                    }
                    else
                    {
                        Console.WriteLine(response);
                    }
                }
                else if (x.Equals("2"))
                {
                    Console.WriteLine("Please enter an email");
                    var email = Console.ReadLine();
                    Console.WriteLine("Please enter a password");
                    var passphrase = Console.ReadLine();
                    Console.WriteLine("Please re enter your password");
                    var samePassphrase = Console.ReadLine();
                    var innerloop = true;
                    while (innerloop)
                    {
                        if (passphrase.Equals(samePassphrase))
                        {
                            innerloop = false;
                        }
                        else
                        {
                            Console.WriteLine("Please re enter your password");
                            samePassphrase = Console.ReadLine();
                        }
                    }
                    UserCreateManager createManager = new UserCreateManager();
                    AccountInfo newAccount = new AccountInfo(email, passphrase, "user");
                    var response = createManager.SaveChanges(newAccount);
                    Console.WriteLine(response + "\n");

                }
                else if (x.Equals("e") || x.Equals("E"))
                {
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again");
                }
            }
            

        }
        public void getLoginMenu()
        {
            Console.WriteLine("1) Login\n2) Register Account\nPress 'e' to exit");
        }
        public void getUserMenu()
        {
            Console.WriteLine("1) Delete Account\nPress 'e' to exit");
        }
        public void getAdminMenu()
        {
            Console.WriteLine("1) Create User\n2) Update Account\n3) Delete Account\n4) Disable Account\n5) Enable Account\nPress 'e' to log out");
        }
        public AccountInfo newAccountInfo()
        {
            Console.WriteLine("Please enter an email");
            var email = Console.ReadLine();
            Console.WriteLine("Please enter a password");
            var passphrase = Console.ReadLine();
            Console.WriteLine("Please enter the account type(admin or user)");
            var accountType = Console.ReadLine();
            AccountInfo newAccount = new AccountInfo(email, passphrase, accountType);
            return newAccount;
        }
    }
}
