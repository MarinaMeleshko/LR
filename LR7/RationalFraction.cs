using System;
using System.Text.RegularExpressions;

namespace FuckYou.Berejnov
{
    public delegate void MyDelegate();
    class Test
    {
        private bool _isStudent;
        public void Method(int rightAnswersCount)
        {
            _isStudent = rightAnswersCount >= 12;
        }
    }

    namespace MyNamespace
    {
        
    }
}

namespace LR7
{
    public delegate void Delegate();
    class RationalFraction : IComparable<RationalFraction>, IEquatable<object>
    {
        public Delegate M = delegate()
        {
            Console.WriteLine("M");
        };

        readonly Delegate _n = () => Console.WriteLine("_n");

        public Delegate G;
        public int Numerator { get; private set; }
        public int Denumerator { get; private set; } //только больше нуля

        public RationalFraction()
        {
            Numerator = 0;
            Denumerator = 1;
            G = M + _n;
        }

        public RationalFraction(int numerator, int denumerator)
        {
            if (denumerator == 0)
            {
                Console.WriteLine("Знаменатель не может быть равен нулю");
                denumerator = 1;
            }
            else
            {
                if (denumerator < 0)
                {
                    denumerator *= -1;
                    numerator *= -1;
                }
            }
            Numerator = numerator;
            Denumerator = denumerator;
            Reduce();
        }

        public static int GreatestCommonDivisor(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                var gcd = a % b;
                a = b;
                b = gcd;
            }
            return a;
        }

        private static int LeastCommonMultiple(int a, int b) => a * b / GreatestCommonDivisor(a, b);

        public void Reduce()
        {
            var gcd = GreatestCommonDivisor(Numerator, Denumerator);
            Numerator /= gcd;
            Denumerator /= gcd;
        }

        public static void ReduceToACommonDenumerator(RationalFraction fraction1, RationalFraction fraction2)
        {
            var commonDenumerator = LeastCommonMultiple(fraction1.Denumerator, fraction2.Denumerator);
            fraction1.Numerator *= commonDenumerator / fraction1.Denumerator;
            fraction2.Numerator *= commonDenumerator / fraction2.Denumerator;
            fraction1.Denumerator = fraction2.Denumerator = commonDenumerator;
        }

        public static RationalFraction operator +(RationalFraction fraction1, RationalFraction fraction2)
        {
            ReduceToACommonDenumerator(fraction1, fraction2);
            var fraction = new RationalFraction(fraction1.Numerator + fraction2.Numerator, fraction1.Denumerator);
            fraction.Reduce();
            fraction1.Reduce();
            fraction2.Reduce();
            return fraction;
        }

        public static RationalFraction operator -(RationalFraction fraction1, RationalFraction fraction2)
            => fraction1 + (RationalFraction)(-1) * fraction2;

        public static RationalFraction operator *(RationalFraction fraction1, RationalFraction fraction2)
        {
            var fraction = new RationalFraction(fraction1.Numerator * fraction2.Numerator,
                fraction1.Denumerator * fraction2.Denumerator);
            fraction.Reduce();
            return fraction;
        }

        public static RationalFraction operator /(RationalFraction fraction1, RationalFraction fraction2)
        {
            var fraction = new RationalFraction(fraction1.Numerator * fraction2.Denumerator,
                fraction1.Denumerator * fraction2.Numerator);
            fraction.Reduce();
            return fraction;
        }

        public static RationalFraction operator ++(RationalFraction fraction) => fraction + (RationalFraction)1;

        public static RationalFraction operator --(RationalFraction fraction) => fraction - (RationalFraction)1;

        public static bool operator >(RationalFraction fraction1, RationalFraction fraction2)
        {
            ReduceToACommonDenumerator(fraction1, fraction2);
            var result = fraction1.Numerator > fraction2.Numerator;
            fraction1.Reduce();
            fraction2.Reduce();
            return result;
        }

        public static bool operator <(RationalFraction fraction1, RationalFraction fraction2) => fraction2 > fraction1;

        public static bool operator >=(RationalFraction fraction1, RationalFraction fraction2)
        {
            ReduceToACommonDenumerator(fraction1, fraction2);
            var result = fraction1.Numerator >= fraction2.Numerator;
            fraction1.Reduce();
            fraction2.Reduce();
            return result;
        }

        public static bool operator <=(RationalFraction fraction1, RationalFraction fraction2) => fraction2 >= fraction1;

        public static bool operator ==(RationalFraction fraction1, RationalFraction fraction2)
        {
            if (object.ReferenceEquals(fraction1, fraction2) == true)
            {
                return true;
            }
            if ((object)fraction1 == null || (object)fraction2 == null)
            {
                return false;
            }
            return (fraction1.Numerator == fraction2.Numerator) && (fraction1.Denumerator == fraction2.Denumerator);
        }

        public static bool operator !=(RationalFraction fraction1, RationalFraction fraction2) => !(fraction1 == fraction2);

        public static explicit operator RationalFraction(int number) => new RationalFraction(number, 1);

        public static explicit operator RationalFraction(double number)
        {
            var denumerator = 1;
            while (Math.Abs(number - Math.Truncate(number)) > 0)
            {
                denumerator *= 10;
                number *= 10;
            }
            var fraction = new RationalFraction((int)number, denumerator);
            fraction.Reduce();
            return fraction;
        }

        public static explicit operator int(RationalFraction fraction) => fraction.Numerator / fraction.Denumerator;

        public static explicit operator double(RationalFraction fraction)
            => (double)fraction.Numerator / fraction.Denumerator;

        public override bool Equals(object obj)
        {
            var pointer = obj as RationalFraction;
            if (pointer == null)
            {
                return false;
            }
            pointer.Reduce();
            return (pointer.Numerator == Numerator) && (pointer.Denumerator == Denumerator);
        }

        public override int GetHashCode() => new Random().Next(1, 1000);//но в лабе это не нужно было
        public string ConvertToACommonFraction() => Convert.ToString(Numerator) + '/' + Convert.ToString(Denumerator);
        public string ConvertToADecimalFraction() => Convert.ToString((double)Numerator / Denumerator);

        private static readonly char[] DivisionSigns = { '/', ':' };
        public static RationalFraction GetRationalFractionFromString(string input)
        {
            double result;
            if (double.TryParse(input, out result))
            {
                return (RationalFraction)result;
            }
            if (!Regex.IsMatch(input, "^-?[0-9]+(/|:)-?[0-9]+$"))
            {
                return new RationalFraction();
            }
            var numeratorAndDenumerator = input.Split(DivisionSigns);
            var numerator = int.Parse(numeratorAndDenumerator[0]);
            var denumerator = int.Parse(numeratorAndDenumerator[1]);
            return new RationalFraction(numerator, denumerator);
        }

        public int CompareTo(RationalFraction fraction)
        {
            if (this > fraction)
            {
                return 1;
            }
            if (this == fraction)
            {
                return 0;
            }
            return -1;
        }
    }
}