/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Text.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * Provides methods for performing text manipulations such as concatenation, searching, case conversion, and substring operations.
 */

using System;
using System.Linq;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods for performing text manipulations such as concatenation, searching, case conversion, and substring operations.
    /// </summary>
    public static class Text
    {
        /// <summary>Appends two text inputs and returns the result.</summary>
        /// <param name="text1">The first text input.</param>
        /// <param name="text2">The second text input to append.</param>
        /// <returns>A string that is the result of appending text2 to text1.</returns>
        public static dynamic? Append(dynamic text1, dynamic text2) => text1 + text2;

        /// <summary>Gets the length of the given text.</summary>
        /// <param name="text">The input text.</param>
        /// <returns>The length of the text.</returns>
        public static dynamic GetLength(dynamic text) => (string)text.Length;

        /// <summary>Checks if subText is part of the main text.</summary>
        /// <param name="text">The main text to search within.</param>
        /// <param name="subText">The text to search for within the main text.</param>
        /// <returns><c>true</c> if subText is found within the main text; otherwise, <c>false</c>.</returns>
        public static dynamic IsSubText(dynamic text, dynamic subText) => text.Contains(subText);

        /// <summary>Checks if the text ends with the specified subText.</summary>
        /// <param name="text">The main text.</param>
        /// <param name="subText">The text to check if it's at the end of the main text.</param>
        /// <returns><c>true</c> if the main text ends with subText; otherwise, <c>false</c>.</returns>
        public static dynamic EndsWith(dynamic text, dynamic subText) => text.EndsWith(subText);

        /// <summary>Checks if the text starts with the specified subText.</summary>
        /// <param name="text">The main text.</param>
        /// <param name="subText">The text to check if it's at the beginning of the main text.</param>
        /// <returns><c>true</c> if the main text starts with subText; otherwise, <c>false</c>.</returns>
        public static dynamic StartsWith(dynamic text, dynamic subText) => text.StartsWith(subText);

        /// <summary>Gets a substring from the given text, starting at a specified position and with the given length.</summary>
        /// <param name="text">The input text.</param>
        /// <param name="start">The starting position (1-based index).</param>
        /// <param name="length">The number of characters to include in the substring.</param>
        /// <returns>The substring starting at the specified position with the specified length.</returns>
        public static dynamic GetSubText(dynamic text, dynamic start, dynamic length) => text.Substring(start - 1, length);

        /// <summary>Gets a substring from the given text, starting at a specified position and extending to the end of the text.</summary>
        /// <param name="text">The input text.</param>
        /// <param name="start">The starting position (1-based index).</param>
        /// <returns>The substring from the specified starting position to the end of the text.</returns>
        public static dynamic GetSubTextToEnd(dynamic text, dynamic start) => text.Substring(start - 1);

        /// <summary>Finds the position where subText first appears in the main text.</summary>
        /// <param name="text">The main text to search within.</param>
        /// <param name="subText">The text to find within the main text.</param>
        /// <returns>The 1-based index of the first occurrence of subText within the main text, or 0 if not found.</returns>
        public static dynamic GetIndexOf(dynamic text, dynamic subText)
        {
            int index = text.IndexOf(subText);
            return index >= 0 ? index + 1 : 0;  // Return 0 if not found (1-based index)
        }

        /// <summary>Converts the given text to lowercase.</summary>
        /// <param name="text">The input text.</param>
        /// <returns>The input text converted to lowercase.</returns>
        public static dynamic ConvertToLowerCase(dynamic text) => text.ToLower();

        /// <summary>Converts the given text to uppercase.</summary>
        /// <param name="text">The input text.</param>
        /// <returns>The input text converted to uppercase.</returns>
        public static dynamic ConvertToUpperCase(dynamic text) => text.ToUpper();

        /// <summary>Gets a character corresponding to the specified Unicode code.</summary>
        /// <param name="characterCode">The Unicode code of the character.</param>
        /// <returns>The character that corresponds to the specified Unicode code.</returns>
        public static dynamic GetCharacter(dynamic characterCode) => (char)characterCode;

        /// <summary>Gets the Unicode code for the specified character.</summary>
        /// <param name="character">The character to get the Unicode code for.</param>
        /// <returns>The Unicode code of the specified character.</returns>
        public static dynamic GetCharacterCode(dynamic character) => character;

        /// <summary>Inverts the text passed as parameter.</summary>
        /// <param name="text"">The text to invert.</param>
        /// <returns>Inverted text.</returns>
        public static dynamic InvertText(dynamic text) => new string(((string)text).Reverse().ToArray());
    }
}
