/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Timer.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides functionality for creating and controlling a timer, including setting intervals, pausing, and resuming the timer.
 */

using System;
using System.Timers;

namespace SmallBasicOpenEditionDll
{
    /// <summary>Provides functionality for creating and controlling a timer, including setting intervals, pausing, and resuming the timer.</summary>
    public static class Timer
    {
        // Backing field for LastError
        private static string? _lastError;

        /// <summary>Stores the last error message, if any operation fails.</summary>
        public static string? LastError
        {
            get => _lastError;
            set
            {
                // Only add a timestamp if the value is not null
                if (value != null)
                {
                    _lastError = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {value}";
                }
                else
                {
                    _lastError = null;  // Set to null without timestamp
                }
            }
        }


        private static System.Timers.Timer _timer = new();
        private static bool _isPaused = false;

        /// <summary>Event that is triggered when the timer ticks based on the specified interval.</summary>
        public static event Action? Tick;

        /// <summary>Gets or sets the interval for the timer in milliseconds. The interval must be between 10 and 100,000,000 milliseconds.</summary>
        /// <value>The interval for the timer in milliseconds.</value>
        public static dynamic Interval
        {
            get => _timer.Interval;
            set
            {
                if (value < 10 || value > 100000000)
                {
                    LastError = "Interval must be between 10 and 100000000 milliseconds.";
                    LastError = "Timer error. Interval invalid: " + value;
                }
                _timer.Interval = value;
                LastError = null;
            }
        }

        /// <summary>Static constructor to initialize the timer and set the Elapsed event handler.</summary>
        static Timer()
        {
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;  // Ensures the timer repeats
        }

        /// <summary>Starts or resumes the timer.</summary>
        public static void Resume()
        {
            if (_isPaused)
            {
                _isPaused = false;
                _timer.Start();
            }
            else if (!_timer.Enabled)
            {
                _timer.Start();
            }
        }

        /// <summary>Pauses the timer if it is currently running.</summary>
        public static void Pause()
        {
            if (_timer.Enabled)
            {
                _isPaused = true;
                _timer.Stop();
            }
        }

        /// <summary>Stops the timer completely, resetting its state.</summary>
        public static void Stop()
        {
            _isPaused = false;
            _timer.Stop();
        }

        /// <summary>Checks if the timer is currently running.</summary>
        public static bool IsRunning()
        {
            return _timer.Enabled && !_isPaused;
        }

        /// <summary>Executes the timer only once after the specified delay (in milliseconds).</summary>
        /// <param name="delay">The delay in milliseconds before the timer elapses.</param>
        public static void ExecuteOnce(int delay)
        {
            if (delay < 10 || delay > 100000000)
            {
                LastError = "Delay must be between 10 and 100000000 milliseconds.";
                return;
            }

            // Stop any existing timer activities and configure for one-time execution
            Stop();

            _timer.Interval = delay;
            _timer.AutoReset = false;  // Only trigger once
            _timer.Start();
        }

        /// <summary>Event handler that is triggered when the timer's Elapsed event occurs. This method invokes the <see cref="Tick"/> event.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="ElapsedEventArgs"/> that contains the event data.</param>
        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke();
        }
    }
}
