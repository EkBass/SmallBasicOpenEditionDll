/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Text.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 13.10.24 Kristian Virtanen
 * License: See license.txt
 */

namespace SmallBasicOpenEditionDll
{
    class Program2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Text Class Test Application");

            // Test Case 1: Append two strings
            Console.WriteLine("\nTest Case 1: Append two strings");
            string result = Text.Append("Hello, ", "world!");
            Console.WriteLine($"Result: {result}");  // Output: "Hello, world!"

            // Test Case 2: Get the length of a string
            Console.WriteLine("\nTest Case 2: Get the length of a string");
            int length = Text.GetLength("Small Basic Open Edition");
            Console.WriteLine($"Length: {length}");  // Output: length of the string

            // Test Case 3: Check if subText is part of the main text
            Console.WriteLine("\nTest Case 3: Check if 'Basic' is part of 'Small Basic Open Edition'");
            bool isSubText = Text.IsSubText("Small Basic Open Edition", "Basic");
            Console.WriteLine($"Is 'Basic' part of the text?: {isSubText}");

            // Test Case 4: Check if text starts with and ends with a specific string
            Console.WriteLine("\nTest Case 4: Check if the text starts with 'Small' and ends with 'Edition'");
            bool startsWith = Text.StartsWith("Small Basic Open Edition", "Small");
            bool endsWith = Text.EndsWith("Small Basic Open Edition", "Edition");
            Console.WriteLine($"Starts with 'Small'?: {startsWith}");
            Console.WriteLine($"Ends with 'Edition'?: {endsWith}");

            // Test Case 5: Get a substring from the main text
            Console.WriteLine("\nTest Case 5: Get a substring from 'Small Basic Open Edition'");
            string substring = Text.GetSubText("Small Basic Open Edition", 7, 5);
            Console.WriteLine($"Substring (position 7, length 5): {substring}");  // Output: "Basic"

            // Test Case 6: Get a substring from a position to the end
            Console.WriteLine("\nTest Case 6: Get a substring from position 12 to the end");
            string substringToEnd = Text.GetSubTextToEnd("Small Basic Open Edition", 12);
            Console.WriteLine($"Substring from position 12 to end: {substringToEnd}");

            // Test Case 7: Find the position of a substring
            Console.WriteLine("\nTest Case 7: Find the position of 'Open' in 'Small Basic Open Edition'");
            int indexOfSubText = Text.GetIndexOf("Small Basic Open Edition", "Open");
            Console.WriteLine($"Position of 'Open': {indexOfSubText}");  // Output: 13 (1-based index)

            // Test Case 8: Convert a string to lowercase and uppercase
            Console.WriteLine("\nTest Case 8: Convert a string to lowercase and uppercase");
            string lowerCaseText = Text.ConvertToLowerCase("Small BASIC Open Edition");
            string upperCaseText = Text.ConvertToUpperCase("Small BASIC Open Edition");
            Console.WriteLine($"Lowercase: {lowerCaseText}");
            Console.WriteLine($"Uppercase: {upperCaseText}");

            // Test Case 9: Get a character from a Unicode code
            Console.WriteLine("\nTest Case 9: Get a character from Unicode code 65 (A)");
            char character = Text.GetCharacter(65);
            Console.WriteLine($"Character for code 65: {character}");  // Output: 'A'

            // Test Case 10: Get the Unicode code for a character
            Console.WriteLine("\nTest Case 10: Get the Unicode code for character 'Z'");
            int characterCode = Text.GetCharacterCode('Z');
            Console.WriteLine($"Unicode code for 'Z': {characterCode}");  // Output: 90

            Console.WriteLine("\nText Class Test Complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}

/*
 * Text Class Test Application

Test Case 1: Append two strings
Result: Hello, world!

Test Case 2: Get the length of a string
Length: 24

Test Case 3: Check if 'Basic' is part of 'Small Basic Open Edition'
Is 'Basic' part of the text?: True

Test Case 4: Check if the text starts with 'Small' and ends with 'Edition'
Starts with 'Small'?: True
Ends with 'Edition'?: True

Test Case 5: Get a substring from 'Small Basic Open Edition'
Substring (position 7, length 5): Basic

Test Case 6: Get a substring from position 12 to the end
Substring from position 12 to end:  Open Edition

Test Case 7: Find the position of 'Open' in 'Small Basic Open Edition'
Position of 'Open': 13

Test Case 8: Convert a string to lowercase and uppercase
Lowercase: small basic open edition
Uppercase: SMALL BASIC OPEN EDITION

Test Case 9: Get a character from Unicode code 65 (A)
Character for code 65: A

Test Case 10: Get the Unicode code for character 'Z'
Unicode code for 'Z': 90

Text Class Test Complete. Press any key to exit.
*/