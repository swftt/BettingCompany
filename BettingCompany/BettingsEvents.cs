using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BettingCompany
{


   
    public enum TypeOfEvent { eSports, Football, Basketball, Cricket, Valleyball, MMA, Box, Marathon, Tennis, Hokey };

  public  class BettingsEvents
    {
       
        public TypeOfEvent BettingEvents { get; set; } = TypeOfEvent.eSports;
        
        public string TeamOne { get; set; }
        
        public string TeamTwo { get; set; }

       
        public decimal TeamOneMonetPlaced { get; set; } = Randomiser.GetRandMonetPlaced();
        public decimal TeamTwoMoneyPlaced { get; set; } = Randomiser.GetRandMonetPlaced();
      

        public DateTime EventStart { get; set; }
        
        public DateTime EventEnd { get; set; }
        public double TeamOneCoefitient { get; set; }
        public double TeamTwoCoefitient { get; set; }
        public void SetTeamOneCoefitient(double money)
        {
            double total = (double)TeamOneMonetPlaced + (double)TeamTwoMoneyPlaced + money;
           double percentage =(((double)TeamOneMonetPlaced + money)/total);
            TeamOneCoefitient =Math.Round(1 / percentage,2);
        }
        public void SetTeamTwoCoefitient(double money)
        {
            double percentage = (double)TeamOneMonetPlaced + (double)TeamTwoMoneyPlaced + money;
            percentage = ((double)TeamTwoMoneyPlaced + money) / percentage;
            TeamTwoCoefitient =Math.Round(1 / percentage,2);
        }
        public override string ToString()
        {
            return $"{BettingEvents.ToString()} {TeamOne} {TeamOneCoefitient}vs{TeamTwo} {TeamTwoCoefitient} \n            Beginning:{EventStart}  End:{EventEnd}";
        }
    }
}
