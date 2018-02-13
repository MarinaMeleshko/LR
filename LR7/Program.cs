//это не смотри, там страшно, тебе ещё рано) 
//это просто преподавателю показать, что всё работает
//(вроде всё работает))

using System;

namespace LR7
{
    class Program
    {
        static void OutputCommonFraction(string output, RationalFraction fraction)
        {
            Console.WriteLine(output + "\t" + fraction.ConvertToACommonFraction());
        }

        static void OutputDecimalFraction(string output, RationalFraction fraction)
        {
            Console.WriteLine(output + "\t" + fraction.ConvertToADecimalFraction());
        }
        static void Main(string[] args)
        {
            char a = '1';
            int b = a + 1;
            Console.WriteLine(b);
            Console.WriteLine("Введите две дроби в любом формате:\n");
            var fractionString1 = Console.ReadLine();
            var fractionString2 = Console.ReadLine();
            var fraction1 = RationalFraction.GetRationalFractionFromString(fractionString1);
            var fraction2 = RationalFraction.GetRationalFractionFromString(fractionString2);
            OutputCommonFraction("\nСложение:", fraction1 + fraction2);
            OutputCommonFraction("Вычитание:", fraction1 - fraction2);
            OutputDecimalFraction("Деление:", fraction1 / fraction2);
            OutputDecimalFraction("Умножение:", fraction1 * fraction2);
            OutputCommonFraction("Инкремент:", ++fraction1);
            OutputDecimalFraction("Декремент:", --fraction2);
            fraction1--;
            fraction2++;
            Console.WriteLine();
            if (fraction1 > fraction2)
            {
                Console.WriteLine(">\n!=");
            }
            else
            {
                Console.WriteLine("<=");
                Console.WriteLine(fraction1 == fraction2 ? "==" : "<\n!=");
            }
            Console.WriteLine("\nint:\t" + (int)fraction1);
            Console.WriteLine("double:\t" + (double)fraction2);
            Console.ReadKey();
        }
    }
}