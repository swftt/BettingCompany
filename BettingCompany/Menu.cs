using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    public class Menu
    {
        public void WinLoseObservant(object sender, StatusEventArgs args)
        {
            Console.Write(args.msg);
        }
        private CasinoBets casino = new CasinoBets { manage = new VisitorManage() };
        

       public void StartBC()
        {
            casino.Lose += WinLoseObservant;
            casino.Win += WinLoseObservant;
            char choice = 'y';
            while(choice=='y'|| choice=='Y')
            {
                int adminChoice;
                int userChoice;
                int startMenuChoice;
                Console.WriteLine("1.Create user account" +
                    "\n 2.Create admin account" +
                    "\n 3.Login to existing account");
                startMenuChoice = Int32.Parse(Console.ReadLine());
                switch(startMenuChoice)
                {
                    case 1: casino.manage.RegisterUser();
                        casino.manage.LoginToAccount();
                        if (casino.manage.LoggedUser != null)
                        {
                            while (choice == 'y' || choice == 'Y')
                            {
                                int regChoice;
                                Console.WriteLine("1.Deposite money" +
                                    "\n 2.Withdraw money" +
                                    "\n 3.Place bet" +
                                    "\n 4.Write to file" +
                                    "\n 5.Read from file" +
                                    "\n 6.Log out from account");
                                regChoice = Int32.Parse(Console.ReadLine());
                               
                                Console.WriteLine("Want to continue?(y/n)");
                                switch (regChoice)
                                {
                                    case 1:
                                        decimal money;
                                        Console.WriteLine("Money to add to account:");
                                        money = Decimal.Parse(Console.ReadLine());
                                        casino.manage.AddBalance(money);
                                        break;
                                    case 2:
                                        decimal moneyWithdraw;
                                        Console.WriteLine("Money to withdraw from account:");
                                        moneyWithdraw = Decimal.Parse(Console.ReadLine());
                                        casino.manage.WithdrawMoney(moneyWithdraw);
                                        break;
                                    case 3:
                                        casino.PlaceBet();
                                        break;
                                    case 4:
                                        casino.manage.WriteLoggedUserToFile();
                                        break;
                                    case 5:
                                        casino.manage.ReadLoggedUserFromFile();
                                        break;
                                    case 6:
                                        casino.manage.LogOutFromUserAccount();
                                        break;
                                    default:
                                        break;
                                }
                                choice = Char.Parse(Console.ReadLine());
                            }
                        }
                        else
                        {
                            while (choice == 'y' || choice == 'Y')
                            {
                                int regChoice;
                                Console.WriteLine("1.Delete user by email" +
                                    "\n 2.Add bettings events" +
                                    "\n 3.write to file" +
                                    "\n 4.Read from file" +
                                    "\n 5.Log out from account");
                                regChoice = Int32.Parse(Console.ReadLine());
                                switch (regChoice)
                                {
                                    case 1:
                                        casino.manage.DeleteUserFromData();
                                        break;
                                    case 2:
                                        char choiceAdmin = 'y';
                                       while(choiceAdmin == 'y'|| choiceAdmin == 'Y')
                                        {
                                            casino.AddBettingEvents();
                                            Console.WriteLine("Want to continue?(y/n)");
                                            choiceAdmin = Char.Parse(Console.ReadLine());

                                        }
                                        break;
                                    case 3:
                                        casino.WriteEventsToFile();
                                        break;
                                    case 4:
                                        casino.ReadFromFile();
                                        break;
                                    case 5:
                                        casino.manage.LogOutFromAdminAccount();
                                        break;
                                }
                                Console.WriteLine("Want to continue?(y/n)");
                                choice = Char.Parse(Console.ReadLine());
                            }
                        }


                        break;
                    case 2:
                        casino.manage.RegisterAdmin();
                        casino.manage.LoginToAccount();
                        if (casino.manage.LoggedUser != null)
                        {
                            while (choice == 'y' || choice == 'Y')
                            {
                                int regChoice;
                                Console.WriteLine("1.Deposite money" +
                                    "\n 2.Withdraw money" +
                                    "\n 3.Place bet" +
                                    "\n 4.Write to file" +
                                    "\n 5.Read from file" +
                                    "\n 6.Log out from account");
                                regChoice = Int32.Parse(Console.ReadLine());
                                switch (regChoice)
                                {
                                    case 1:
                                        decimal money;
                                        Console.WriteLine("Money to add to account:");
                                        money = Decimal.Parse(Console.ReadLine());
                                        casino.manage.AddBalance(money);
                                        break;
                                    case 2:
                                        decimal moneyWithdraw;
                                        Console.WriteLine("Money to withdraw from account:");
                                        moneyWithdraw = Decimal.Parse(Console.ReadLine());
                                        casino.manage.WithdrawMoney(moneyWithdraw);
                                        break;
                                    case 3:
                                        casino.PlaceBet();
                                        break;
                                    case 4:
                                        casino.manage.WriteLoggedUserToFile();
                                        break;
                                    case 5:
                                        casino.manage.ReadLoggedUserFromFile();
                                        break;
                                    case 6:
                                        casino.manage.LogOutFromUserAccount();
                                        break;
                                }
                                Console.WriteLine("Want to continue?(y/n)");
                                choice = Char.Parse(Console.ReadLine());
                            }
                        }
                        else
                        {
                            while (choice == 'y' || choice == 'Y')
                            {
                                int regChoice;
                                Console.WriteLine("1.Delete user by email" +
                                    "\n 2.Add bettings events"+
                                    "\n 3.write to file" +
                                    "\n 4.Read from file" +
                                    "\n 5.Log out from account");
                                regChoice = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Want to continue?(y/n)");
                                choice = Char.Parse(Console.ReadLine());
                            }
                        }
                        break;
                    case 3:
                        casino.manage.LoginToAccount();
                        if (casino.manage.LoggedUser!=null)
                        {
                            while (choice == 'y' || choice == 'Y')
                            {
                                int regChoice;
                                Console.WriteLine("1.Deposite money" +
                                    "\n 2.Withdraw money" +
                                    "\n 3.Place bet" +
                                    "\n 4.Write to file" +
                                    "\n 5.Read from file" +
                                    "\n 6.Log out from account");
                                regChoice = Int32.Parse(Console.ReadLine());
                                switch (regChoice)
                                {
                                    case 1:
                                        decimal money;
                                        Console.WriteLine("Money to add to account:");
                                        money = Decimal.Parse(Console.ReadLine());
                                        casino.manage.AddBalance(money);
                                        break;
                                    case 2:
                                        decimal moneyWithdraw;
                                        Console.WriteLine("Money to withdraw from account:");
                                        moneyWithdraw = Decimal.Parse(Console.ReadLine());
                                        casino.manage.WithdrawMoney(moneyWithdraw);
                                        break;
                                    case 3:
                                        casino.PlaceBet();
                                        break;
                                    case 4:
                                        casino.manage.WriteLoggedUserToFile();
                                        break;
                                    case 5:
                                        casino.manage.ReadLoggedUserFromFile();
                                        break;
                                    case 6:
                                        casino.manage.LogOutFromUserAccount();
                                        break;
                                }
                                Console.WriteLine("Want to continue?(y/n)");
                                choice = Char.Parse(Console.ReadLine());
                            }
                        }
                        else
                        {
                            while (choice == 'y' || choice == 'Y')
                            {
                                int regChoice;
                                Console.WriteLine("1.Delete user by email" +
                                    "\n 2.Add bettings events" +
                                    "\n 3.write to file" +
                                    "\n 4.Read from file" +
                                    "\n 5.Log out from account");
                                regChoice = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Want to continue?(y/n)");
                                choice = Char.Parse(Console.ReadLine());
                            }
                        }


                        break;



                }
                Console.WriteLine("Want to continue?(y/n)");
                choice =Char.Parse(Console.ReadLine());
            }
        }

        private void Casino_Lose(object sender, StatusEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
