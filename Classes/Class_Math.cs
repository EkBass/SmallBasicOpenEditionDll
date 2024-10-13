/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Math.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * Math class provides a collection of math functions and sych that allow users to perform a mathematical operations.
 * Operations include basic arithmetic, trigonometric calculations, logarithms, rounding, and random number generation.
 * */

using System;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides mathematical functions and constants, including basic arithmetic operations,
    /// trigonometric functions, and random number generation.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Gets the value of Pi (π).
        /// </summary>
        public static double Pi => System.Math.PI;

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="number">A number whose absolute value is to be determined.</param>
        /// <returns>The absolute value of the specified number.</returns>
        public static int Abs(dynamic number)
        {
            return System.Math.Abs(number);
        }

        /// <summary>
        /// Returns the smallest integer greater than or equal to the specified number.
        /// </summary>
        /// <param name="number">A number to round up.</param>
        /// <returns>The smallest integer greater than or equal to the specified number.</returns>
        public static int Ceiling(dynamic number)
        {
            return System.Math.Ceiling(number);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified number.
        /// </summary>
        /// <param name="number">A number to round down.</param>
        /// <returns>The largest integer less than or equal to the specified number.</returns>
        public static int Floor(dynamic number)
        {
            return System.Math.Floor(number);
        }

        /// <summary>
        /// Returns the natural logarithm (base e) of a number.
        /// </summary>
        /// <param name="number">A number whose natural logarithm is to be found.</param>
        /// <returns>The natural logarithm of the specified number.</returns>
        public static dynamic NaturalLog(dynamic number)
        {
            return System.Math.Log(number);
        }

        /// <summary>
        /// Returns the base-10 logarithm of a number.
        /// </summary>
        /// <param name="number">A number whose base-10 logarithm is to be found.</param>
        /// <returns>The base-10 logarithm of the specified number.</returns>
        public static dynamic Log(dynamic number)
        {
            return System.Math.Log10(number);
        }

        /// <summary>
        /// Returns the cosine of the specified angle (in radians).
        /// </summary>
        /// <param name="angle">An angle in radians.</param>
        /// <returns>The cosine of the specified angle.</returns>
        public static dynamic Cos(dynamic angle)
        {
            return System.Math.Cos(angle);
        }

        /// <summary>
        /// Returns the sine of the specified angle (in radians).
        /// </summary>
        /// <param name="angle">An angle in radians.</param>
        /// <returns>The sine of the specified angle.</returns>
        public static dynamic Sin(dynamic angle)
        {
            return System.Math.Sin(angle);
        }

        /// <summary>
        /// Returns the tangent of the specified angle (in radians).
        /// </summary>
        /// <param name="angle">An angle in radians.</param>
        /// <returns>The tangent of the specified angle.</returns>
        public static dynamic Tan(dynamic angle)
        {
            return System.Math.Tan(angle);
        }

        /// <summary>
        /// Returns the arcsine of the specified sine value, in radians.
        /// </summary>
        /// <param name="sinValue">A sine value.</param>
        /// <returns>The arcsine of the specified sine value, in radians.</returns>
        public static dynamic ArcSin(dynamic sinValue)
        {
            return System.Math.Asin(sinValue);
        }

        /// <summary>
        /// Returns the arccosine of the specified cosine value, in radians.
        /// </summary>
        /// <param name="cosValue">A cosine value.</param>
        /// <returns>The arccosine of the specified cosine value, in radians.</returns>
        public static dynamic ArcCos(dynamic cosValue)
        {
            return System.Math.Acos(cosValue);
        }

        /// <summary>
        /// Returns the arctangent of the specified tangent value, in radians.
        /// </summary>
        /// <param name="tanValue">A tangent value.</param>
        /// <returns>The arctangent of the specified tangent value, in radians.</returns>
        public static dynamic ArcTan(dynamic tanValue)
        {
            return System.Math.Atan(tanValue);
        }

        /// <summary>
        /// Converts the specified angle from radians to degrees.
        /// </summary>
        /// <param name="angle">An angle in radians.</param>
        /// <returns>The equivalent angle in degrees.</returns>
        public static dynamic GetDegrees(dynamic angle)
        {
            return angle * (180 / System.Math.PI);
        }

        /// <summary>
        /// Converts the specified angle from degrees to radians.
        /// </summary>
        /// <param name="angle">An angle in degrees.</param>
        /// <returns>The equivalent angle in radians.</returns>
        public static dynamic GetRadians(dynamic angle)
        {
            return angle * (System.Math.PI / 180);
        }

        /// <summary>
        /// Returns the square root of the specified number.
        /// </summary>
        /// <param name="number">A number whose square root is to be found.</param>
        /// <returns>The square root of the specified number.</returns>
        public static dynamic SquareRoot(dynamic number)
        {
            return System.Math.Sqrt(number);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="baseNumber">The base number.</param>
        /// <param name="exponent">The exponent to raise the base to.</param>
        /// <returns>The base number raised to the power of the exponent.</returns>
        public static dynamic Power(dynamic baseNumber, dynamic exponent)
        {
            return System.Math.Pow(baseNumber, exponent);
        }

        /// <summary>
        /// Rounds the specified number to the nearest integer.
        /// </summary>
        /// <param name="number">A number to round.</param>
        /// <returns>The nearest integer to the specified number.</returns>
        public static int Round(dynamic number)
        {
            return System.Math.Round(number);
        }

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        /// <param name="number1">The first number to compare.</param>
        /// <param name="number2">The second number to compare.</param>
        /// <returns>The greater of the two numbers.</returns>
        public static dynamic Max(dynamic number1, dynamic number2)
        {
            return System.Math.Max(number1, number2);
        }

        /// <summary>
        /// Returns the smaller of two numbers.
        /// </summary>
        /// <param name="number1">The first number to compare.</param>
        /// <param name="number2">The second number to compare.</param>
        /// <returns>The smaller of the two numbers.</returns>
        public static dynamic Min(dynamic number1, dynamic number2)
        {
            return System.Math.Min(number1, number2);
        }

        /// <summary>
        /// Returns the remainder of division of two numbers.
        /// </summary>
        /// <param name="dividend">The number to be divided.</param>
        /// <param name="divisor">The number by which to divide the dividend.</param>
        /// <returns>The remainder of the division.</returns>
        public static dynamic Remainder(dynamic dividend, dynamic divisor)
        {
            return dividend % divisor;
        }

        /// <summary>
        /// Generates a random number between 1 and the specified maximum number.
        /// </summary>
        /// <param name="maxNumber">The maximum number (inclusive) to generate.</param>
        /// <returns>A random integer between 1 and the specified maximum number.</returns>
        public static int GetRandomNumber(dynamic maxNumber)
        {
            Random random = new();
            return random.Next(1, (int)maxNumber + 1);
        }

        /// <summary>
        /// Converts a double-precision floating-point number to a decimal.
        /// </summary>
        /// <param name="number">A double-precision floating-point number to convert.</param>
        /// <returns>The equivalent decimal representation of the specified number.</returns>
        public static decimal DoubleToDecimal(dynamic number)
        {
            return Convert.ToDecimal(number);
        }
    }
}
