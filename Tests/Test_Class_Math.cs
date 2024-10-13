/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Math.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 12.10.24 Kristian Virtanen
 * License: See license.txt
 */

using SmallBasicOpenEditionDll;
namespace SmallBasicOpenEditionDll
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test Pi constant
            Console.WriteLine("Pi: " + Math.Pi);

            // Test Absolute value
            Console.WriteLine("Abs(-5): " + Math.Abs(-5));

            // Test Ceiling
            Console.WriteLine("Ceiling(4.2): " + Math.Ceiling(4.2));

            // Test Floor
            Console.WriteLine("Floor(4.8): " + Math.Floor(4.8));

            // Test Natural Log (logarithm base e)
            Console.WriteLine("Natural Log(2.718): " + Math.NaturalLog(2.718));

            // Test Log (logarithm base 10)
            Console.WriteLine("Log(1000): " + Math.Log(1000));

            // Test Cosine
            Console.WriteLine("Cos(0): " + Math.Cos(0));

            // Test Sine
            Console.WriteLine("Sin(0): " + Math.Sin(0));

            // Test Tangent
            Console.WriteLine("Tan(45 degrees): " + Math.Tan(Math.GetRadians(45)));

            // Test Square Root
            Console.WriteLine("SquareRoot(16): " + Math.SquareRoot(16));

            // Test Power
            Console.WriteLine("Power(2, 3): " + Math.Power(2, 3));

            // Test Round
            Console.WriteLine("Round(4.6): " + Math.Round(4.6));

            // Test Max
            Console.WriteLine("Max(10, 20): " + Math.Max(10, 20));

            // Test Min
            Console.WriteLine("Min(10, 20): " + Math.Min(10, 20));

            // Test Remainder
            Console.WriteLine("Remainder(10, 3): " + Math.Remainder(10, 3));

            // Test Get Random Number
            Console.WriteLine("Random number between 1 and 100: " + Math.GetRandomNumber(100));

            // Test Angle Conversion: Degrees to Radians
            Console.WriteLine("45 degrees in radians: " + Math.GetRadians(45));

            // Test Angle Conversion: Radians to Degrees
            Console.WriteLine("Pi radians in degrees: " + Math.GetDegrees(Math.Pi));

            // Test Decimal Conversion
            Console.WriteLine("Double to Decimal (123.456): " + Math.DoubleToDecimal(123.456));

            Console.ReadLine(); // Pause the console window to view results
        }
    }
}

/* OUTPUT
Pi: 3,141592653589793
Abs(-5): 5
Ceiling(4.2): 5
Floor(4.8): 4
Natural Log(2.718): 0,999896315728952
Log(1000): 3
Cos(0): 1
Sin(0): 0
Tan(45 degrees): 0,9999999999999999
SquareRoot(16): 4
Power(2, 3): 8
Round(4.6): 5
Max(10, 20): 20
Min(10, 20): 10
Remainder(10, 3): 1
Random number between 1 and 100: 37
45 degrees in radians: 0,7853981633974483
Pi radians in degrees: 180
Double to Decimal (123.456): 123,456
*/