/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Sound.cs
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
            TextWindow.WriteLine("Welcome to the Sound Test App!");

            // Play system sounds
            TextWindow.WriteLine("\nPlaying system sounds...");
            Sound.PlayClickAndWait();
            Sound.PlayChimeAndWait();
            Sound.PlayChimesAndWait();
            Sound.PlayBellRingAndWait();

            // Play a custom MP3 file (manually provide the path)
            string filePath = "C:\\Users\\ekvir\\Downloads\\mp3.mp3";


            if (filePath.ToLower() != "skip")
            {
                // Play the audio file
                TextWindow.WriteLine("Playing audio file...");
                Sound.Play(filePath);

                // Pause for 5 seconds to let the audio play
                TextWindow.WriteLine("Pausing for 5 seconds...");
                System.Threading.Thread.Sleep(5000);

                // Test pause functionality
                TextWindow.WriteLine("Pausing audio...");
                Sound.Pause();
                System.Threading.Thread.Sleep(2000); // Wait for 2 seconds while paused

                // Resume playing audio
                TextWindow.WriteLine("Resuming audio...");
                Sound.Play(filePath);

                // Stop audio after 5 seconds
                System.Threading.Thread.Sleep(5000);
                TextWindow.WriteLine("Stopping audio...");
                Sound.Stop();
            }

            // Test PlayAndWait functionality
            if (filePath.ToLower() != "skip")
            {
                TextWindow.WriteLine("\nTesting PlayAndWait method for the same MP3 file...");
                Sound.PlayAndWait(filePath);
            }

            TextWindow.WriteLine("\nSound test complete. Press any key to exit.");
            TextWindow.ReadKey();
        }
    }
}

/*
# Works fine
*/