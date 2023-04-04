using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.Proxy

//Create a class file with the name Employee.cs and then copy and paste the following code into it.
//As you can see, this is a very simple class having three properties i.e. Username, Password,
//and Role and we are also having one Parameterized constructor to initialize these data members.
{
    public class Customer 
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Customer(string username, string password)        
        {
            Username = username;
            Password = password;
        }
    }
}
