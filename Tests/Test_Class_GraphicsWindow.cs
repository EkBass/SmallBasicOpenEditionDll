/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_GraphicsWindow.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 13.10.24 Kristian Virtanen
 * License: See license.txt
*/

namespace SmallBasicOpenEditionDll
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize and show the GraphicsWindow
            GraphicsWindow.Width = 480;
            GraphicsWindow.Height = 300;
            GraphicsWindow.BackgroundColor = "LightBlue";
            GraphicsWindow.CanResize = true;
            GraphicsWindow.Show();

            // Draw some shapes
            GraphicsWindow.PenColor = "Red";
            GraphicsWindow.PenWidth = 5;
            GraphicsWindow.DrawRectangle(100, 100, 200, 150);

            GraphicsWindow.BrushColor = "Green";
            GraphicsWindow.FillEllipse(300, 300, 200, 100);

            // Draw text
            GraphicsWindow.PenColor = "Black";
            GraphicsWindow.DrawText(400, 50, "Testing Graphics Window");

            // Event handlers for key and mouse actions
            GraphicsWindow.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    GraphicsWindow.Hide();
                    Application.Exit();
                }
                else
                {
                    Console.WriteLine($"Key Pressed: {e.KeyCode}");
                }
            };

            GraphicsWindow.MouseMove += (sender, e) =>
            {
                Console.WriteLine($"Mouse moved to X: {e.X}, Y: {e.Y}");
            };

            GraphicsWindow.MouseDown += (sender, e) =>
            {
                Console.WriteLine($"Mouse button clicked at X: {e.X}, Y: {e.Y}");
            };

            // Keep the application running
            Application.Run();
        }
    }
}
