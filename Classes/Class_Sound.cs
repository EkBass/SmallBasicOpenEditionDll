/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Sound.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * Provides methods for playing system sounds and audio files, as well as controlling playback such as stopping, pausing, and waiting for playback to complete.
 */

using System;
using System.Media;
using System.Windows.Media;
using System.Threading;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods for playing system sounds and audio files, as well as controlling playback such as stopping, pausing, and waiting for playback to complete.
    /// </summary>
    public static class Sound
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


        private static MediaPlayer mediaPlayer = new();
        private static bool isPlaying = false;

        /// <summary>Plays the system click sound (Asterisk).</summary>
        public static void PlayClick() => SystemSounds.Asterisk.Play();

        /// <summary>Plays the system click sound (Asterisk) and waits for approximately 1 second.</summary>
        public static void PlayClickAndWait()
        {
            PlayClick();
            Thread.Sleep(1000);
        }

        /// <summary>Plays the system chime sound (Beep).</summary>
        public static void PlayChime() => SystemSounds.Beep.Play();

        /// <summary>Plays the system chime sound (Beep) and waits for approximately 1 second.</summary>
        public static void PlayChimeAndWait()
        {
            PlayChime();
            Thread.Sleep(1000);
        }

        /// <summary>Plays the system chimes sound (Exclamation).</summary>
        public static void PlayChimes() => SystemSounds.Exclamation.Play();

        /// <summary>Plays the system chimes sound (Exclamation) and waits for approximately 1 second.</summary>
        public static void PlayChimesAndWait()
        {
            PlayChimes();
            Thread.Sleep(1000);
        }

        /// <summary>Plays the system bell ring sound (Hand).</summary>
        public static void PlayBellRing() => SystemSounds.Hand.Play();

        /// <summary>Plays the system bell ring sound (Hand) and waits for approximately 1 second.</summary>
        public static void PlayBellRingAndWait()
        {
            PlayBellRing();
            Thread.Sleep(1000);
        }

        /// <summary>Plays an audio file from the specified file path.</summary>
        /// <param name="filePath">The file path of the audio to play (supports local and network paths).</param>
        public static bool Play(string filePath)
        {
            try
            {
                if (isPlaying)
                {
                    Stop(); // Stop the current media if already playing
                }

                mediaPlayer.Open(new Uri((string)filePath, UriKind.RelativeOrAbsolute));
                mediaPlayer.Play();
                isPlaying = true;
                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Plays an audio file from the specified file path and waits for the audio to finish playing.</summary>
        /// <param name="filePath">The file path of the audio to play (supports local and network paths).</param>
        public static bool PlayAndWait(string filePath)
        {
            try
            {
                if (isPlaying)
                {
                    Stop(); // Stop the current media if already playing
                }

                // Open the media file without playing yet
                mediaPlayer.Open(new Uri((string)filePath, UriKind.RelativeOrAbsolute));

                // Wait until the media is loaded and its duration is available
                while (!mediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    Thread.Sleep(100); // Poll every 100ms to check if duration is available
                }

                // Get the total duration of the media
                TimeSpan duration = mediaPlayer.NaturalDuration.TimeSpan;

                // Start playing the media
                mediaPlayer.Play();
                isPlaying = true;

                // Sleep for the duration of the audio
                Thread.Sleep(duration);

                // Stop and clean up after the media finishes
                mediaPlayer.Stop();
                mediaPlayer.Close();
                isPlaying = false;
                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Stops the playback of an audio file.
        /// </summary>
        public static bool Stop()
        {
            try
            {
                if (isPlaying)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Close();
                    isPlaying = false;
                    LastError = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Error stopping file: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Pauses the playback of an audio file.
        /// </summary>
        public static bool Pause()
        {
            try
            {
                mediaPlayer.Pause();
                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = "Error pausing file: " + ex.Message;
                return false;
            }
        }
    }
}
