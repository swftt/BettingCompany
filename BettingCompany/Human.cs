using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
     public abstract class Human
     {
        public string Name { get;}
        public string Surname { get;}
        public int Age { get;} = 18;
        public string Country { get; }
        public string Password { get; }
        public string Login { get ;}
        public string Email { get; set; }
        public string PhoneNumber { get; }
        public Human(string name,string surname,string country,int age,string password,string login,string email,string phoneNumber)
        {
            
            Name = name;
            Surname = surname;
            Country = country;
            Age = age;
            Password = password;
            Login = login;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        
        public Human()
        {

        }
        public override string ToString()
        {
            return $"{Name} {Surname} {PhoneNumber} {Country} {Age} {Login} {Password} {Email} ";
        }
       

    }
}
