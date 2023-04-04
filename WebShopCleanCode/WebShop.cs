using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.State;

namespace WebShopCleanCode
{
    public class WebShop//hej
    {
        
        Database database = new Database();
        List<Product> products = new List<Product>();
        List<Customer> customers = new List<Customer>();

        public Istate state;
        string currentMenu = "main menu";
        int currentChoice = 1;
        int amountOfOptions = 3;
        string option1 = "See Wares";
        string option2 = "Customer Info";
        string option3 = "Login";
        string option4 = "";
        string info = "What would you like to do?";

        string username = null;
        string password = null;
        Customer currentCustomer;
        //Dessa tillhör RegisterNewCustomer:
        string newPassword, firstName, lastName, email, address, phoneNumber;
        int age;

        public WebShop()
        {
            products = database.GetProducts();
            customers = database.GetCustomers();
            state = new LoggedOut();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the WebShop!");
            while (true)
            {
                Console.WriteLine(info);

                if (currentMenu.Equals("purchase menu"))
                {
                    SeeAllWaresMenu();
                }
                else
                {
                    PrintOptions();
                }

                ToggleButtons();

                CheckIfLoggedIn();

                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "left":
                    case "l":
                        ToggleLeft();
                        break;
                    case "right":
                    case "r":
                        ToggleRight();
                        break;
                    case "ok":
                    case "k":
                    case "o":
                        ToggleOk(choice);
                        break;
                    case "back":
                    case "b":
                        ToggleBack();
                        break;
                    case "quit":
                    case "q":
                        Quit();
                        return;
                    default:
                        Console.WriteLine("That is not an applicable option.");
                        break;
                }
            }
        }

        private void CheckIfLoggedIn()
        {
            if (currentCustomer != null)
            {
                Console.WriteLine("Current user: " + currentCustomer.Username);
            }
            else
            {
                Console.WriteLine("Nobody logged in.");
            }
        }

        private static void Quit()
        {
            Console.WriteLine("The console powers down. You are free to leave.");
            Environment.Exit(0);
        }

        private void ToggleBack()
        {
            if (currentMenu.Equals("main menu"))
            {
                Console.WriteLine("\nYou're already on the main menu.\n");
            }
            else if (currentMenu.Equals("purchase menu"))
            {
                option1 = "See all wares";
                option2 = "Purchase a ware";
                option3 = "Sort wares";
                if (currentCustomer == null)
                {
                    option4 = "Login";
                }
                else
                {
                    option4 = "Logout";
                }
                amountOfOptions = 4;
                currentChoice = 1;
                currentMenu = "wares menu";
                info = "What would you like to do?";
            }
            else
            {
                option1 = "See Wares";
                option2 = "Customer Info";
                if (currentCustomer == null)
                {
                    option3 = "Login";
                }
                else
                {
                    option3 = "Logout";
                }
                info = "What would you like to do?";
                currentMenu = "main menu";
                currentChoice = 1;
                amountOfOptions = 3;
            }
        }

