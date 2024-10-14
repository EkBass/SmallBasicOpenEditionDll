/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Stack.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods to create and manage multiple stacks, identified by name, allowing values to be pushed and popped.
 */

using System;
using System.Collections.Generic;

namespace SmallBasicOpenEditionDll
{
    /// <summary>Provides methods to create and manage multiple stacks, identified by name, allowing values to be pushed and popped.</summary>
    public static class Stack
    {
        // Backing field for LastError
        private static string? _lastError;

        /// <summary>Stores the last error message, if any operation fails.</summary>
        public static string? LastError
        {
            get => _lastError;
            private set
            {
                // Add a timestamp in "yyyy-MM-dd HH:mm:ss" format before the error message
                _lastError = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {value}";
            }
        }

        // Dictionary to store multiple stacks, each identified by a name
        private static Dictionary<string, Stack<object>> stacks = [];

        /// <summary>Pushes a value onto the specified stack. If the stack does not exist, it is created.</summary>
        /// <param name="stackName">The name of the stack to push the value onto.</param>
        /// <param name="value">The value to push onto the stack.</param>
        public static bool PushValue(string stackName, object value)
        {
            try
            {
                if (!stacks.ContainsKey(stackName))
                {
                    // If the stack doesn't exist, create it
                    stacks[stackName] = new Stack<object>();
                }
                // Push the value onto the stack
                stacks[stackName].Push(value);

                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Gets the number of items in the specified stack.</summary>
        /// <param name="stackName">The name of the stack to get the count of items from.</param>
        /// <returns>The number of items in the stack.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified stack does not exist.</exception>
        public static int GetCount(string stackName)
        {
            if (stacks.ContainsKey(stackName))
            {
                return stacks[stackName].Count;
            }
            else
            {
                LastError = $"Stack with name {stackName} does not exist.";
                return -1;
            }
        }

        /// <summary>Pops the top value from the specified stack.</summary>
        /// <param name="stackName">The name of the stack to pop the value from.</param>
        /// <returns>The value popped from the top of the stack.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stack is either empty or does not exist.</exception>
        public static object? PopValue(string stackName)
        {
            if (stacks.ContainsKey(stackName) && stacks[stackName].Count > 0)
            {
                LastError = null;
                return stacks[stackName].Pop();
            }
            else
            {
                LastError = $"Stack with name {stackName} is either empty or does not exist.";
                return null;
            }
        }
    }
}
