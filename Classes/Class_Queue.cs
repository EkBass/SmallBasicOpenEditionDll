/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Queue.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods to create and manage multiple queues, identified by name, allowing values to be enqueued and dequeued.
 */

using System;
using System.Collections.Generic;

namespace SmallBasicOpenEditionDll
{
    /// <summary>Provides methods to create and manage multiple queues, identified by name, allowing values to be enqueued and dequeued.</summary>
    public static class Queue
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

        // Dictionary to store multiple queues, each identified by a name
        private static Dictionary<string, Queue<object>> queues = [];

        /// <summary>Enqueues a value into the specified queue. If the queue does not exist, it is created.</summary>
        /// <param name="queueName">The name of the queue to enqueue the value into.</param>
        /// <param name="value">The value to enqueue into the queue.</param>
        public static bool EnqueueValue(string queueName, object value)
        {
            try
            {
                if (!queues.ContainsKey(queueName))
                {
                    // If the queue doesn't exist, create it
                    queues[queueName] = new Queue<object>();
                }
                // Enqueue the value into the queue
                queues[queueName].Enqueue(value);

                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Gets the number of items in the specified queue.</summary>
        /// <param name="queueName">The name of the queue to get the count of items from.</param>
        /// <returns>The number of items in the queue.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified queue does not exist.</exception>
        public static int GetCount(string queueName)
        {
            if (queues.ContainsKey(queueName))
            {
                return queues[queueName].Count;
            }
            else
            {
                LastError = $"Queue with name {queueName} does not exist.";
                return -1;
            }
        }

        /// <summary>Dequeues the front value from the specified queue.</summary>
        /// <param name="queueName">The name of the queue to dequeue the value from.</param>
        /// <returns>The value dequeued from the front of the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the queue is either empty or does not exist.</exception>
        public static object? DequeueValue(string queueName)
        {
            if (queues.ContainsKey(queueName) && queues[queueName].Count > 0)
            {
                LastError = null;
                return queues[queueName].Dequeue();
            }
            else
            {
                LastError = $"Queue with name {queueName} is either empty or does not exist.";
                return null;
            }
        }
    }
}
