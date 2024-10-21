/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Text.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods for performing text manipulations such as concatenation, searching, case conversion, and substring operations.
 */

namespace SmallBasicOpenEditionDll.Classes
{
    /// <summary>
    /// Provides methods for performing text manipulations such as concatenation, searching, case conversion, and substring operations.
    /// </summary>
    public static class Text
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


        /// <summary>Appends two text inputs and returns the result.</summary>
        /// <param name="text1">The first text input.</param>
        /// <param name="text2">The second text input to append.</param>
        /// <returns>A string that is the result of appending text2 to text1.</returns>
        public static string? Append(string text1, string text2) => text1 + text2;

        /// <summary>Gets the length of the given text.</summary>
        /// <param name="text">The input text.</param>
        /// <returns>The length of the text.</returns>
        public static int GetLength(string text) => text.Length;

        /// <summary>Checks if subText is part of the main text.</summary>
        /// <param name="text">The main text to search within.</param>
        /// <param name="subText">The text to search for within the main text.</param>
        /// <returns><c>true</c> if subText is found within the main text; otherwise, <c>false</c>.</returns>
        public static bool IsSubText(string text, string subText) => text.Contains(subText);

        /// <summary>Checks if the text ends with the specified subText.</summary>
        /// <param name="text">The main text.</param>
        /// <param name="subText">The text to check if it's at the end of the main text.</param>
        /// <returns><c>true</c> if the main text ends with subText; otherwise, <c>false</c>.</returns>
        public static bool EndsWith(string text, string subText) => text.EndsWith(subText);

        /// <summary>Checks if the text starts with the specified subText.</summary>
        /// <param name="text">The main text.</param>
        /// <param name="subText">The text to check if it's at the beginning of the main text.</param>
        /// <returns><c>true</c> if the main text starts with subText; otherwise, <c>false</c>.</returns>
        public static bool StartsWith(string text, string subText) => text.StartsWith(subText);

        /// <summary>Gets a substring from the given text, starting at a specified position and with the given length.</summary>
        /// <param name="text">The input text.</param>
        /// <param name="start">The starting position (1-based index).</param>
        /// <param name="length">The number of characters to include in the substring.</param>
        /// <returns>The substring starting at the specified position with the specified length.</returns>
        public static string GetSubText(string text, int start, int length) => text.Substring(start - 1, length);

        /// <summary>Gets a substring from the given text, starting at a specified position and extending to the end of the text.</summary>
        /// <param name="text">The input text.</param>
        /// <param name="start">The starting position (1-based index).</param>
        /// <returns>The substring from the specified starting position to the end of the text.</returns>
        public static string GetSubTextToEnd(string text, int start) => text[(start - 1)..];

        /// <summary>Finds the position where subText first appears in the main text.</summary>
        /// <param name="text">The main text to search within.</param>
        /// <param name="subText">The text to find within the main text.</param>
        /// <returns>The 1-based index of the first occurrence of subText within the main text, or 0 if not found.</returns>
        public static int GetIndexOf(string text, string subText)
        {
            int index = text.IndexOf(subText);
            return index >= 0 ? index + 1 : 0;  // Return 0 if not found (1-based index)
        }

        /// <summary>Converts the given text to lowercase.</summary>
        /// <param name="text">The input text.</param>
        /// <returns>The input text converted to lowercase.</returns>
        public static string ConvertToLowerCase(string text) => text.ToLower();

        /// <summary>Converts the given text to uppercase.</summary>
        /// <param name="text">The input text.</param>
        /// <returns>The input text converted to uppercase.</returns>
        public static string ConvertToUpperCase(string text) => text.ToUpper();

        /// <summary>Gets a character corresponding to the specified Unicode code.</summary>
        /// <param name="characterCode">The Unicode code of the character.</param>
        /// <returns>The character that corresponds to the specified Unicode code.</returns>
        public static char GetCharacter(int characterCode) => (char)characterCode;

        /// <summary>Gets the Unicode code for the specified character.</summary>
        /// <param name="character">The character to get the Unicode code for.</param>
        /// <returns>The Unicode code of the specified character.</returns>
        public static int GetCharacterCode(dynamic character)
        {
            string cc = (string)character;

            // Ensure that the input is a single character
            if (cc == null || cc.Length != 1)
            {
                LastError = "Invalid parameter passed to 'Text.GetCharacterCode'. Expecting a single character, received: " + character;
                return 0;
            }

            // Reset any previous errors
            LastError = null;

            // Get the Unicode code of the first (and only) character
            return (int)cc[0];
        }


        /// <summary>Inverts the text passed as parameter.</summary>
        /// <param name="text">The text to invert.</param>
        /// <returns>Inverted text.</returns>
        public static string InvertText(string text) => new(text.Reverse().ToArray());

        /// <summary>Converts number as string. 123 > "123"</summary>
        /// <param name="text">The text to convert.</param>
        public static string Str(int number) => number.ToString();
    }
}
