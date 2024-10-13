/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_File.cs
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
            dynamic fileName = "test.txt";
            dynamic fileContents = "First line\nSecond Line\n\nFourth Line";
            TextWindow.WriteLine("Writing file:\n " + fileName + "\n" + fileContents + "\n\n");
            File.WriteContents(fileName, fileContents);

            TextWindow.WriteLine("Clearing string again and reading second line of file to it...\n\n");
            fileContents = "";
            dynamic lineNum = 2;
            fileContents = File.ReadLine(fileName, lineNum);
            TextWindow.WriteLine("Line 2 from file is: " + fileContents + "\n\n");

            string newLine = "New line here.";
            TextWindow.WriteLine("Writing new line: " + newLine);
            File.InsertLine(fileName, 2, newLine);

            string appendLine = "This line is append here.";
            TextWindow.WriteLine("Appending new line: " + appendLine);
            File.AppendContents(fileName, appendLine);

            string newFile = "test2.txt";
            TextWindow.WriteLine("Copying file...");
            File.CopyFile(fileName, newFile);

            TextWindow.WriteLine("Removing old file...");
            File.DeleteFile(fileName);

            fileContents = "";
            fileContents = File.ReadContents(newFile);
            TextWindow.WriteLine("Readed contents from new file:\n");
            TextWindow.WriteLine(fileContents + "\n\n");

            string direc = "New_Dir";
            TextWindow.WriteLine("Creating new directory...");
            File.CreateDirectory(direc);

            TextWindow.WriteLine("Listinf files and directories...");
            TextWindow.WriteLine(File.GetFiles("C:"));
            TextWindow.WriteLine(File.GetDirectories("C:"));

            TextWindow.WriteLine("Deleting temp directory");
            File.DeleteDirectory(direc);


        }
    }
}

/* OUTPUT
Writing file:
 test.txt
First line
Second Line

Fourth Line


Clearing string again and reading second line of file to it...


Line 2 from file is: Second Line


Writing new line: New line here.
Appending new line: This line is append here.
Copying file...
Removing old file...
Readed contents from new file:

First line
New line here.
Second Line

Fourth Line
This line is append here.



Creating new directory...
Listinf files and directories...
C:\SBOE_Tests.deps.json
C:\SBOE_Tests.dll
C:\SBOE_Tests.exe
C:\SBOE_Tests.pdb
C:\SBOE_Tests.runtimeconfig.json
C:\SmallBasicOpenEditionDll.dll
C:\SmallBasicOpenEditionDll.pdb
C:\test2.txt
C:\New_Dir
Deleting temp directory
*/