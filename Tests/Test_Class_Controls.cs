/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Controls.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 13.10.24 Kristian Virtanen
 * License: See license.txt
 */

namespace SmallBasicOpenEditionDll
{
    static class Program2
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize and show the GraphicsWindow
            GraphicsWindow.Width = 800;
            GraphicsWindow.Height = 600;
            GraphicsWindow.BackgroundColor = "LightGray";
            GraphicsWindow.CanResize = true;
            GraphicsWindow.Show();

            // Add a button and subscribe to its click event
            dynamic buttonName = Controls.AddButton("Click Me", 50, 50);
            Controls.ButtonClicked += (sender, e) =>
            {
                Console.WriteLine($"Button '{Controls.LastClickedButton}' was clicked!");
            };

            // Add a single-line textbox and subscribe to its text typed event
            dynamic textBoxName = Controls.AddTextBox(50, 120);
            Controls.TextTyped += (sender, e) =>
            {
                string typedText = Controls.GetTextBoxText(textBoxName);
                Console.WriteLine($"TextBox '{Controls.LastTypedTextBox}' content changed to: {typedText}");
            };

            // Add a multi-line textbox
            dynamic multiLineTextBoxName = Controls.AddMultiLineTextBox(50, 180);
            Controls.SetTextBoxText(multiLineTextBoxName, "This is a multi-line textbox.");

            // Handle button click to modify textbox
            Controls.ButtonClicked += (sender, e) =>
            {
                if (Controls.LastClickedButton == buttonName)
                {
                    Controls.SetTextBoxText(textBoxName, "Button Clicked!");
                    Console.WriteLine("Button click updated the textbox!");
                }
            };

            Program.Sleep(5);
            Controls.Move(multiLineTextBoxName, 300, 300);
            Console.WriteLine("Moved the multi-line textbox after 5 seconds.");

            // Keep the application running
            Application.Run();
        }
    }
}
