using System;

namespace BasicAuth
{
    public class Program
    {
        public static void Menu()
        {
            UserManagement user1 = new UserManagement();
            UserInterface user2 = new UserInterface();
            int choice;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== BASIC AUTHENTICATION==");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Show User");
                Console.WriteLine("3. Search User");
                Console.WriteLine("4. Login User");
                Console.WriteLine("5. Exit");
                Console.Write("Input :  ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            user1.CreateUser();
                            break;
                        case 2:
                            user2.ShowUsers();
                            break;
                        case 3:
                            user2.SearchUser();
                            break;
                        case 4:
                            user1.LoginUser();
                            break;
                        case 5:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Pilihan tidak valid.");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Masukkan pilihan yang valid.");
                    Console.ReadLine();
                }

            }

        }
        public static void Main()
        {
            Menu();
        }

    } 
}
