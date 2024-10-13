/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Stack.cs
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
            Console.WriteLine("Stack Test Application");

            // Test case 1: Push values onto a new stack
            Console.WriteLine("\nTest Case 1: Push values onto stack 'numbers'");
            Stack.PushValue("numbers", 10);
            Stack.PushValue("numbers", 20);
            Stack.PushValue("numbers", 30);

            Console.WriteLine($"'numbers' stack count after pushing values: {Stack.GetCount("numbers")}");

            // Test case 2: Pop values from the stack
            Console.WriteLine("\nTest Case 2: Pop values from stack 'numbers'");
            Console.WriteLine($"Popped value: {Stack.PopValue("numbers")}");
            Console.WriteLine($"Popped value: {Stack.PopValue("numbers")}");
            Console.WriteLine($"'numbers' stack count after popping values: {Stack.GetCount("numbers")}");

            // Test case 3: Push values onto another stack
            Console.WriteLine("\nTest Case 3: Push values onto stack 'letters'");
            Stack.PushValue("letters", "A");
            Stack.PushValue("letters", "B");
            Stack.PushValue("letters", "C");

            Console.WriteLine($"'letters' stack count: {Stack.GetCount("letters")}");

            // Test case 4: Try to pop from an empty stack
            Console.WriteLine("\nTest Case 4: Pop remaining values from stack 'numbers'");
            try
            {
                Console.WriteLine($"Popped value: {Stack.PopValue("numbers")}");
                Console.WriteLine($"Popped value: {Stack.PopValue("numbers")}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Test case 5: Try to pop from a non-existent stack
            Console.WriteLine("\nTest Case 5: Try to pop from non-existent stack 'nonexistent'");
            try
            {
                Console.WriteLine($"Popped value: {Stack.PopValue("nonexistent")}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Test case 6: Check the count of a non-existent stack
            Console.WriteLine("\nTest Case 6: Get count of non-existent stack 'nonexistent'");
            try
            {
                Console.WriteLine($"'nonexistent' stack count: {Stack.GetCount("nonexistent")}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nStack Test Complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}

/*
Stack Test Application

Test Case 1: Push values onto stack 'numbers'
'numbers' stack count after pushing values: 3

Test Case 2: Pop values from stack 'numbers'
Popped value: 30
Popped value: 20
'numbers' stack count after popping values: 1

Test Case 3: Push values onto stack 'letters'
'letters' stack count: 3

Test Case 4: Pop remaining values from stack 'numbers'
Popped value: 10
Error: Stack with name numbers is either empty or does not exist.

Test Case 5: Try to pop from non-existent stack 'nonexistent'
Error: Stack with name nonexistent is either empty or does not exist.

Test Case 6: Get count of non-existent stack 'nonexistent'
Error: Stack with name nonexistent does not exist.

Stack Test Complete. Press any key to exit.
*/