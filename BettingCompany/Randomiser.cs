using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    class Randomiser
    {
        static private readonly Random randomHumanValue = new Random();
        public static decimal GetRandMonetPlaced()
        {
            decimal randMoney = randomHumanValue.Next(100, 10000) + (decimal)(Math.Round(randomHumanValue.NextDouble(), 2));
            return randMoney;
        }
        
    }
}
