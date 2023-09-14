
namespace GanjilGenap
{

    public class GanjilGenap
    {
        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("\tMENU GANJIL GENAP\t");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("1. Cek Genap atau Ganjil");
                Console.WriteLine("2. Cetak Genap/Ganjil (dengan limit)");
                Console.WriteLine("3. Exit");
                Console.WriteLine("-------------------------------------");
                Console.Write("Masukkan pilihan : ");

                int pilihan;
                if (int.TryParse(Console.ReadLine(), out pilihan))
                {
                    switch (pilihan)
                    {
                        case 1:
                            Console.Write("Masukkan bilangan yang ingin dicek: ");
                            if (int.TryParse(Console.ReadLine(), out int angka))
                            {
                                EvenOddCheck(angka);
                            }
                            else
                            {
                                Console.WriteLine("Inputan tidak valid. Masukkan hanya angka.");
                            }

                            break;
                        case 2:
                            Console.Write("Pilih tampilan (ganjil/genap): ");
                            string choice = Console.ReadLine().ToLower();
                            Console.Write("Masukkan batas limit: ");
                            if (int.TryParse(Console.ReadLine(), out int batas))
                            {
                                PrintEvenOdd(batas, choice);
                            }
                            else
                            {
                                Console.WriteLine("Inputan limit tidak valid. Masukkan hanya angka.");
                            }

                            break;
                        case 3:
                            Console.WriteLine("Program selesai.");
                            return;
                        default:
                            Console.WriteLine("Pilihan tidak valid.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Masukkan pilihan yang valid.");
                }
            }
        }

        static void PrintEvenOdd(int limit, string choice)
        {

            Console.WriteLine($"Print bilangan dari 1 - {limit}:");


            if (limit < 1)
            {
                Console.WriteLine("Input Limit Tidak Valid!!");
            }
            else
            {
                int start = (choice == "ganjil") ? 1 : 2;

                if (choice == "ganjil" || choice == "genap")
                {
                    for (int i = start; i <= limit; i += 2)
                    {
                        Console.Write($"{i}, ");
                    }
                    Console.WriteLine();
                }
                else
                {

                    Console.WriteLine("Input Pilihan Tidak Valid!!");
                }
            }

            Console.WriteLine("=====================================");
        }


        static void EvenOddCheck(int input)
        {
            if (input < 1)
            {
                Console.WriteLine("Invalid Input!!!");
            }
            else if (input % 2 == 0)
            {
                Console.WriteLine($"{input} adalah angka genap.");
            }
            else
            {
                Console.WriteLine($"{input} adalah angka ganjil.");
            }

            Console.WriteLine("=====================================");
        }

        public static void Ganjil()
        {
            Menu();
        }
    }
}

