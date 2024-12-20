﻿/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Stack.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods to create and manage multiple stacks, identified by name, allowing values to be pushed and popped.
 */

namespace SmallBasicOpenEditionDll
{
    /// <summary>Provides methods to create and manage multiple stacks, identified by name, allowing values to be pushed and popped.</summary>
    public static class Stack
    {
        private static string? _lastError;

        public static string? LastError
        {
            get => _lastError;
            set
            {
                if (value != null)
                {
                    _lastError = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {value}";
                }
                else
                {
                    _lastError = null;
                }
            }
        }

        // Dictionary to store multiple stacks, identified by name
        private static readonly Dictionary<string, Stack<object>> stacks = [];

        /// <summary>Pushes a value onto the specified stack. If the stack does not exist, it is created.</summary>
        /// <param name="stackName">The name of the stack to push the value onto.</param>
        /// <param name="value">The value to push onto the stack.</param>
        public static bool PushValue(string stackName, object value)
        {
            try
            {
                // Use TryGetValue to avoid redundant lookup
                if (!stacks.TryGetValue(stackName, out Stack<object>? stack))
                {
                    stack = new Stack<object>();
                    stacks[stackName] = stack;
                }

                stack.Push(value);
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
        public static int GetCount(string stackName)
        {
            // Use TryGetValue to avoid multiple lookups
            if (stacks.TryGetValue(stackName, out Stack<object>? stack))
            {
                return stack.Count;
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
        public static object? PopValue(string stackName)
        {
            // Use TryGetValue to avoid redundant lookup
            if (stacks.TryGetValue(stackName, out Stack<object>? stack) && stack.Count > 0)
            {
                LastError = null;
                return stack.Pop();
            }
            else
            {
                LastError = $"Stack with name {stackName} is either empty or does not exist.";
                return null;
            }
        }

        /// <summary>Removes the stack with the specified name from the dictionary.</summary>
        /// <param name="stackName">The name of the stack to remove.</param>
        /// <returns>True if removed with success, otherwise false.</returns>
        public static bool RemoveStack(string stackName)
        {
            // Remove the ContainsKey check, since Remove handles this internally
            if (stacks.Remove(stackName))
            {
                LastError = null;
                return true;
            }
            else
            {
                LastError = $"Stack with name '{stackName}' does not exist.";
                return false;
            }
        }
    }
}
