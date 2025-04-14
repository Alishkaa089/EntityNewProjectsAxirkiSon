using System;
using System.Linq;
using static ReadPassword;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new StepShopDbContext())
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice (0, 1, or 2): ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("Exiting the program...");
                    break;
                }

                if (choice == "2")
                {
                    Console.Write("New username: ");
                    string newUsername = Console.ReadLine();
                    Console.Write("New password: ");
                    string newPassword = PasswordReader.ReadPassword();

                    Console.Write("Role (admin, user, cashier): ");
                    string role = Console.ReadLine().ToLower();

                    var newUser = new User
                    {
                        Username = newUsername,
                        Password = newPassword,
                        Role = role,
                        IsDeleted = false
                    };

                    try
                    {
                        var signUp = new Sign_Up();
                        string result = signUp.SignUp(newUser);
                        Console.WriteLine(result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred during registration: {ex.Message}");
                    }

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                }


                if (choice == "1")
                {
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = PasswordReader.ReadPassword();

                    try
                    {
                        var signIn = new Sign_In();
                        var user = signIn.SignIn(username, password);

                        if (user.IsDeleted)
                        {
                            Console.WriteLine("This user has been deleted and cannot log in.");
                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey();
                            continue;
                        }

                        Console.WriteLine($"\nLogin successful! Your role: {user.Role}");
                        switch (user.Role.ToLower())
                        {
                            case "admin":
                                AdminActions.PerformActions(context);
                                break;
                            case "user":
                                UserActions.PerformActions(context, user);
                                break;
                            case "cashier":
                                CashierActions.PerformActions(context);
                                break;
                            default:
                                Console.WriteLine("Unrecognized role!");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                    }
                }
            }
        }
    }


}
