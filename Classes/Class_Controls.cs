/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Contolr.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Updated: 13th October 2024 Kristian Virtanen
 * License: See license.txt
 *
 * Description:
 * The Controls class provides functionality to create and manage elements (buttons and textboxes) within a GraphicsWindow.
 * It internally stores the created controls in a dictionary (controlList), using their unique names as keys. 
 * When adding a control, the class only returns the name of the control to external code, not the actual control object.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides functionality to create and manage UI controls (buttons, textboxes) in the GraphicsWindow.
    /// </summary>
    public static class Controls
    {
        private static Dictionary<string, Control> controlList = [];

        // Button and TextBox nullable reference types to store the last interacted controls
        private static Button? lastClickedButton = null;
        private static TextBox? lastTypedTextBox = null;

        /// <summary>
        /// Occurs when any button is clicked.
        /// </summary>
        public static event EventHandler? ButtonClicked;

        /// <summary>
        /// Occurs when text is typed into any textbox.
        /// </summary>
        public static event EventHandler? TextTyped;

        /// <summary>
        /// Gets the name of the last clicked button, or null if no button has been clicked.
        /// </summary>
        public static dynamic? LastClickedButton => lastClickedButton?.Name;

        /// <summary>
        /// Gets the name of the last text box that had text typed into it, or null if no text was typed.
        /// </summary>
        public static dynamic? LastTypedTextBox => lastTypedTextBox?.Name;

        /// <summary>
        /// Adds a button to the GraphicsWindow at the specified position.
        /// </summary>
        /// <param name="caption">The text to display on the button.</param>
        /// <param name="left">The x-coordinate where the button will be placed.</param>
        /// <param name="top">The y-coordinate where the button will be placed.</param>
        /// <returns>The name of the created button.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the GraphicsWindow is not initialized.</exception>
        public static dynamic AddButton(string caption, int left, int top)
        {
            Button button = new() { Text = caption, Location = new Point(left, top), Name = "Button" + controlList.Count };

            button.Click += (s, e) =>
            {
                lastClickedButton = button;
                ButtonClicked?.Invoke(button, EventArgs.Empty);
            };

            controlList[button.Name] = button;

            if (GraphicsWindow.drawingPanel == null)
            {
                throw new InvalidOperationException("Graphics window is not initialized.");
            }

            GraphicsWindow.drawingPanel.Controls.Add(button);
            return button.Name;  // Return the name as a string, not the object
        }

        /// <summary>
        /// Adds a single-line textbox to the GraphicsWindow at the specified position.
        /// </summary>
        /// <param name="left">The x-coordinate where the textbox will be placed.</param>
        /// <param name="top">The y-coordinate where the textbox will be placed.</param>
        /// <returns>The name of the created textbox.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the GraphicsWindow is not initialized.</exception>
        public static dynamic AddTextBox(int left, int top)
        {
            TextBox textBox = new() { Location = new Point(left, top), Name = "TextBox" + controlList.Count };

            textBox.TextChanged += (s, e) =>
            {
                lastTypedTextBox = textBox;
                TextTyped?.Invoke(textBox, EventArgs.Empty);
            };

            controlList[textBox.Name] = textBox;

            if (GraphicsWindow.drawingPanel == null)
            {
                throw new InvalidOperationException("Graphics window is not initialized.");
            }

            GraphicsWindow.drawingPanel.Controls.Add(textBox);
            return textBox.Name;  // Return the name as a string, not the object
        }

        /// <summary>
        /// Adds a multi-line textbox to the GraphicsWindow at the specified position.
        /// </summary>
        /// <param name="left">The x-coordinate where the textbox will be placed.</param>
        /// <param name="top">The y-coordinate where the textbox will be placed.</param>
        /// <returns>The name of the created multi-line textbox.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the GraphicsWindow is not initialized.</exception>
        public static dynamic AddMultiLineTextBox(int left, int top)
        {
            TextBox textBox = new() { Location = new Point(left, top), Name = "MultiLineTextBox" + controlList.Count, Multiline = true, Size = new Size(200, 100) };

            textBox.TextChanged += (s, e) =>
            {
                lastTypedTextBox = textBox;
                TextTyped?.Invoke(textBox, EventArgs.Empty);
            };

            controlList[textBox.Name] = textBox;

            if (GraphicsWindow.drawingPanel == null)
            {
                throw new InvalidOperationException("Graphics window is not initialized.");
            }

            GraphicsWindow.drawingPanel.Controls.Add(textBox);
            return textBox.Name;  // Return the name as a string, not the object
        }

        /// <summary>
        /// Gets the text from the specified textbox.
        /// </summary>
        /// <param name="textBoxName">The name of the textbox.</param>
        /// <returns>The text contained in the specified textbox.</returns>
        /// <exception cref="ArgumentException">Thrown if the textbox is not found.</exception>
        public static dynamic GetTextBoxText(string textBoxName)
        {
            if (controlList.TryGetValue(textBoxName, out var control) && control is TextBox textBox)
            {
                return textBox.Text;
            }
            throw new ArgumentException($"TextBox with name {textBoxName} not found.");
        }

        /// <summary>
        /// Sets the text in the specified textbox.
        /// </summary>
        /// <param name="textBoxName">The name of the textbox.</param>
        /// <param name="text">The text to set in the textbox.</param>
        /// <exception cref="ArgumentException">Thrown if the textbox is not found.</exception>
        public static void SetTextBoxText(string textBoxName, string text)
        {
            if (controlList.TryGetValue(textBoxName, out var control) && control is TextBox textBox)
            {
                textBox.Text = text;
            }
            else
            {
                throw new ArgumentException($"TextBox with name {textBoxName} not found.");
            }
        }

        /// <summary>
        /// Moves the specified control to the given coordinates.
        /// </summary>
        /// <param name="controlName">The name of the control.</param>
        /// <param name="x">The new x-coordinate for the control.</param>
        /// <param name="y">The new y-coordinate for the control.</param>
        /// <exception cref="ArgumentException">Thrown if the control is not found.</exception>
        public static void Move(string controlName, int x, int y)
        {
            if (controlList.TryGetValue(controlName, out var control))
            {
                control.Location = new Point(x, y);
            }
            else
            {
                throw new ArgumentException($"Control with name {controlName} not found.");
            }
        }

        /// <summary>
        /// Sets the size of the specified control.
        /// </summary>
        /// <param name="controlName">The name of the control.</param>
        /// <param name="width">The new width of the control.</param>
        /// <param name="height">The new height of the control.</param>
        /// <exception cref="ArgumentException">Thrown if the control is not found.</exception>
        public static void SetSize(string controlName, int width, int height)
        {
            if (controlList.TryGetValue(controlName, out var control))
            {
                control.Size = new Size(width, height);
            }
            else
            {
                throw new ArgumentException($"Control with name {controlName} not found.");
            }
        }
    }
}
