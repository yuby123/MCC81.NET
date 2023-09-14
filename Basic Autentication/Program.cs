using System;

namespace BasicAuth
{

    public class User
    {
        public int userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Program
    {
        private static List<User> users = new List<User>();

        public static void Main()
        {
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
                            CreateUser();
                            break;
                        case 2:
                            ShowUsers();
                            break;
                        case 3:
                            SearchUser();
                            break;
                        case 4:
                            LoginUser();
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

        public static void CreateUser()
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

            var user = new User
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
            if (firstName.Length >= 2 && lastName.Length >= 2)
            {
                string username = firstName.Substring(0, 2) + lastName.Substring(0, 2);
                return username;
            }
            else
            {
                Console.WriteLine("First Name and Last Name must have at least 2 characters.");
                return null; // Mengembalikan null jika panjang nama tidak memenuhi syarat
            }
        }



        public static void ShowUsers()
        {
            Console.Clear();
            Console.WriteLine("==SHOW USER==");
            Console.WriteLine("========================");

            int userId = 1;
            foreach (var user in users)
            {
                Console.WriteLine($"ID       : {userId}");
                Console.WriteLine($"Name     : {user.FirstName} {user.LastName}");
                Console.WriteLine($"Username : {user.Username}");
                Console.WriteLine($"Password : {user.Password}");
                Console.WriteLine("========================");
                userId++;
            }

            Console.WriteLine("Menu");
            Console.WriteLine("1. Edit User");
            Console.WriteLine("2. Delete User");
            Console.WriteLine("3. Back");

            int choice;
            do
            {
                Console.Write("Masukkan Pilihan: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            EditUser();
                            break;
                        case 2:
                            DeleteUser();
                            break;
                        case 3:
                            return;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
            } while (true);
        }

        public static void EditUser()
        {

            Console.Write("Id Yang Ingin Diubah: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                if (userId >= 1 && userId <= users.Count)
                {
                    User userToEdit = users[userId - 1];
                    Console.Write("New First Name: ");
                    userToEdit.FirstName = Console.ReadLine();

                    Console.Write("New Last Name: ");
                    userToEdit.LastName = Console.ReadLine();

                    // Buat username otomatis berdasarkan 3 digit awal dari firstName dan lastName yang baru
                    userToEdit.Username = GenerateUsername(userToEdit.FirstName, userToEdit.LastName);

                    Console.Write("New Password: ");
                    userToEdit.Password = Console.ReadLine();

                    Console.WriteLine("User Sudah Berhasil Di Edit");

                }
                else
                {
                    Console.WriteLine("Invalid ID. User not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }
        }

        public static void DeleteUser()
        {
            Console.Write("Id Yang Ingin Dihapus: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                if (userId >= 1 && userId <= users.Count)
                {
                    User userToDelete = users[userId - 1];
                    users.Remove(userToDelete);
                    Console.WriteLine("Akun Berhasil Di Hapus");

                }
                else
                {
                    Console.WriteLine("Invalid ID. User not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }
        }

        public static void SearchUser()
        {
            Console.Clear();
            Console.WriteLine("==Cari Akun==");
            Console.Write("Masukkan Nama : ");
            string name = Console.ReadLine();

            // Instead of FirstOrDefault, we use Where to get all matching users
            var matchedUsers = users.Where(u => u.FirstName == name || u.LastName == name).ToList();

            if (matchedUsers.Count > 0)
            {
                foreach (var user in matchedUsers)
                {
                    Console.WriteLine("========================");
                    Console.WriteLine($"ID       : {user.userId}");
                    Console.WriteLine($"Name     : {user.FirstName} {user.LastName}");
                    Console.WriteLine($"Username : {user.Username}");
                    Console.WriteLine($"Password : {user.Password}");
                    Console.WriteLine("========================");
                }
            }
            else
            {
                Console.WriteLine("User not found!");
            }

            Console.ReadLine();
        }


        public static void LoginUser()
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
