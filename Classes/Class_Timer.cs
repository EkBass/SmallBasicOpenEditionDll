/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Timer.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 12th October 2024
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
        private static System.Timers.Timer _timer = new();
        private static bool _isPaused = false;

        /// <summary>Event that is triggered when the timer ticks based on the specified interval.</summary>
        public static event Action? Tick;

        /// <summary>Gets or sets the interval for the timer in milliseconds. The interval must be between 10 and 100,000,000 milliseconds.</summary>
        /// <value>The interval for the timer in milliseconds.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the interval is outside the valid range (10 to 100,000,000 milliseconds).</exception>
        public static dynamic Interval
        {
            get => _timer.Interval;
            set
            {
                if (value < 10 || value > 100000000)
                {
                    throw new ArgumentOutOfRangeException("Interval must be between 10 and 100000000 milliseconds.");
                }
                _timer.Interval = value;
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

        /// <summary>Event handler that is triggered when the timer's Elapsed event occurs. This method invokes the <see cref="Tick"/> event.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="ElapsedEventArgs"/> that contains the event data.</param>
        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke();
        }
    }
}