        private string ToggleOk(string choice)
        {
            if (currentMenu.Equals("main menu"))
            {
                switch (currentChoice)
                {
                    case 1:
                        option1 = "See all wares";
                        option2 = "Purchase a ware";
                        option3 = "Sort wares";
                        if (currentCustomer == null)
                        {
                            option4 = "Login";
                        }
                        else
                        {
                            option4 = "Logout";
                        }
                        amountOfOptions = 4;
                        currentChoice = 1;
                        currentMenu = "wares menu";
                        info = "What would you like to do?";
                        break;
                    case 2:
                        if (currentCustomer != null)
                        {
                            option1 = "See your orders";
                            option2 = "Set your info";
                            option3 = "Add funds";
                            option4 = "";
                            amountOfOptions = 3;
                            currentChoice = 1;
                            info = "What would you like to do?";
                            currentMenu = "customer menu";
                        }
                        else
                        {
                            Console.WriteLine("\nNobody is logged in.\n");
                        }
                        break;
                    case 3:
                        if (currentCustomer == null)
                        {
                            option1 = "Set Username";
                            option2 = "Set Password";
                            option3 = "Login";
                            option4 = "Register";
                            amountOfOptions = 4;
                            currentChoice = 1;
                            info = "Please submit username and password.";
                            username = null;
                            password = null;
                            currentMenu = "login menu";
                        }
                        else
                        {
                            option3 = "Login";
                            Console.WriteLine($"\n{currentCustomer.Username} logged out.");
                            currentChoice = 1;
                            currentCustomer = null;
                        }
                        break;
                    default:
                        Console.WriteLine("\nNot an option.\n");
                        break;
                }
            }
            else if (currentMenu.Equals("customer menu"))
            {
                switch (currentChoice)
                {
                    case 1:
                        currentCustomer.PrintOrders();
                        break;
                    case 2:
                        currentCustomer.PrintInfo();
                        break;
                    case 3:
                        Console.WriteLine("How many funds would you like to add?");
                        string amountString = Console.ReadLine();
                        try
                        {
                            int amount = int.Parse(amountString);
                            if (amount < 0)
                            {
                                Console.WriteLine("\nDon't add negative amounts.\n");
                            }
                            else
                            {
                                currentCustomer.Funds += amount;
                                Console.WriteLine($"\n{amount} added to your profile.\n");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("\nPlease write a number next time.\n");
                        }
                        break;
                    default:
                        Console.WriteLine("\nNot an option.\n");
                        break;
                }
            }
            else if (currentMenu.Equals("sort menu"))
            {
                bool back = true;
                switch (currentChoice)
                {
                    case 1:
                        bubbleSort("name", false);
                        Console.WriteLine("\nWares sorted.\n");
                        break;
                    case 2:
                        bubbleSort("name", true);
                        Console.WriteLine("\nWares sorted.\n");
                        break;
                    case 3:
                        bubbleSort("price", false);
                        Console.WriteLine("\nWares sorted.\n");
                        break;
                    case 4:
                        bubbleSort("price", true);
                        Console.WriteLine("\nWares sorted.\n");
                        break;
                    default:
                        back = false;
                        Console.WriteLine("\nNot an option.\n");
                        break;
                }
                if (back)
                {
                    option1 = "See all wares";
                    option2 = "Purchase a ware";
                    option3 = "Sort wares";
                    if (currentCustomer == null)
                    {
                        option4 = "Login";
                    }
                    else
                    {
                        option4 = "Logout";
                    }
                    amountOfOptions = 4;
                    currentChoice = 1;
                    currentMenu = "wares menu";
                    info = "What would you like to do?";
                }
            }
            else if (currentMenu.Equals("wares menu"))
            {
                switch (currentChoice)
                {
                    case 1:
                        Console.WriteLine();
                        foreach (Product product in products)
                        {
                            product.PrintInfo();
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        if (currentCustomer != null)
                        {
                            currentMenu = "purchase menu";
                            info = "What would you like to purchase?";
                            currentChoice = 1;
                            amountOfOptions = products.Count;
                        }
                        else
                        {
                            Console.WriteLine("\nYou must be logged in to purchase wares.\n");
                            currentChoice = 1;
                        }
                        break;
                    case 3:
                        option1 = "Sort by name, descending";
                        option2 = "Sort by name, ascending";
                        option3 = "Sort by price, descending";
                        option4 = "Sort by price, ascending";
                        info = "How would you like to sort them?";
                        currentMenu = "sort menu";
                        currentChoice = 1;
                        amountOfOptions = 4;
                        break;
                    case 4:
                        if (currentCustomer == null)
                        {
                            option1 = "Set Username";
                            option2 = "Set Password";
                            option3 = "Login";
                            option4 = "Register";
                            amountOfOptions = 4;
                            info = "Please submit username and password.";
                            currentChoice = 1;
                            currentMenu = "login menu";
                        }
                        else
                        {
                            option4 = "Login";
                            Console.WriteLine($"\n{currentCustomer.Username} + logged out.\n");
                            currentCustomer = null;
                            currentChoice = 1;
                        }
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("\nNot an option.\n");
                        break;
                }
            }
            //hela denna går att göra en metod av:
            else if (currentMenu.Equals("login menu"))
            {
                switch (currentChoice)
                {
                    case 1:
                        Console.WriteLine("A keyboard appears.");
                        Console.WriteLine("Please input your username.");
                        username = Console.ReadLine();
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine("A keyboard appears.");
                        Console.WriteLine("Please input your password.");
                        password = Console.ReadLine();
                        Console.WriteLine();
                        break;
                    case 3:
                        if (username == null || password == null)
                        {
                            Console.WriteLine("\nIncomplete data.\n");
                        }
                        else
                        {
                            bool found = false;
                            CanLogIn(found);
                            if (found == false)
                            {
                                Console.WriteLine("\nInvalid credentials.\n");
                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("Please write your username.");
                        string newUsername = Console.ReadLine();
                        foreach (Customer customer in customers)
                        {
                            if (customer.Username.Equals(username))
                            {
                                Console.WriteLine("\nUsername already exists.\n");
                                break;
                            }
                        }
                        // Would have liked to be able to quit at any time in here.

                        RegisterNewCustomer(out choice, out newPassword, out firstName, out lastName, out email, out age, out address, out phoneNumber);

                        AddNewCustomer(newUsername);
                        option1 = "See Wares";
                        option2 = "Customer Info";
                        if (currentCustomer == null)
                        {
                            option3 = "Login";
                        }
                        else
                        {
                            option3 = "Logout";
                        }
                        info = "What would you like to do?";
                        currentMenu = "main menu";
                        currentChoice = 1;
                        amountOfOptions = 3;
                        break;
                    default:
                        Console.WriteLine("\nNot an option.\n");
                        break;
                }
            }
            else if (currentMenu.Equals("purchase menu"))
            {
                int index = currentChoice - 1;
                Product product = products[index];
                if (product.InStock())
                {
                    if (currentCustomer.CanAfford(product.Price))
                    {
                        currentCustomer.Funds -= product.Price;
                        product.NrInStock--;
                        currentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
                        Console.WriteLine($"\nSuccessfully bought {product.Name}\n");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nYou cannot afford.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nNot in stock.\n");
                }
            }

            return choice;
        }

        private void AddNewCustomer(string newUsername)
        {
            Customer newCustomer = new Customer(newUsername, newPassword, firstName, lastName, email, age, address, phoneNumber);
            customers.Add(newCustomer);
            currentCustomer = newCustomer;
            Console.WriteLine($"\n{newCustomer.Username} successfully added and is now logged in.\n");
        }

        private static void RegisterNewCustomer(out string choice, out string newPassword, out string firstName, out string lastName, out string email, out int age, out string address, out string phoneNumber)
        {
            choice = "";
            bool next = false;
            newPassword = null;
            firstName = null;
            lastName = null;
            email = null;
            age = -1;
            address = null;
            phoneNumber = null;
            while (true)
            {
                Console.WriteLine("Do you want a password? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your password.");
                        newPassword = Console.ReadLine();
                        if (newPassword.Equals(""))
                        {
                            Console.WriteLine($"\nPlease actually write something.\n");
                            continue;
                        }
                        else
                        {
                            next = true;
                            break;
                        }
                    }
                }
                if (choice.Equals("n") || next)
                {
                    next = false;
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
            while (true)
            {
                Console.WriteLine("Do you want a first name? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your first name.");
                        firstName = Console.ReadLine();
                        if (firstName.Equals(""))
                        {
                            Console.WriteLine("\nPlease actually write something.\n");
                            continue;
                        }
                        else
                        {
                            next = true;
                            break;
                        }
                    }
                }
                if (choice.Equals("n") || next)
                {
                    next = false;
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
            while (true)
            {
                Console.WriteLine("Do you want a last name? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your last name.");
                        lastName = Console.ReadLine();
                        if (lastName.Equals(""))
                        {
                            Console.WriteLine("\nPlease actually write something.\n");
                            continue;
                        }
                        else
                        {
                            next = true;
                            break;
                        }
                    }
                }
                if (choice.Equals("n") || next)
                {
                    next = false;
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
            while (true)
            {
                Console.WriteLine("Do you want an email? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your email.");
                        email = Console.ReadLine();
                        if (email.Equals(""))
                        {
                            Console.WriteLine("\nPlease actually write something.\n");
                            continue;
                        }
                        else
                        {
                            next = true;
                            break;
                        }
                    }
                }
                if (choice.Equals("n") || next)
                {
                    next = false;
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
            while (true)
            {
                Console.WriteLine("Do you want an age? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your age.");
                        string ageString = Console.ReadLine();
                        try
                        {
                            age = int.Parse(ageString);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("\nPlease write a number.\n");
                            continue;
                        }
                        next = true;
                        break;
                    }
                }
                if (choice.Equals("n") || next)
                {
                    next = false;
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
            while (true)
            {
                Console.WriteLine("Do you want an address? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your address.");
                        address = Console.ReadLine();
                        if (address.Equals(""))
                        {
                            Console.WriteLine("\nPlease actually write something.\n");
                            continue;
                        }
                        else
                        {
                            next = true;
                            break;
                        }
                    }
                }
                if (choice.Equals("n") || next)
                {
                    next = false;
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
            while (true)
            {
                Console.WriteLine("Do you want a phone number? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your phone number.");
                        phoneNumber = Console.ReadLine();
                        if (phoneNumber.Equals(""))
                        {
                            Console.WriteLine("\nPlease actually write something.\n");
                            continue;
                        }
                        else
                        {
                            next = true;
                            break;
                        }
                    }
                }
                if (choice.Equals("n") || next)
                {
                    break;
                }
                Console.WriteLine("\ny or n, please.\n");
            }
        }
        public void LogIn()
        {
            state.ChangeState(this);
        }

        public void LogOut()
        {
            state.ChangeState(this);
        }

        private void CanLogIn(bool found)
        {
            foreach (Customer customer in customers)
            {
                if (username.Equals(customer.Username) && customer.CheckPassword(password))
                {
                    Console.WriteLine();
                    Console.WriteLine(customer.Username + " logged in.");
                    Console.WriteLine();
                    currentCustomer = customer;
                    found = true;
                    option1 = "See Wares";
                    option2 = "Customer Info";
                    if (currentCustomer == null)
                    {
                        option3 = "Login";
                    }
                    else
                    {
                        option3 = "Logout";
                    }
                    info = "What would you like to do?";
                    currentMenu = "main menu";
                    currentChoice = 1;
                    amountOfOptions = 3;
                    break;
                }
            }
        }

        private void ToggleRight()
        {
            if (currentChoice < amountOfOptions)
            {
                currentChoice++;
            }
        }

        private void ToggleLeft()
        {
            if (currentChoice > 1)
            {
                currentChoice--;
            }
        }

        private void ToggleButtons()
        {
            for (int i = 0; i < amountOfOptions; i++)
            {
                Console.Write(i + 1 + "\t");
            }
            Console.WriteLine();
            for (int i = 1; i < currentChoice; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine("|");

            Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
        }

        private void PrintOptions()
        {
            Console.WriteLine("1: " + option1);
            Console.WriteLine("2: " + option2);
            if (amountOfOptions > 2)
            {
                Console.WriteLine("3: " + option3);
            }
            if (amountOfOptions > 3)
            {
                Console.WriteLine("4: " + option4);
            }
        }

        private void SeeAllWaresMenu()
         {
            for (int i = 0; i < amountOfOptions; i++)
            {
                Console.WriteLine(i + 1 + ": " + products[i].Name + ", " + products[i].Price + "kr");
            }
            Console.WriteLine("Your funds: " + currentCustomer.Funds);
         }

        private void bubbleSort(string variable, bool ascending)
        {
            if (variable.Equals("name")) {
                int length = products.Count;
                for(int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].Name.CompareTo(products[j + 1].Name) < 0)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].Name.CompareTo(products[j + 1].Name) > 0)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
            else if (variable.Equals("price"))
            {
                int length = products.Count;
                for (int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].Price > products[j + 1].Price)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].Price < products[j + 1].Price)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
        }
    }
}
