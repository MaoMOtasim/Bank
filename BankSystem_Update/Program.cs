using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystemUpdated;


namespace BankSystemUpdated
{
    // 1- Customer Class And The Attribute
    public class Customer
    {
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public int PIN { get; set; }
        public double Balance { get; set; }

        // 2- This Method To Ganarete Account Number
        public int GenerateRandomNo()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();

            return _rdm.Next(_min, _max);
        }
    }

    // 3- The CustomerOperations Class To Keep All Opr In Same Unite "Encapsulation"
    public class CustomerOperations : Customer
    {
        public Customer Registration(int accountNumber=8,int id)
        {
            Console.WriteLine("Enter Your Name");
            string customerName = Console.ReadLine();

            Console.WriteLine("Enter your PIN");
            int customerPIN = int.Parse(Console.ReadLine());

            Customer customer = new Customer();
            customer.Name = customerName;
            customer.AccountNumber = GenerateRandomNo();
            customer.PIN = customerPIN;

            return customer;

        }

        //  4- This Method To Display And Save Customers In List
        // OverLoding
        public void DisplayCustomerInfo(List<Customer> customers)
        {
            Console.WriteLine("List of registered Customers");

            foreach (var customer in customers)
            {
                DisplayCustomerInfo(customer);
            }
        }

        // 5- This Method To Display When Make Deposit .
        // OverLoding
        public void DisplayCustomerInfo(Customer customer)
        {
            Console.WriteLine("Customer Name: " + customer.Name);
            Console.WriteLine("Customer Account: " + customer.AccountNumber);
            Console.WriteLine("Customer PIN: " + customer.PIN);
            Console.WriteLine("Balance: " + customer.Balance);
            Console.WriteLine("-------------------------------------------");
        }

        // 6- Verify customer's PIN and return the customer object if PIN is correct, null otherwise
        public Customer FindCustomerByPin(int pin, List<Customer> customers)
        {
            return customers.FirstOrDefault(customer => customer.PIN == pin);
        }

        // 7- Perform a deposit for a customer (Encapsulation)
        public void Deposit(List<Customer> customers)
        {
            Console.WriteLine("Enter The PIN: ");
            int pin = Int32.Parse(Console.ReadLine());
            var customer = FindCustomerByPin(pin, customers);

            if (customer == null)
            {
                Console.WriteLine("No customer found with this PIN.");
            }
            else
            {
                Console.WriteLine("Enter the amount to deposit: ");
                double amount = double.Parse(Console.ReadLine());

                customer.Balance += amount;
                DisplayCustomerInfo(customer);
            }
        }

        // 8- Perform a Withdrawal for a customer (Encapsulation)
        public void Withdrawal(List<Customer> customers)
        {
            Console.WriteLine("Enter The PIN: ");
            int pin = Int32.Parse(Console.ReadLine());
            var customer = FindCustomerByPin(pin, customers);

            if (customer == null)
            {
                Console.WriteLine("No customer found with this PIN.");
            }
            else
            {
                Console.WriteLine("Enter the amount to withdrawal: ");
                double amount = double.Parse(Console.ReadLine());

                if (amount > customer.Balance)
                {
                    Console.WriteLine("Insufficient balance.");

                    return;
                }

                customer.Balance -= amount;
                DisplayCustomerInfo(customer);
            }

        }
        // 9- // Perform a transfer between two customers (Encapsulation)
        public void Transfer(List<Customer> customers)
        {
            Console.WriteLine("Enter the customer's PIN to transfer from:");
            int pinFrom = int.Parse(Console.ReadLine());
            var customerFrom = FindCustomerByPin(pinFrom, customers);

            if (customerFrom == null)
            {
                Console.WriteLine("No customer found with this PIN.");
            }
            else
            {
                Console.WriteLine("Enter the account number to transfer to: ");
                int accountNumberTo = int.Parse(Console.ReadLine());
                var customerTo = customers.FirstOrDefault(cusstomer => cusstomer.AccountNumber == accountNumberTo);



                if (customerTo == null)
                {
                    Console.WriteLine("No customer found with account number.");

                    return;
                }

                Console.WriteLine("Enter the amount to transfer: ");
                double amount = double.Parse(Console.ReadLine());


                if (amount > customerFrom.Balance)
                {
                    Console.WriteLine("Insufficient balance.");

                    return;
                }

                customerFrom.Balance -= amount;
                customerTo.Balance += amount;

                Console.WriteLine("Transfer successful.");
                DisplayCustomerInfo(customerFrom);
                DisplayCustomerInfo(customerTo);

            }
        }




        internal class Program
        {
            static void Main(string[] args)
            {
                List<Customer> customerList = new List<Customer>();
                CustomerOperations customerOperations = new CustomerOperations();

                while (true)
                {
                    Console.WriteLine("Please choose from the following options:");
                    Console.WriteLine("1- Add new customer");
                    Console.WriteLine("2- Display all customers");
                    Console.WriteLine("3- Deposit");
                    Console.WriteLine("4- Withdraw");
                    Console.WriteLine("5- Transfer");
                    Console.WriteLine("6- Exit");

                    int userInput = Int32.Parse(Console.ReadLine());

                    switch (userInput)
                    {
                        case 1:
                            var customer = customerOperations.Registration();
                            customerList.Add(customer);

                            break;

                        case 2:
                            customerOperations.DisplayCustomerInfo(customerList);

                            break;

                        case 3:
                            customerOperations.Deposit(customerList);

                            break;

                        case 4:
                            customerOperations.Withdrawal(customerList);

                            break;

                        case 5:
                            customerOperations.Transfer(customerList);
                            break;

                        case 6:
                           return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                }
            }


        }
    }
}


