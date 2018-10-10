using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
   
    class Program
    {
        static void Main(string[] args)
        {

            //User user = new User("Vova", "Chubiuk", "Ukraine", "12", "13", "lol@gmail.com", "281159", 19);
            //GGBET.AddBalance(user,500,"DOL");
            //Console.WriteLine(user.Balance);
            //GGBET.AddBalance(user, 1000);
            //Console.WriteLine(user.Balance);
            //Console.WriteLine(user.ToString());
            //GGBET.WithdrawMoney(user, 70000);
            //Console.WriteLine(user.ToString());


            CasinoBets casinoBets = new CasinoBets();
            //casinoBets.AddBettingEvents();
            //casinoBets.WriteToFile();
            //casinoBets.ReadFromFile();
            //casinoBets.AddBettingEvents();
            //casinoBets.WriteToFile();
            //casinoBets.ReadFromFile();
            casinoBets.RegisterUser();
            casinoBets.WriteusersAdminsToDataBase();
            casinoBets.ReadusersAdminsFromDataBase();
            
            

        }
    }
}
