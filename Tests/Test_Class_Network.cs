/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Network.cs
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
            string content = Network.GetWebPageContents("http://example.com");
            Console.WriteLine(content);

            string fileName = "temp.txt";
            Network.DownloadFile(fileName, "https://huggingface.co/front/assets/huggingface_logo-noborder.svg");
        }
    }
}

/* OUTPUT
# Works fine
*/