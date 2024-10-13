/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_ImageList.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 13.10.24 Kristian Virtanen
 * License: See license.txt
 */

namespace SmallBasicOpenEditionDll
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWindow.WriteLine("Loading image...");
            string imgName = ImageList.LoadImage("C:\\Users\\ekvir\\Pictures\\Screenshots\\test.png").GetAwaiter().GetResult();
            TextWindow.WriteLine("Loaded image: " + imgName);
            TextWindow.WriteLine("Image width: " + ImageList.GetWidthOfImage(imgName));
            TextWindow.WriteLine("Image height: " + ImageList.GetHeightOfImage(imgName));

            Image newImage = ImageList.GetImageByName(imgName);
            TextWindow.WriteLine(newImage.ToString());
        }
    }
}

/* OUTPUT
Loading image...
Loaded image: Image0
Image width: 906
Image height: 682
System.Drawing.Bitmap
*/