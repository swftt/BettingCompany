using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    public class User:Human
    {
        private Dictionary<DateTime, BettingsEvents> betsPlaced = new Dictionary<DateTime, BettingsEvents>();
        public Dictionary<DateTime,BettingsEvents> BetsPlaced
        { get
            {
                return betsPlaced;
            }
            set
            {
                betsPlaced = value;
            }
        }
        public decimal Balance { get; set; } = 0;
        public string Valute { get; } = "GRN";
        public User(string name, string surname, string country, string login, string password, string email, string phoneNumber, int age, decimal balance = 0, string valute = "GRN", Dictionary<DateTime, BettingsEvents> myBets = null)
            :base(name,surname,country,age,password,login,email,phoneNumber)
        {
            Balance = balance;
            Valute = valute;
            BetsPlaced = myBets;
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
