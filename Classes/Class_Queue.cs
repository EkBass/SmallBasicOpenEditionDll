/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Queue.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods to create and manage multiple queues, identified by name, allowing values to be enqueued and dequeued.
 */

namespace SmallBasicOpenEditionDll.Classes
{
    public static class Queue
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

        // Dictionary to store multiple queues
        private static readonly Dictionary<string, Queue<object>> queues = [];

        public static bool EnqueueValue(string queueName, object value)
        {
            try
            {
                // Use TryGetValue to check if the queue exists
                if (!queues.TryGetValue(queueName, out Queue<object>? queue))
                {
                    queue = new Queue<object>();
                    queues[queueName] = queue;
                }

                queue.Enqueue(value);
                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static int GetCount(string queueName)
        {
            // Use TryGetValue to avoid extra lookup
            if (queues.TryGetValue(queueName, out Queue<object>? queue))
            {
                return queue.Count;
            }
            else
            {
                LastError = $"Queue with name {queueName} does not exist.";
                return -1;
            }
        }

        public static object? DequeueValue(string queueName)
        {
            // Use TryGetValue to check if the queue exists and is not empty
            if (queues.TryGetValue(queueName, out Queue<object>? queue) && queue.Count > 0)
            {
                LastError = null;
                return queue.Dequeue();
            }
            else
            {
                LastError = $"Queue with name {queueName} is either empty or does not exist.";
                return null;
            }
        }

        public static bool RemoveQueue(string queueName)
        {
            // Remove the ContainsKey check as Dictionary.Remove handles non-existent keys
            if (queues.Remove(queueName))
            {
                LastError = null;
                return true;
            }
            else
            {
                LastError = $"Queue with name '{queueName}' does not exist.";
                return false;
            }
        }
    }
}
