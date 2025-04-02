using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBNCaller_Lib
{
    internal class ISBNCalculator
    {
        internal string CalculateISBN10(string isbn13)
        {
            isbn13 = isbn13.Remove(0, 3);
            isbn13 = isbn13.Remove(9, 1);
            char[] split = isbn13.ToCharArray();
            int[] numbers = { int.Parse(split[0].ToString()), int.Parse(split[1].ToString()), int.Parse(split[2].ToString()), int.Parse(split[3].ToString()), int.Parse(split[4].ToString()), int.Parse(split[5].ToString()), int.Parse(split[6].ToString()), int.Parse(split[7].ToString()), int.Parse(split[8].ToString()) };
            int checkDigit = 0;
            for (int index = 0; index < 9; index++)
            {
                checkDigit += numbers[index] * (index + 1);
            }

            checkDigit = checkDigit % 10;
            string isbn10 = $"{isbn13}{checkDigit}";

            return isbn10;
        }

        internal string CalculateISBN13(string isbn10)
        {
            isbn10 = isbn10.Remove(9, 1);
            int checkDigit = 0;
            char[] split = isbn10.ToCharArray();
            int[] numbers = { 9, 7, 8, int.Parse(split[0].ToString()), int.Parse(split[1].ToString()), int.Parse(split[2].ToString()), int.Parse(split[3].ToString()), int.Parse(split[4].ToString()), int.Parse(split[5].ToString()), int.Parse(split[6].ToString()), int.Parse(split[7].ToString()), int.Parse(split[8].ToString()) };
            for (int index = 0; index < 12; index += 2)
            {
                checkDigit += numbers[index];
            }

            for (int index = 1; index < 12; index += 2)
            {
                checkDigit += numbers[index] * 3;
            }

            checkDigit = checkDigit % 10;
            checkDigit = checkDigit == 0 ? checkDigit : 10 - checkDigit;
            string isbn13 = $"978{isbn10}{checkDigit}";

            return isbn13;
        }
    }
}
