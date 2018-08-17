using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using System.Windows.Forms;

namespace PasswordGen
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool keepGenerating = true;
            while (keepGenerating)
                keepGenerating = PasswordGeneratorCore();

            Environment.Exit(-1);
        }

        public static bool PasswordGeneratorCore()
        {
            Console.Write("Specify number of characters: ");
            var length = int.Parse(Console.ReadLine());
            var password = new Faker().Internet.Password(length, false);
            Console.WriteLine($"Password is: {password}");
            Console.Write("Copy to clipboard?: ");
            var response = Console.ReadKey();
            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Clipboard.SetText(password);
                Console.WriteLine("Password copied.");
            }
            else
                Console.WriteLine();

            Console.Write("Generate another? ");
            response = Console.ReadKey();
            Console.WriteLine();
            return (response.Key == ConsoleKey.Y);
        }

    }
}
