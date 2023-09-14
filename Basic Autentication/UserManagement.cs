using BasicAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuth;

internal class UserManagement
{
    public static List<Users> users = new List<Users>();
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


        int nextId = 1; // Menentukan ID berikutnya
        if (users.Count > 0)
        {
            nextId = users.Max(u => u.userId) + 1;
        }

        // Mengecek apakah ID sudah ada dalam daftar pengguna
        while (users.Any(u => u.userId == nextId))
        {
            nextId++; // Menambahkan 1 ke ID jika sudah ada
        }

        var user = new Users
        {
            userId = nextId,
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

    public void ShowUsers()
    {
        Console.Clear();
        Console.WriteLine("==SHOW USER==");
        Console.WriteLine("========================");

      
        foreach (Users user in users)
        {
            Console.WriteLine($"ID       : {user.userId}");
            Console.WriteLine($"Name     : {user.FirstName} {user.LastName}");
            Console.WriteLine($"Username : {user.Username}");
            Console.WriteLine($"Password : {user.Password}");
            Console.WriteLine("========================");
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
                Users userToEdit = users[userId - 1];
                Console.Write("New First Name: ");
                userToEdit.FirstName = Console.ReadLine();

                Console.Write("New Last Name: ");
                userToEdit.LastName = Console.ReadLine();

                // Buat username otomatis berdasarkan 2 digit awal dari firstName dan lastName yang baru
                userToEdit.Username = UserManagement.GenerateUsername(userToEdit.FirstName, userToEdit.LastName);

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
            Users userDelete = users.FirstOrDefault(u => u.userId == userId);
            if (userDelete != null)
            {
                users.Remove(userDelete);
                Console.WriteLine($"Data dengan id {userId} berhasil dihapus ! \n");
            }
            else
            {
                Console.WriteLine($"Data dengan id {userId} tidak ada !");
            }
        }
    }
    public void SearchUser()
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

