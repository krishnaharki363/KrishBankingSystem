using System;

namespace SimpleBankSystem
{
    class Program
    {
        // Constants
        private const string CORRECT_PIN = "5050";
        private const int MAX_ATTEMPTS = 3;
        
        // Account balance
        private static decimal balance = 1000.00m; // Starting balance
        
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║   Welcome to Krish Bank System   ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();
            
            // PIN Authentication
            if (!AuthenticateUser())
            {
                Console.WriteLine("\n System Locked! Too many failed attempts.");
                Console.WriteLine("Please contact Krish Naharki.");
                return;
            }
            
            Console.WriteLine("\n✓ Login Successful!");
            Console.WriteLine();
            
            // Main banking operations loop
            RunBankingOperations();
            
            Console.WriteLine("\nThank you for using Krish Bank System!");
            Console.WriteLine("Goodbye!");
        }
        
        /// <summary>
        /// Authenticates user with PIN verification
        /// </summary>
        /// <returns>True if authentication successful, false otherwise</returns>
        static bool AuthenticateUser()
        {
            int attempts = 0;
            
            while (attempts < MAX_ATTEMPTS)
            {
                Console.Write($"Enter PIN (Attempt {attempts + 1}/{MAX_ATTEMPTS}): ");
                string enteredPin = ReadPassword();
                
                if (enteredPin == CORRECT_PIN)
                {
                    return true;
                }
                
                attempts++;
                
                if (attempts < MAX_ATTEMPTS)
                {
                    Console.WriteLine($" Incorrect PIN. You have {MAX_ATTEMPTS - attempts} attempt(s) remaining.");
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// Reads password input and masks it with asterisks
        /// </summary>
        /// <returns>The entered password</returns>
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            
            do
            {
                key = Console.ReadKey(true);
                
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            
            Console.WriteLine();
            return password;
        }
        
        /// <summary>
        /// Main loop for banking operations
        /// </summary>
        static void RunBankingOperations()
        {
            bool running = true;
            
            while (running)
            {
                DisplayMenu();
                
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                
                switch (choice)
                {
                    case "1":
                        Deposit();
                        break;
                    case "2":
                        Withdraw();
                        break;
                    case "3":
                        CheckBalance();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine(" Invalid choice. Please select 1-4.");
                        break;
                }
                
                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        
        /// <summary>
        /// Displays the main menu
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("┌────────────────────────────────┐");
            Console.WriteLine("│      BANKING OPERATIONS        │");
            Console.WriteLine("├────────────────────────────────┤");
            Console.WriteLine("│ 1. Deposit                     │");
            Console.WriteLine("│ 2. Withdrawal                  │");
            Console.WriteLine("│ 3. Balance Inquiry             │");
            Console.WriteLine("│ 4. Exit                        │");
            Console.WriteLine("└────────────────────────────────┘");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Handles deposit operation
        /// </summary>
        static void Deposit()
        {
            Console.WriteLine("═══ DEPOSIT ═══");
            Console.Write("Enter amount to deposit: $");
            
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    balance += amount;
                    Console.WriteLine($"\n✓ Successfully deposited ${amount:F2}");
                    Console.WriteLine($"New balance: ${balance:F2}");
                }
                else
                {
                    Console.WriteLine("\n Error: Deposit amount must be positive!");
                }
            }
            else
            {
                Console.WriteLine("\n Error: Invalid amount. Please enter a valid number.");
            }
        }
        
        /// <summary>
        /// Handles withdrawal operation
        /// </summary>
        static void Withdraw()
        {
            Console.WriteLine("═══ WITHDRAWAL ═══");
            Console.WriteLine($"Current balance: ${balance:F2}");
            Console.Write("Enter amount to withdraw: $");
            
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    if (amount <= balance)
                    {
                        balance -= amount;
                        Console.WriteLine($"\n✓ Successfully withdrew ${amount:F2}");
                        Console.WriteLine($"New balance: ${balance:F2}");
                    }
                    else
                    {
                        Console.WriteLine($"\n Error: Insufficient funds!");
                        Console.WriteLine($"Available balance: ${balance:F2}");
                        Console.WriteLine($"Attempted withdrawal: ${amount:F2}");
                        Console.WriteLine($"Shortfall: ${amount - balance:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("\n Error: Withdrawal amount must be positive!");
                }
            }
            else
            {
                Console.WriteLine("\n Error: Invalid amount. Please enter a valid number.");
            }
        }
        
        /// <summary>
        /// Displays current balance
        /// </summary>
        static void CheckBalance()
        {
            Console.WriteLine("═══ BALANCE INQUIRY ═══");
            Console.WriteLine($"\nYour current balance: ${balance:F2}");
        }
    }
}