/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Control.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
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
    /// <summary>Provides functionality to create and manage UI controls (buttons, textboxes) in the GraphicsWindow.</summary>
    public static class Controls
    {
        // Backing field for LastError
        private static string? _lastError;

        /// <summary>Stores the last error message, if any operation fails.</summary>
        public static string? LastError
        {
            get => _lastError;
            private set
            {
                // Add a timestamp in "yyyy-MM-dd HH:mm:ss" format before the error message
                _lastError = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss}: {value}";
            }
        }

        // Each button and box gets unique name because of this babe.
        private static int _counter = 0;
        static int Counter()
        {
            return System.Threading.Interlocked.Increment(ref _counter);
        }


        // Checks is Panel available.
        private static bool IsGraphicsWindowInitialized()
        {
            if (GraphicsWindow.drawingPanel == null)
            {
                LastError = "Graphics window is not initialized.";
                return false;
            }
            return true;
        }


        // Initialize control list
        private static Dictionary<string, Control> controlList = [];

        // Nullable reference types to store the last interacted controls
        private static Button? lastClickedButton = null;
        private static TextBox? lastTypedTextBox = null;

        /// <summary>Occurs when any button is clicked.</summary>
        public static event EventHandler? ButtonClicked;

        /// <summary>Occurs when text is typed into any textbox.</summary>
        public static event EventHandler? TextTyped;

        /// <summary>Gets the name of the last clicked button, or null if no button has been clicked.</summary>
        public static string? LastClickedButton => lastClickedButton?.Name;

        /// <summary>Gets the name of the last text box that had text typed into it, or null if no text was typed.</summary>
        public static string? LastTypedTextBox => lastTypedTextBox?.Name;

        /// <summary>Adds a button to the GraphicsWindow at the specified position.</summary>
        public static string? AddButton(string caption, int left, int top)
        {
            LastError = null;
            if (!IsGraphicsWindowInitialized()) return null;

            try
            {
                string controlName = $"Button{Counter()}";
                Button button = new() { Text = caption, Location = new Point(left, top), Name = controlName };
                button.Click += (s, e) => { lastClickedButton = button; ButtonClicked?.Invoke(button, EventArgs.Empty); };
                controlList[button.Name] = button;

                GraphicsWindow.drawingPanel.Controls.Add(button);
                return button.Name;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Adds a single-line textbox to the GraphicsWindow at the specified position.</summary>
        public static string? AddTextBox(int left, int top)
        {
            LastError = null;
            if (!IsGraphicsWindowInitialized()) return null;

            try
            {
                string controlName = $"TextBox{Counter()}";
                TextBox textBox = new() { Location = new Point(left, top), Name = controlName };
                textBox.TextChanged += (s, e) => { lastTypedTextBox = textBox; TextTyped?.Invoke(textBox, EventArgs.Empty); };
                controlList[textBox.Name] = textBox;

                GraphicsWindow.drawingPanel.Controls.Add(textBox);
                return textBox.Name;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Adds a multi-line textbox to the GraphicsWindow at the specified position.</summary>
        public static string? AddMultiLineTextBox(int left, int top)
        { 
            int defaultSize = 200;
            return AddMultiLineTextBoxWithSize(left, top, defaultSize, defaultSize);
        }

        public static string? AddMultiLineTextBoxWithSize(int left, int top, int width, int height)
        {
            LastError = null;
            if (!IsGraphicsWindowInitialized()) return null;

            try
            {
                string controlName = $"MultiLineTextBox{Counter()}";
                TextBox textBox = new()
                {
                    Location = new Point(left, top),
                    Name = controlName,
                    Multiline = true,
                    Size = new Size(width, height) // Customizable size for multiline
                };

                textBox.TextChanged += (s, e) => { lastTypedTextBox = textBox; TextTyped?.Invoke(textBox, EventArgs.Empty); };
                controlList[textBox.Name] = textBox;

                GraphicsWindow.drawingPanel.Controls.Add(textBox);
                return textBox.Name;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Gets the text from the specified textbox.</summary>
        public static string? GetTextBoxText(string textBoxName)
        {
            LastError = null;

            if (controlList.TryGetValue(textBoxName, out var control) && control is TextBox textBox)
            {
                return textBox.Text;
            }
            else
            {
                LastError = $"TextBox with name {textBoxName} not found.";
                return null;
            }
        }

        /// <summary>Sets the text in the specified textbox.</summary>
        public static bool SetTextBoxText(string textBoxName, string text)
        {
            LastError = null;
            if (controlList.TryGetValue(textBoxName, out var control) && control is TextBox textBox)
            {
                textBox.Text = text;
                return true;
            }
            else
            {
                LastError = $"TextBox with name {textBoxName} not found.";
                return false;
            }
        }

        /// <summary>Moves the specified control to the given coordinates.</summary>
        public static bool Move(string controlName, int x, int y)
        {
            LastError = null;
            if (controlList.TryGetValue(controlName, out var control))
            {
                control.Location = new Point(x, y);
                return true;
            }
            else
            {
                LastError = $"Control with name {controlName} not found.";
                return false;
            }
        }

        /// <summary>Sets the size of the specified control.</summary>
        public static bool SetSize(string controlName, int width, int height)
        {
            LastError = null;
            if (controlList.TryGetValue(controlName, out var control))
            {
                control.Size = new Size(width, height);
                return true;
            }
            else
            {
                LastError = $"Control with name {controlName} not found.";
                return false;
            }
        }

        public static bool RemoveControl(string controlName)
        {
            LastError = null;
            
            try
            { 
                if (controlList.TryGetValue(controlName, out var control))
                {
                    if (control is Button button)
                    {
                        button.Click -= (s, e) => { lastClickedButton = button; ButtonClicked?.Invoke(button, EventArgs.Empty); };
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.TextChanged -= (s, e) => { lastTypedTextBox = textBox; TextTyped?.Invoke(textBox, EventArgs.Empty); };
                    }

                    GraphicsWindow.drawingPanel.Controls.Remove(control);
                    controlList.Remove(controlName);
                    return true;
                }
                else
                {
                    LastError = $"Control with name {controlName} not found.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

    }
}
