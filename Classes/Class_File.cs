/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_file.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods for file and directory operations, such as reading, writing, copying, and deleting files.
  */

using System.IO;
namespace SmallBasicOpenEditionDll.Classes
{
    /// <summary>Provides methods for file and directory operations, such as reading, writing, copying, and deleting files.</summary>
    public static class File
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


        /// <summary>Reads the contents of a file.</summary>
        public static string? ReadContents(string filePath)
        {
            LastError = null;
            try
            {
                return System.IO.File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Writes the contents to a file, overwriting the file if it exists.</summary>
        public static bool WriteContents(string filePath, string contents)
        {
            LastError = null;
            try
            {
                System.IO.File.WriteAllText(filePath, contents);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Reads a specific line from a file.</summary>
        public static string? ReadLine(string filePath, int lineNumber)
        {
            LastError = null;
            try
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                if (lineNumber >= 1 && lineNumber <= lines.Length)
                {
                    return lines[lineNumber - 1];
                }
                else
                {
                    LastError = "Invalid line number: " + lineNumber;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Writes a specific line in the file.</summary>
        public static bool WriteLine(string filePath, int lineNumber, string contents)
        {
            LastError = null;
            try
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                if (lineNumber >= 1 && lineNumber <= lines.Length)
                {
                    lines[lineNumber - 1] = contents;
                    System.IO.File.WriteAllLines(filePath, lines);
                    return true;
                }
                LastError = $"No such line number: {lineNumber} at file: {filePath}";
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Inserts a specific line in the file without overwriting existing lines.</summary>
        public static bool InsertLine(string filePath, int lineNumber, string contents)
        {
            LastError = null;
            try
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList(); // Convert to list to allow insertions

                if (lineNumber >= 1 && lineNumber <= lines.Count + 1) // Allow insertion at the end as well
                {
                    lines.Insert(lineNumber - 1, contents); // Insert the new line at the specified position
                    System.IO.File.WriteAllLines(filePath, lines); // Write the updated lines back to the file
                    return true;
                }
                LastError = $"Invalid line number: {lineNumber} at file: {filePath}";
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Appends contents to the end of a file.</summary>
        public static bool AppendContents(string filePath, string contents)
        {
            LastError = null;
            try
            {
                System.IO.File.AppendAllText(filePath, contents + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Copies a file from the source path to the destination path.</summary>
        public static bool CopyFile(string sourceFilePath, string destinationFilePath)
        {
            LastError = null;
            try
            {
                System.IO.File.Copy(sourceFilePath, destinationFilePath, true);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Deletes the specified file.</summary>
        public static bool DeleteFile(string filePath)
        {
            LastError = null;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                LastError = $"No such file: {filePath}";
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Creates a new directory at the specified path.</summary>
        public static bool CreateDirectory(string directoryPath)
        {
            LastError = null;
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    return true;
                }
                LastError = $"Directory already exists: {directoryPath}";
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Deletes the specified directory and all its contents.</summary>
        public static bool DeleteDirectory(string directoryPath)
        {
            LastError = null;
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                    return true;
                }
                LastError = $"No such directory: {directoryPath}";
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Retrieves all file paths in the specified directory.</summary>
        public static string? GetFiles(string directoryPath)
        {
            LastError = null;
            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                return string.Join(Environment.NewLine, files);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Retrieves all directory paths in the specified directory.</summary>
        public static string? GetDirectories(string directoryPath)
        {
            LastError = null;
            try
            {
                string[] directories = Directory.GetDirectories(directoryPath);
                return string.Join(Environment.NewLine, directories);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }
    }
}
