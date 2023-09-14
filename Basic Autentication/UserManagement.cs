using BasicAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuth
{
    internal class UserManagement
    {
        private static List<Users> users = new List<Users>();
        public void CreateUser()
        {
            Console.Clear();
            string firstName, lastName, username, password;

            do
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                lastName = Console.ReadLine();

                username = GenerateUsername(firstName, lastName);

                if (IsUsernameTaken(username))
                {
                    Console.WriteLine("Username already exists. Please choose a different one.");
                }
            } while (IsUsernameTaken(username));

            bool isValidPassword;

            do
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
                isValidPassword = ValidatePassword(password);

                if (!isValidPassword)
                {
                    Console.WriteLine("Password must have at least 8 characters with at least one Capital letter, one lowercase letter, and one number.");
                }
            } while (!isValidPassword);

            var user = new Users
            {
                userId = users.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            users.Add(user);

            Console.WriteLine("\nUser data has been successfully created.");
            Console.ReadLine();
        }


        public static bool IsUsernameTaken(string username)
        {
            // Check if the username already exists in the list of users
            return users.Any(user => user.Username == username);
        }


        public static bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowerCase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            return hasUpperCase && hasLowerCase && hasDigit;
        }

        public static string GenerateUsername(string firstName, string lastName)
        {
            // Mengambil dua huruf pertama dari nama depan dan nama belakang
            string firstTwoChars = firstName.Substring(0, Math.Min(2, firstName.Length));
            string lastTwoChars = lastName.Substring(0, Math.Min(2, lastName.Length));

            // Menggabungkan dua huruf dari nama depan dan nama belakang menjadi username
            string username = firstTwoChars + lastTwoChars;

            return username;
        }

        public void LoginUser()
        {
            Console.Clear();
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("Login successful!");
                GanjilGenap.GanjilGenap.Ganjil();
            }
            else
            {
                Console.WriteLine("Invalid username or password!");
            }
        }
    }
}
