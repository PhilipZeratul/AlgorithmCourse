using System;
using System.Numerics;


namespace KaratsubaMultiplication
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string xString = "3141592653589793238462643383279502884197169399375105820974944592";
            string yString = "2718281828459045235360287471352662497757247093699959574966967627";
            BigInteger x = BigInteger.Parse(xString);
            BigInteger y = BigInteger.Parse(yString);

            Console.WriteLine("Karatsuba Multiplication: {0} * {1}", x, y);

            BigInteger result = Karatsuba(x, y);

            Console.WriteLine("Result  = {0}", result);
            Console.WriteLine("Should be {0}", x * y);
        }

        private static BigInteger Karatsuba(BigInteger x, BigInteger y)
        {
            Console.WriteLine("________________________");
            Console.WriteLine("x = {0}, y = {1}", x, y);

            if (x < 10 || y < 10)
            {
                BigInteger result = x * y;
                Console.WriteLine("return {0}", result);
                return result;
            }

            BigInteger xDigitsHalfPow10 = BigInteger.Pow(10, (int)((Math.Floor(BigInteger.Log10(x) + 1)) / 2));
            BigInteger yDigitsHalfPow10 = BigInteger.Pow(10, (int)((Math.Floor(BigInteger.Log10(y) + 1)) / 2));

            Console.WriteLine("xDigitsHalfPow10 = {0}, yDigitsHalfPow10 = {1}", xDigitsHalfPow10, yDigitsHalfPow10);

            // We cannot use Katsuba Multiplication when x & y have different num of digits!
            if (xDigitsHalfPow10 != yDigitsHalfPow10)
            {
                BigInteger result = x * y;
                Console.WriteLine("Different digits, cannot use Katsuba Multiplicaiton, return {0}", result);
                return result;
            }


            BigInteger a = x / xDigitsHalfPow10;
            BigInteger b = x % xDigitsHalfPow10;
            BigInteger c = y / yDigitsHalfPow10;
            BigInteger d = y % yDigitsHalfPow10;

            Console.WriteLine("a = {0}, b = {1}, c = {2}, d = {3}", a, b, c, d);

            BigInteger ac = Karatsuba(a, c);
            BigInteger bd = Karatsuba(b, d);
            BigInteger step3 = Karatsuba((a + b), (c + d));
            BigInteger step4 = step3 - ac - bd;

            return ac * xDigitsHalfPow10 * yDigitsHalfPow10 + bd + step4 * xDigitsHalfPow10;
        }
    }
}
