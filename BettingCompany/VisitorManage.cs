using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
     class VisitorManage
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private Dictionary<string, Admin> admins = new Dictionary<string, Admin>();
        private User loggedAccoutUser;
        private bool isUserSigned = false;
        private bool isAdminSigned = false;
        private Admin loggedAccountAdmin;
        public User LoggedUser
        { get
            {
                return loggedAccoutUser;
            }
          set
            {
               if(value is User)
                {
                    loggedAccoutUser = value;
                }
            }
                }
        public Admin LoggedAdmin {
            get
            {
                return loggedAccountAdmin;
            }
            set
            {
                if(value is Admin)
                {
                    loggedAccountAdmin = value;
                }
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
                    Console.Write("Enter email:");
                    email = Console.ReadLine();


                    User user = new User(name, surname, country, login, password, email, phoneNumber, age);
                    if ((!users.ContainsKey(email)) && (!admins.ContainsKey(email)))
                    {
                        users.Add(user.Email, user);
                    }
                    else
                    {

                        do
                        {
                            Console.WriteLine("Cant register user with same email is already exists");
                            Console.Write("Enter your email:");
                            email = Console.ReadLine();
                        } while (users.ContainsKey(email) || admins.ContainsKey(email));
                        user.Email = email;
                        users.Add(user.Email, user);

                    }


                }
            }
            catch (UserRegistrationException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void RegisterAdmin()
        {
            int age;
            string name;
            string surname;
            string country;
            string login;
            string password;
            string email;
            string phoneNumber;
            Console.Write("To register a new admin you have to enter codeword:");
            string adminCode = String.Empty;
            adminCode = Console.ReadLine();
            if (adminCode == Admin.adminCode)
            {
                try
                {
                    Console.Write("Enter age(You have to be over 18 years to register on site):");
                    age = Int32.Parse(Console.ReadLine());
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
                        Console.Write("Enter email:");
                        email = Console.ReadLine();
                        Admin admin = new Admin(name, surname, country, login, password, email, phoneNumber, age);
                        if ((!users.ContainsKey(email)) && (!admins.ContainsKey(email)))
                        {
                            admins.Add(email, admin);
                        }
                        else
                        {


                            do
                            {
                                Console.WriteLine("Cant register user with same email is already exists");
                                Console.Write("Enter your email:");
                                email = Console.ReadLine();
                            } while (admins.ContainsKey(email) || users.ContainsKey(email));
                            admin.Email = email;
                            admins.Add(email, admin);

                        }

                    }
                }
                catch (UserRegistrationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("You entered invalid admin code,please register later or contact with other admins");
            }
        }
        public void WriteAdminToFile()
        {

            BinaryWriter binaryWriter;
            try
            {
                binaryWriter = new BinaryWriter(new FileStream("AdminData.txt", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant create/open file");
                return;
            }
            try
            {
                foreach (var admin in admins)
                {


                    binaryWriter.Write(admin.Key);
                    binaryWriter.Write(admin.Value.Age);
                    binaryWriter.Write(admin.Value.Name);
                    binaryWriter.Write(admin.Value.Surname);
                    binaryWriter.Write(admin.Value.Login);
                    binaryWriter.Write(admin.Value.Password);
                    binaryWriter.Write(admin.Value.Country);
                    binaryWriter.Write(admin.Value.PhoneNumber);




                }
                admins.Clear();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant write to the file");
                return;
            }
            binaryWriter.Close();

        }
        public void WriteUsersToFile()
        {

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
                foreach (var user in users)
                {

                    binaryWriter.Write(user.Key);
                    binaryWriter.Write(user.Value.Age);
                    binaryWriter.Write(user.Value.Name);
                    binaryWriter.Write(user.Value.Surname);
                    binaryWriter.Write(user.Value.Login);
                    binaryWriter.Write(user.Value.Password);
                    binaryWriter.Write(user.Value.Country);
                    binaryWriter.Write(user.Value.PhoneNumber);
                    binaryWriter.Write(user.Value.Balance);
                    binaryWriter.Write(user.Value.Valute);
                    binaryWriter.Write(user.Value.BetsPlaced.Count);  //amount of placed bets 
                    foreach (var bets in user.Value.BetsPlaced)
                    {
                        binaryWriter.Write(bets.Key.ToString());
                        binaryWriter.Write((Int32)bets.Value.BettingEvents);
                        binaryWriter.Write(bets.Value.TeamOne);
                        binaryWriter.Write(bets.Value.TeamOneCoefitient);
                        binaryWriter.Write(bets.Value.TeamOneMonetPlaced);
                        binaryWriter.Write(bets.Value.TeamTwo);
                        binaryWriter.Write(bets.Value.TeamTwoCoefitient);
                        binaryWriter.Write(bets.Value.TeamTwoMoneyPlaced);
                        binaryWriter.Write(bets.Value.EventEnd.ToString());
                                                   

                    }
                    {

                    }




                }
                users.Clear();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant write to the file");
                return;
            }
            binaryWriter.Close();
        }
        public void DeleteUserFromData()
        {
            if(loggedAccountAdmin!=null)
            {
                string email;
              
                    do
                    {
                        Console.WriteLine("Enter email to delete user:");
                        email = Console.ReadLine();
                    } while (!users.ContainsKey(email));

                users.Remove(email);
            }
        }
        public void ReadUsersFromFile()
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
                        Dictionary<DateTime, BettingsEvents> betsPlacedUser = new Dictionary<DateTime, BettingsEvents>();
                        string email = b.ReadString();
                        int age = b.ReadInt32();
                        string name = b.ReadString();
                        string surname = b.ReadString();
                        string login = b.ReadString();
                        string password = b.ReadString();
                        string country = b.ReadString();
                        string phoneNumber = b.ReadString();
                        decimal balance = b.ReadDecimal();
                        string valute = b.ReadString();
                        int betsPlaced = b.ReadInt32();
                        for (int i = 0; i < betsPlaced; i++)
                        {
                            string eventStartStr = b.ReadString();
                            DateTime eventStart = DateTime.Parse(eventStartStr);
                            TypeOfEvent typeOfEvent =(TypeOfEvent)b.ReadInt32();
                            string teamOne = b.ReadString();
                            double teamOneCoefitient = b.ReadDouble();
                            decimal teamOneMonetPlaced = b.ReadDecimal();
                            string teamTwo = b.ReadString();
                            double teamTwoCoefitient = b.ReadDouble();
                            decimal teamTwoMoneyPlaced = b.ReadDecimal();
                            string eventEndStr = b.ReadString();
                            DateTime eventEnd=DateTime.Parse(eventEndStr);

                            BettingsEvents bettings = new BettingsEvents { TeamOne = teamOne, TeamOneMonetPlaced = teamOneMonetPlaced, TeamOneCoefitient = teamOneCoefitient, TeamTwo = teamTwo, TeamTwoCoefitient = teamTwoCoefitient, TeamTwoMoneyPlaced = teamTwoMoneyPlaced,EventStart=eventStart,EventEnd=eventEnd, };
                            betsPlacedUser.Add(eventStart, bettings);
                            pos += eventStartStr.Length * 2 + eventEndStr.Length * 2 + sizeof(int) + teamOne.Length * 2 + sizeof(double) * 2 + sizeof(decimal) * 2 + teamTwo.Length * 2;
                        }
                        User user = new User(name, surname, country, login, password, email, phoneNumber, age, balance, valute,betsPlacedUser);
                        betsPlacedUser.Clear();
                        users.Add(user.Email, user);
                        pos += sizeof(int) + email.Length * 2 + name.Length * 2 + surname.Length * 2 + login.Length * 2 + password.Length * 2 + country.Length * 2 + phoneNumber.Length * 2 + sizeof(decimal) + valute.Length * 2;

                    }



                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant open file");

            }
        }
        public void ReadAdminsFromFile()
        {
            try
            {
                using (BinaryReader b = new BinaryReader(
                File.Open("AdminData.txt", FileMode.Open)))
                {
                    int pos = 0;
                    int length = (int)b.BaseStream.Length;
                    while (pos < length)
                    {
                        string email = b.ReadString();
                        int age = b.ReadInt32();
                        string name = b.ReadString();
                        string surname = b.ReadString();
                        string login = b.ReadString();
                        string password = b.ReadString();
                        string country = b.ReadString();
                        string phoneNumber = b.ReadString();
                        Admin admin = new Admin(name, surname, country, login, password, email, phoneNumber, age);
                        admins.Add(admin.Email, admin);
                        pos += sizeof(int) + email.Length * 2 + name.Length * 2 + surname.Length * 2 + login.Length * 2 + password.Length * 2 + country.Length * 2 + phoneNumber.Length * 2;

                    }



                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant open file");

            }
        }
        public void LogOutFromUserAccount()
        {
            loggedAccoutUser = null;
            if(File.Exists("SignedUserData.txt"))
            {
                File.Delete("SignedUserData.txt");
            }
        }
        public void LogOutFromAdminAccount()
        {
            loggedAccountAdmin = null;
            if (File.Exists("SignedAdminData.txt"))
            {
                File.Delete("SignedAdminData.txt");
            }
        }

        public void LoginToAccount()
        {
            string email = String.Empty;
            string password = String.Empty;
            Console.Write("Enter your email:");
            email = Console.ReadLine();
            
            if (users.ContainsKey(email) || admins.ContainsKey(email))
            {
                Console.Write("Enter your password:");
                password = Console.ReadLine();
                bool found = false;
                foreach (var user in users)
                {
                    if (user.Value.Password == password)
                    {
                        loggedAccoutUser = user.Value;
                        isUserSigned = true;
                        found = true;
                    }


                }
                if (!found)
                {
                    foreach (var admin in admins)
                    {
                        if (admin.Value.Password == password)
                        {
                            loggedAccountAdmin = admin.Value;
                            isAdminSigned = true;
                            found = true;
                        }


                    }
                }
            }


        }
        public void AddBalance(decimal additionalBalance, string valute = "GRN")
        {
            if (loggedAccoutUser != null)
            {
                if (valute == "DOL")
                {
                    additionalBalance *= 28;
                }
                if (valute == "EUR")
                {
                    additionalBalance *= 30;
                }
                try
                {
                    if (loggedAccoutUser.Balance == 0 && additionalBalance < 500)
                    {
                        FirstDepositeException ex = new FirstDepositeException(string.Format($"Too small first deposit,you have to make first deposit more then 500 {loggedAccoutUser.Valute}"));
                        throw ex;
                    }
                    else
                    {
                        loggedAccoutUser.Balance += additionalBalance;
                        Console.WriteLine($"Congratulations,you`re successfully made a  deposit for {additionalBalance} {loggedAccoutUser.Valute} now you balance is  {loggedAccoutUser.Balance} {loggedAccoutUser.Valute}!");
                    }


                }
                catch (FirstDepositeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("You have to login first");
            }
        }
        public void WriteLoggedUserToFile()
        {
            BinaryWriter binaryWriter;
            try
            {
                binaryWriter = new BinaryWriter(new FileStream("SignedUserData.txt", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant create/open file");
                return;
            }
            try
            {

                binaryWriter.Write(loggedAccoutUser.Age);
                binaryWriter.Write(loggedAccoutUser.Name);
                binaryWriter.Write(loggedAccoutUser.Surname);
                binaryWriter.Write(loggedAccoutUser.Login);
                binaryWriter.Write(loggedAccoutUser.Password);
                binaryWriter.Write(loggedAccoutUser.Country);
                binaryWriter.Write(loggedAccoutUser.PhoneNumber);
                binaryWriter.Write(loggedAccoutUser.Balance);
                binaryWriter.Write(loggedAccoutUser.Valute);
                loggedAccoutUser = null;

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant write to the file");
                return;
            }
            binaryWriter.Close();
            if(users!=null)
            {
                users.Remove(LoggedUser.Email);
                users.Add(LoggedUser.Email,LoggedUser);
            }
            else
            {
                ReadUsersFromFile();
                users.Remove(LoggedUser.Email);
                users.Add(LoggedUser.Email, LoggedUser);

            }
            
        }
        
        public void WriteLoggedAdminToFile()
        {
            BinaryWriter binaryWriter;
            try
            {
                binaryWriter = new BinaryWriter(new FileStream("SignedAdminData.txt", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant create/open file");
                return;
            }
            try
            {

                binaryWriter.Write(loggedAccountAdmin.Age);
                binaryWriter.Write(loggedAccountAdmin.Name);
                binaryWriter.Write(loggedAccountAdmin.Surname);
                binaryWriter.Write(loggedAccountAdmin.Login);
                binaryWriter.Write(loggedAccountAdmin.Password);
                binaryWriter.Write(loggedAccountAdmin.Country);
                binaryWriter.Write(loggedAccountAdmin.PhoneNumber);
       
                loggedAccountAdmin = null;

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant write to the file");
                return;
            }
            binaryWriter.Close();
        }
        public void ReadLoggedUserFromFile()
        {
            try
            {
                using (BinaryReader b = new BinaryReader(
                File.Open("SignedUserData.txt", FileMode.Open)))
                {
                    int pos = 0;
                    int length = (int)b.BaseStream.Length;
                    while (pos < length)
                    {
                        string email = b.ReadString();
                        int age = b.ReadInt32();
                        string name = b.ReadString();
                        string surname = b.ReadString();
                        string login = b.ReadString();
                        string password = b.ReadString();
                        string country = b.ReadString();
                        string phoneNumber = b.ReadString();
                        decimal balance = b.ReadDecimal();
                        string valute = b.ReadString();
                         loggedAccoutUser = new User(name, surname, country, login, password, email, phoneNumber, age, balance, valute);
                        pos += sizeof(int) + email.Length * 2 + name.Length * 2 + surname.Length * 2 + login.Length * 2 + password.Length * 2 + country.Length * 2 + phoneNumber.Length * 2 + sizeof(decimal) + valute.Length * 2;

                    }



                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant open file");

            }
        }
        public void ReadLoggedAdminFromFile()
        {
            try
            {
                using (BinaryReader b = new BinaryReader(
                File.Open("SignedAdminData.txt", FileMode.Open)))
                {
                    int pos = 0;
                    int length = (int)b.BaseStream.Length;
                    while (pos < length)
                    {
                        string email = b.ReadString();
                        int age = b.ReadInt32();
                        string name = b.ReadString();
                        string surname = b.ReadString();
                        string login = b.ReadString();
                        string password = b.ReadString();
                        string country = b.ReadString();
                        string phoneNumber = b.ReadString();
                        loggedAccountAdmin = new Admin(name, surname, country, login, password, email, phoneNumber, age);
                        pos += sizeof(int) + email.Length * 2 + name.Length * 2 + surname.Length * 2 + login.Length * 2 + password.Length * 2 + country.Length * 2 + phoneNumber.Length * 2;

                    }



                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + " Cant open file");

            }
        }
        public void WithdrawMoney(decimal withdrawAmount, string valute = "GRN")
        {
            if (loggedAccoutUser != null)
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
                    if (loggedAccoutUser.Balance == 0)
                    {
                        FirstDepositeException ex = new FirstDepositeException(string.Format($"You have no money on your account,you cant do a withdraw!"));
                        throw ex;
                    }
                    else if ((loggedAccoutUser.Balance - withdrawAmount) < 0)
                    {
                        FirstDepositeException ex = new FirstDepositeException(string.Format($"The withdraw amount is too big,you cant do a withdraw!"));
                        throw ex;
                    }
                    else
                    {
                        loggedAccoutUser.Balance -= withdrawAmount;
                        Console.WriteLine($"Congratulations,you`re successfully withdrawal {withdrawAmount} {loggedAccoutUser.Valute} now you balance is  {loggedAccoutUser.Balance} {loggedAccoutUser.Valute}!");
                    }


                }
                catch (FirstDepositeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("To withdraw money,you have to login first");
            }

        }
        
    }
}
