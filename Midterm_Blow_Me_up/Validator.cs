using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Midterm_Blow_Me_up
{
    class Validator
    {
        static Validator()
        {

        }

        public static int CheckInts(string errorDiag, string x, int min, int max)
        {
            int z;
            string y = x;
            while (!Int32.TryParse(y, out z) || z < min || z > max)
            {
                Console.WriteLine(errorDiag);
                y = Console.ReadLine();
            }

            return z;
        }

        public static double CheckDubs(string x)
        {
            double z;
            string y = x;

            while (!Double.TryParse(y, out z) || z <= 0)
            {
                Console.WriteLine("That is not a valid input, please enter a positive number\n");
                y = Console.ReadLine();
            }

            return z;
        }

        public static string CheckInput(string x, char a)
        {
            string b = a.ToString().ToLower();
            Regex reg = new Regex(@"\s{0,}[a-" + b + "[A-" + a + "]");

            while (!reg.IsMatch(x))
            {
                Console.WriteLine($"Please enter A-{a} to choose a row");
                x = Console.ReadLine();
            }

            return x;
        }

        public static string CheckFlag(string errorDiag, string x)
        {
            Regex reg = new Regex(@"\s{0,}[fFcC]");

            while (!reg.IsMatch(x))
            {
                Console.WriteLine(errorDiag);
                x = Console.ReadLine();
            }

            return x;
        }
    }
}
