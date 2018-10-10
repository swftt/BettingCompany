using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    public class User:Human
    {
        public decimal Balance { get; set; } = 0;
        public string Valute { get; } = "GRN";
        public User(string name,string surname,string country,string login,string password,string email,string phoneNumber,int age,decimal balance=0,string valute="GRN")
            :base(name,surname,country,age,password,login,email,phoneNumber)
        {
            Balance = balance;
            Valute = valute;
        }
        public User()
        {

        }
        public override string ToString()
        {
            return base.ToString() + $"{Balance} {Valute}";
        }
    }
}
