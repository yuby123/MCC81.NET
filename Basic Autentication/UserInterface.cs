using BasicAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuth
{
    public class UserInterface
    {
        private static List<Users> users = new List<Users>();
  
        public void ShowUsers()
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
                if (userId >= 1 && userId <= users.Count)
                {
                    Users userToDelete = users[userId - 1];
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
    }
}
