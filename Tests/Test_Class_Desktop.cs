/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Desktop.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 12.10.24 Kristian Virtanen
 * License: See license.txt
 */
 
using SmallBasicOpenEditionDll;
namespace SmallBasicOpenEditionDll
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWindow.WriteLine("Desktop height is " + Desktop.Height);
            TextWindow.WriteLine("Desktop width is: " + Desktop.Width);

            // Lets try to change the background image too
            Desktop.SetWallpaper("C:\\Users\\ekvir\\Pictures\\Screenshots\\test.png");
        }
    }
}

/* OUTPUT
Desktop height is 1080
Desktop width is: 2048
# Desktop background image changed as expected.
*/