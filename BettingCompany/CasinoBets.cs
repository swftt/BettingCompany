using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BettingCompany
{

     class CasinoBets
    {

        private Dictionary<TypeOfEvent, List<BettingsEvents>> bettingEvents = new Dictionary<TypeOfEvent, List<BettingsEvents>>();
        public VisitorManage manage { get; set; }
        public DateTime currentTime = DateTime.Now;
        public CasinoBets(VisitorManage manage)
        {
            this.manage = manage;
        }

        public CasinoBets()
        {

        }
        public void AddBettingEvents()
        {
            if (manage.LoggedAdmin != null)
            {
                char choice = 'y';
                while (choice == 'y' || choice == 'Y')
                {
                    BettingsEvents bettings = new BettingsEvents();
                    Console.WriteLine("Chose a discipline of the bet:");
                    Console.Write(" 1.eSports\n 2.Football\n 3.Basketball\n 4.Cricket\n 5.Valleyball\n 6.MMA\n 7.Box\n 8.Marathon\n 9.Tennis\n 10.Hokey\n");
                    switch (Int32.Parse(Console.ReadLine()))
                    {
                        case 1:
                            bettings.BettingEvents = TypeOfEvent.eSports;
                            break;
                        case 2:
                            bettings.BettingEvents = TypeOfEvent.Football;
                            break;
                        case 3:
                            bettings.BettingEvents = TypeOfEvent.Basketball;
                            break;
                        case 4:
                            bettings.BettingEvents = TypeOfEvent.Cricket;
                            break;
                        case 5:
                            bettings.BettingEvents = TypeOfEvent.Valleyball;
                            break;
                        case 6:
                            bettings.BettingEvents = TypeOfEvent.MMA;
                            break;
                        case 7:
                            bettings.BettingEvents = TypeOfEvent.Box;
                            break;
                        case 8:
                            bettings.BettingEvents = TypeOfEvent.Marathon;
                            break;
                        case 9:
                            bettings.BettingEvents = TypeOfEvent.Tennis;
                            break;
                        case 10:
                            bettings.BettingEvents = TypeOfEvent.Hokey;
                            break;
                    }
                    Console.Write("Team one name:");
                    bettings.TeamOne = Console.ReadLine();
                    Console.Write("Team two name:");
                    bettings.TeamTwo = Console.ReadLine();
                    bettings.EventStart = DateTime.Now;
                    Console.Write("Enter length of event in minutes:");
                    bettings.EventEnd = bettings.EventStart.AddSeconds(Int32.Parse(Console.ReadLine()));
                    bettings.SetTeamOneCoefitient(0);
                    bettings.SetTeamTwoCoefitient(0);
                    if (bettingEvents.ContainsKey(bettings.BettingEvents))
                    {
                        foreach (var item in bettingEvents)
                        {
                            if (item.Key == bettings.BettingEvents)
                            {
                                item.Value.Add(bettings);
                            }
                            break;
                        }
                    }
                    else
                    {
                        List<BettingsEvents> betEvents = new List<BettingsEvents>();
                        betEvents.Add(bettings);
                        bettingEvents.Add(bettings.BettingEvents, betEvents);
                    }
                    Console.WriteLine("Want to continue adding events? (y/n)");
                    choice = Char.Parse(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("You`re not allowed to manage bets");
            }

        }
        public void WriteEventsToFile()
        {
            BinaryWriter binaryWriter;
            try
            {
                binaryWriter = new BinaryWriter(new FileStream("BetEvents.txt", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant create/open file");
                return;
            }
            try
            {
                foreach (var typeOfEvent in bettingEvents)
                {

                    binaryWriter.Write((Int32)typeOfEvent.Key);
                    binaryWriter.Write(typeOfEvent.Value.Count);
                    foreach (var betEvent in typeOfEvent.Value)
                    {
                        binaryWriter.Write(betEvent.TeamOne);
                        binaryWriter.Write(betEvent.TeamOneCoefitient);
                        binaryWriter.Write(betEvent.TeamOneMonetPlaced);
                        binaryWriter.Write(betEvent.TeamTwo);
                        binaryWriter.Write(betEvent.TeamTwoCoefitient);
                        binaryWriter.Write(betEvent.TeamTwoMoneyPlaced);
                        binaryWriter.Write(betEvent.EventStart.ToString());
                        binaryWriter.Write(betEvent.EventEnd.ToString());
                    }
                }
                bettingEvents.Clear();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant write to the file");
                return;
            }
            binaryWriter.Close();
        }
        public void ReadFromFile()
        {
            try
            {
                using (BinaryReader b = new BinaryReader(
                File.Open("BetEvents.txt", FileMode.Open)))
                {
                    int pos = 0;
                    int length = (int)b.BaseStream.Length;
                    while (pos < length)
                    {
                        List<BettingsEvents> bettings = new List<BettingsEvents>();
                        pos += sizeof(int);
                        TypeOfEvent typeOfEvent = (TypeOfEvent)b.ReadInt32(); 
                        int eventsCount = b.ReadInt32();
                        pos += sizeof(int);
                        for (int i = 0; i < eventsCount; i++)
                        {

                            string teamOne = b.ReadString();
                            double teamOneCoefitient = b.ReadDouble();
                            decimal teamOneMoneyPlaced = b.ReadDecimal();
                            string teamTwo = b.ReadString();
                            double teamTwoCoefitient = b.ReadDouble();
                            decimal teamTwoMoneyPlaced=b.ReadDecimal();
                            string startStr = b.ReadString();
                            string endStr = b.ReadString();

                            DateTime eventStart = DateTime.Parse(startStr);
                            DateTime eventEnd = DateTime.Parse(endStr);

                            BettingsEvents bet = new BettingsEvents
                            {
                                TeamOne = teamOne,
                                TeamTwo = teamTwo,
                                TeamOneCoefitient = teamOneCoefitient,
                                TeamTwoCoefitient = teamTwoCoefitient,
                                TeamOneMonetPlaced = teamOneMoneyPlaced,
                                TeamTwoMoneyPlaced=teamTwoMoneyPlaced,
                                EventStart = eventStart,
                                EventEnd = eventEnd,
                                BettingEvents = typeOfEvent
                            };
                            bettings.Add(bet);
                            pos += teamOne.Length * 2 + sizeof(double)+sizeof(decimal) + teamTwo.Length * 2 + sizeof(double)+sizeof(decimal) + startStr.Length * 2 + endStr.Length * 2;


                        }
                        bettingEvents.Add(typeOfEvent, bettings);
                    }

                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant open file");

            }

        }


        public void PlaceBet()
        {
            if (manage.LoggedUser != null)
            {
                int choice;
                Console.WriteLine("Chose a discipline of the bet:");
                Console.Write(" 1.eSports\n 2.Football\n 3.Basketball\n 4.Cricket\n 5.Valleyball\n 6.MMA\n 7.Box\n 8.Marathon\n 9.Tennis\n 10.Hokey\n");
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:
                        choice = (int)TypeOfEvent.eSports;
                        break;
                    case 2:
                        choice = (int)TypeOfEvent.Football;
                        break;
                    case 3:
                        choice = (int)TypeOfEvent.Basketball;
                        break;
                    case 4:
                        choice = (int)TypeOfEvent.Cricket;
                        break;
                    case 5:
                        choice = (int)TypeOfEvent.Valleyball;
                        break;
                    case 6:
                        choice = (int)TypeOfEvent.MMA;
                        break;
                    case 7:
                        choice = (int)TypeOfEvent.Box;
                        break;
                    case 8:
                        choice = (int)TypeOfEvent.Marathon;
                        break;
                    case 9:
                        choice = (int)TypeOfEvent.Tennis;
                        break;
                    case 10:
                        choice = (int)TypeOfEvent.Hokey;
                        break;
                    default:
                        choice = 0;
                        break;
                }
                Console.Write("Team one name:");
                string TeamOne = Console.ReadLine();
                Console.Write("Team two name:");
                string TeamTwo = Console.ReadLine();
                List<BettingsEvents> tmpEvents = new List<BettingsEvents>();
                foreach (var item in bettingEvents)
                {
                    if ((int)item.Key == choice)
                    {
                        foreach (var betEvent in item.Value)
                        {
                            if (betEvent.TeamOne == TeamOne && betEvent.TeamTwo == TeamTwo)
                            {
                                string bettingChoice = String.Empty;
                                Console.WriteLine("Enter amount of money to bet: ");
                                int betAmount = Int32.Parse(Console.ReadLine());
                                if(manage.LoggedUser.Balance>=betAmount && betAmount>0)
                                {

                                   
                                    do
                                    {
                                       Console.WriteLine("Select team to bet:");
                                       bettingChoice = Console.ReadLine();
                                    } while (bettingChoice!=betEvent.TeamOne || bettingChoice != betEvent.TeamTwo);
                                    if(bettingChoice==betEvent.TeamOne)
                                    {
                                        betEvent.SetTeamOneCoefitient(betAmount);
                                        manage.LoggedUser.BetsPlaced.Add(betEvent.EventStart, betEvent);
                                        foreach (var userBets in manage.LoggedUser.BetsPlaced)
                                        {
                                            if (userBets.Key == betEvent.EventStart)
                                            {
                                                userBets.Value.TeamOne +=betAmount.ToString()+"<";
                                            }
                                        }
                                       
                                    }
                                    else
                                    {
                                        betEvent.SetTeamTwoCoefitient(betAmount);
                                        manage.LoggedUser.BetsPlaced.Add(betEvent.EventStart, betEvent);
                                        foreach (var userBets in manage.LoggedUser.BetsPlaced)
                                        {
                                            if (userBets.Key == betEvent.EventStart)
                                            {
                                                userBets.Value.TeamTwo += betAmount.ToString() + "<";
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                Console.Write("Enter how much money to bet:");
            }
            else
            {
                Console.WriteLine("You have first to sign in to place bets");
            }
        }
        public void PrintBetEvents()
        {
            foreach (var discipline in bettingEvents)
            {
                Console.WriteLine(discipline.Key);
                foreach (var item in discipline.Value)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        static Random randomWinner = new Random();
        public void PrintStatisticsOfBets()
        {
            currentTime = DateTime.Now;
            foreach (var bet in manage.LoggedUser.BetsPlaced)
            {
                if (currentTime > bet.Value.EventEnd)
                {
                    int winnerCounter = 0;
                    int randWinner = randomWinner.Next(0, 1000);
                    double teamOneChance = (double)(bet.Value.TeamOneMonetPlaced / (bet.Value.TeamOneMonetPlaced + bet.Value.TeamTwoMoneyPlaced));
                    Math.Round(teamOneChance, 2);
                    teamOneChance *= 10;
                    if (randWinner <= teamOneChance && bet.Value.TeamOne.Last() == '<')
                    {
                        int counter = 1;
                        int moneyPlaced = 0;
                        int numHelper;
                        int i = bet.Value.TeamOne.Length-1;
                        while(bet.Value.TeamOne[i]!=' ')
                        {
                            if(bet.Value.TeamOne[i]!='<')
                            {
                                numHelper = Int32.Parse(bet.Value.TeamOne[i].ToString());
                                moneyPlaced += numHelper * counter;
                                counter *= 10;
                            }
                        }
                        manage.LoggedUser.Balance += (decimal)(moneyPlaced * bet.Value.TeamOneCoefitient);
                        Win?.Invoke(this, new StatusEventArgs("You have won!"));
                        Console.WriteLine($"Current balance:{manage.LoggedUser.Balance}");
                        winnerCounter++;
                    }
                    else if(randWinner > teamOneChance && bet.Value.TeamTwo.Last() == '<')
                    {
                        int counter = 1;
                        int moneyPlaced = 0;
                        int numHelper;
                        int i = bet.Value.TeamTwo.Length - 1;
                        while (bet.Value.TeamOne[i] != ' ')
                        {
                            if (bet.Value.TeamOne[i] != '<')
                            {
                                numHelper = Int32.Parse(bet.Value.TeamTwo[i].ToString());
                                moneyPlaced += numHelper * counter;
                                counter *= 10;
                            }
                        }
                        manage.LoggedUser.Balance += (decimal)(moneyPlaced * bet.Value.TeamTwoCoefitient);
                        Win?.Invoke(this, new StatusEventArgs("You have won!"));
                        Console.WriteLine($" Current balance:{manage.LoggedUser.Balance}");
                        winnerCounter++;
                    }
                    else if(winnerCounter==0)
                    {
                        if(bet.Value.TeamOne.Last()=='<')
                        {
                            int counter = 1;
                            int moneyPlaced = 0;
                            int numHelper;
                            int i = bet.Value.TeamOne.Length - 1;
                            while (bet.Value.TeamOne[i] != ' ')
                            {
                                if (bet.Value.TeamOne[i] != '<')
                                {
                                    numHelper = Int32.Parse(bet.Value.TeamOne[i].ToString());
                                    moneyPlaced += numHelper * counter;
                                    counter *= 10;
                                }
                            }
                            manage.LoggedUser.Balance += (decimal)(moneyPlaced * bet.Value.TeamOneCoefitient);
                        }
                        else
                        {
                            int counter = 1;
                            int moneyPlaced = 0;
                            int numHelper;
                            int i = bet.Value.TeamTwo.Length - 1;
                            while (bet.Value.TeamOne[i] != ' ')
                            {
                                if (bet.Value.TeamOne[i] != '<')
                                {
                                    numHelper = Int32.Parse(bet.Value.TeamTwo[i].ToString());
                                    moneyPlaced += numHelper * counter;
                                    counter *= 10;
                                }
                            }
                            manage.LoggedUser.Balance += (decimal)(moneyPlaced * bet.Value.TeamTwoCoefitient);
                        }
                        Lose?.Invoke(this, new StatusEventArgs("You have lost!"));
                        Console.WriteLine($" Current balance:{manage.LoggedUser.Balance}");

                    }
                    
                }
            }
        }

        public delegate void MyUserBets(object sender, StatusEventArgs args);
        public event MyUserBets Win;
        public event MyUserBets Lose;
    }
}

