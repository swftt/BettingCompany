using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BettingCompany
{


   
    public enum TypeOfEvent { eSports, Football, Basketball, Cricket, Valleyball, MMA, Box, Marathon, Tennis, Hokey };

    class BettingsEvents
    {
       
        public TypeOfEvent BettingEvents { get; set; } = TypeOfEvent.eSports;
        
        public string TeamOne { get; set; }
        
        public string TeamTwo { get; set; }

        public double TeamOneCoefitient { get; set; } = 1;

        public double TeamTwoCoefitient { get; set; } = 1;
        
        public DateTime EventStart { get; set; }
        
        public DateTime EventEnd { get; set; }
    }
}
