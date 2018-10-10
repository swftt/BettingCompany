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
        private Dictionary<string, Human> usersAdmins = new Dictionary<string, Human>();
        private Human loggedAccount;
        public void AddBettingEvents()
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
                bettings.EventEnd = bettings.EventStart.AddMinutes(Int32.Parse(Console.ReadLine()));

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
                        binaryWriter.Write(betEvent.TeamTwo);
                        binaryWriter.Write(betEvent.TeamTwoCoefitient);
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
                        TypeOfEvent typeOfEvent = (TypeOfEvent)b.ReadInt32(); //(TypeOfEvent)Enum.Parse(typeof(TypeOfEvent), b.ReadString());
                        int eventsCount = b.ReadInt32();
                        pos += sizeof(int);
                        for (int i = 0; i < eventsCount; i++)
                        {

                            string teamOne = b.ReadString();
                            double teamOneCoefitient = b.ReadDouble();
                            string teamTwo = b.ReadString();
                            double teamTwoCoefitient = b.ReadDouble();
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
                                EventStart = eventStart,
                                EventEnd = eventEnd,
                                BettingEvents = typeOfEvent
                            };
                            bettings.Add(bet);
                            pos += teamOne.Length * 2 + sizeof(double) + teamTwo.Length * 2 + sizeof(double) + startStr.Length * 2 + endStr.Length * 2;


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
        public void RegisterUser()
        {
            int age;
            string name;
            string surname;
            string country;
            string login;
            string password;
            string email;
            string phoneNumber;
            Console.WriteLine();
            Console.Write("Enter age(You have to be over 18 years to register on site):");
            age = Int32.Parse(Console.ReadLine());
            try
            {

                if (age < 18)
                {
                    UserRegistrationException ex = new UserRegistrationException(string.Format($"Persons under 18 years cant register"));
                    throw ex;
                }
                else
                {

                   
                    Console.Write("Enter name:");
                    name = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write("Enter surname:");
                    surname = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write("Enter country:");
                    country = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write("Enter login:");
                    login = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write("Enter password:");
                    password = Console.ReadLine();


                    Console.WriteLine();
                    Console.Write("Enter phone number:");
                    phoneNumber = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Enter email:");
                    email = Console.ReadLine();
                    Console.Write("Register user or admin?(1/0)");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Human user = new User(name, surname, country, login, password, email, phoneNumber, age);
                        if (!usersAdmins.ContainsKey(email))
                        {
                            usersAdmins.Add(user.Email, user);
                        }
                        else
                        {

                            do
                            {
                                Console.WriteLine("Cant register user with same email is already exists");
                                Console.Write("Enter your email:");
                                email = Console.ReadLine();
                            } while (usersAdmins.ContainsKey(email));
                            user.Email = email;
                            usersAdmins.Add(user.Email, user);

                        }
                    }
                    else if(choice=="0")
                    {
                        Console.Write("To register a new admin you have to enter codeword:");
                        string adminCode = String.Empty;
                        adminCode = Console.ReadLine();
                        if ( adminCode == Admin.adminCode)
                        {
                            Human admin = new Admin(name, surname, country, login, password, email, phoneNumber, age);
                            if (!usersAdmins.ContainsKey(email))
                            {
                                usersAdmins.Add(email, admin);
                            }
                            else
                            {

                                do
                                {
                                    Console.WriteLine("Cant register user with same email is already exists");
                                    Console.Write("Enter your email:");
                                    email = Console.ReadLine();
                                } while (usersAdmins.ContainsKey(email));
                                admin.Email = email;
                                usersAdmins.Add(email, admin);

                            }
                        }
                        else
                        {
                            Console.WriteLine("You entered invalid admin code,please register later or contact with other admins");
                        }


                    }
                }
            }
            catch (UserRegistrationException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void WriteusersAdminsToDataBase()
        {
            string adm = "admin";
            string usr = "user";
            BinaryWriter binaryWriter;
            try
            {
                binaryWriter = new BinaryWriter(new FileStream("UserData.txt", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant create/open file");
                return;
            }
            try
            {
                foreach (var human in usersAdmins)
                {
                    if (human.Value is User)
                    {
                        binaryWriter.Write(usr);
                        binaryWriter.Write(human.Key);
                        binaryWriter.Write((human.Value as User).Age);
                        binaryWriter.Write((human.Value as User).Name);
                        binaryWriter.Write((human.Value as User).Surname);
                        binaryWriter.Write((human.Value as User).Login);
                        binaryWriter.Write((human.Value as User).Password);
                        binaryWriter.Write((human.Value as User).Country);
                        binaryWriter.Write((human.Value as User).PhoneNumber);
                        binaryWriter.Write((human.Value as User).Balance);
                        binaryWriter.Write((human.Value as User).Valute);
                    }
                    else
                    {
                        binaryWriter.Write(adm);
                        binaryWriter.Write(human.Key);
                        binaryWriter.Write((human.Value as Admin).Age);
                        binaryWriter.Write((human.Value as Admin).Name);
                        binaryWriter.Write((human.Value as Admin).Surname);
                        binaryWriter.Write((human.Value as Admin).Login);
                        binaryWriter.Write((human.Value as Admin).Password);
                        binaryWriter.Write((human.Value as Admin).Country);
                        binaryWriter.Write((human.Value as Admin).PhoneNumber);
                    }

                }
                usersAdmins.Clear();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant write to the file");
                return;
            }
            binaryWriter.Close();
        }
        public void ReadusersAdminsFromDataBase()
        {
            try
            {
                using (BinaryReader b = new BinaryReader(
                File.Open("UserData.txt", FileMode.Open)))
                {
                    int pos = 0;
                    int length = (int)b.BaseStream.Length;
                    while (pos < length)
                    {
                        string userAdminChoice = b.ReadString();
                        string email = b.ReadString();
                        int age = b.ReadInt32();
                        string name = b.ReadString();
                        string surname = b.ReadString();
                        string login = b.ReadString();
                        string password = b.ReadString();
                        string country = b.ReadString();
                        string phoneNumber = b.ReadString();
                        if (userAdminChoice == "user")
                        {
                            decimal balance = b.ReadDecimal();
                            string valute = b.ReadString();
                            Human user = new User(name, surname, country, login, password, email, phoneNumber, age, balance, valute);
                            usersAdmins.Add(user.Email, user);
                            pos += userAdminChoice.Length * 2 + sizeof(int) + email.Length * 2 + name.Length * 2 + surname.Length * 2 + login.Length * 2 + password.Length * 2 + country.Length * 2 + phoneNumber.Length * 2 + sizeof(decimal) + valute.Length * 2;
                        }
                        else
                        {
                            Human admin = new User(name, surname, country, login, password, email, phoneNumber, age);
                            usersAdmins.Add(admin.Email, admin);
                            pos += userAdminChoice.Length * 2 + sizeof(int) + email.Length * 2 + name.Length * 2 + surname.Length * 2 + login.Length * 2 + password.Length * 2 + country.Length * 2 + phoneNumber.Length * 2;
                        }

                        { }
                    }



                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant open file");

            }
        }
        public void LoginToAccount()
        {
            string email = String.Empty;
            string password = String.Empty;
            Console.Write("Enter your email:");
            email = Console.ReadLine();
            if (usersAdmins.ContainsKey(email))
            {
                Console.Write("Enter your password:");
                password = Console.ReadLine();
                foreach (var userAdmin in usersAdmins)
                {
                    if(userAdmin.Value.Password.CompareTo(password)==0)
                    {
                        if(userAdmin.Value is User)
                        {
                            loggedAccount = userAdmin.Value;
                        }
                    }
                    else
                    {
                        loggedAccount = userAdmin.Value;
                    }
                }
            }
           
            
        }
        //public void AddBalance(decimal additionalBalance,string valute = "GRN")
        //{
        //    if (user is User)
        //    {
        //        if (valute == "DOL")
        //        {
        //            additionalBalance *= 28;
        //        }
        //        if (valute == "EUR")
        //        {
        //            additionalBalance *= 30;
        //        }
        //        try
        //        {
        //            if (additionalBalance < 500)
        //            {
        //                FirstDepositeException ex = new FirstDepositeException(string.Format($"Too small first deposit,you have to make first deposit more then 500 {(user as User).Valute}"));
        //                throw ex;
        //            }
        //            else
        //            {
        //                (user as User).Balance += additionalBalance;
        //                Console.WriteLine($"Congratulations,you`re successfully made a  deposit for {additionalBalance} {(user as User).Valute} now you balance is  {(user as User).Balance} {(user as User).Valute}!");
        //            }


        //        }
        //        catch (FirstDepositeException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}
        public void WithdrawMoney(object user, decimal withdrawAmount, string valute = "GRN")
        {
            if (user is User)
            {
                if (valute == "DOL")
                {
                    withdrawAmount *= 28;
                }
                if (valute == "EUR")
                {
                    withdrawAmount *= 30;
                }
                try
                {
                    if ((user as User).Balance == 0)
                    {
                        FirstDepositeException ex = new FirstDepositeException(string.Format($"You have no money on your account,you cant do a withdraw!"));
                        throw ex;
                    }
                    else if (((user as User).Balance - withdrawAmount) < 0)
                    {
                        FirstDepositeException ex = new FirstDepositeException(string.Format($"The withdraw amount is too big,you cant do a withdraw!"));
                        throw ex;
                    }
                    else
                    {
                        (user as User).Balance -= withdrawAmount;
                        Console.WriteLine($"Congratulations,you`re successfully withdrawal {withdrawAmount} {(user as User).Valute} now you balance is  {(user as User).Balance} {(user as User).Valute}!");
                    }


                }
                catch (FirstDepositeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }






    }
}
