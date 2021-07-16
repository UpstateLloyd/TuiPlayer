using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace TuiPlayer
{
    class Gui
    {
        /// <summary>
        /// Builds the user interface
        /// </summary>
        public void Start() {
            Application.Init();                     // Creates an instance of MainLoop to process events etc.

            var top = Application.Top;
            var tframe = top.Frame;

            var win = new Window("TuiPlayer")       // Create top level window
            {
                X = 0,
                Y = 1,                              // Leave one row for the toplevel menu
                Width = Dim.Fill(),                 // Dim.Fill() will auto resize
                Height = Dim.Fill() - 1,            // Subtract 1 row for the statusbar
            };

            
            var menu = new MenuBar(new MenuBarItem[]    // Create the menubar.
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Open", "Open a music file", () => Application.RequestStop()),

                    new MenuItem("_Open Stream", "Open a stream", () => Application.RequestStop()),

                    new MenuItem("_Quit", "Exit MusicSharp", () => Application.RequestStop()),
                }),

                new MenuBarItem("_Help", new MenuItem[]
                {
                    new MenuItem("_About", string.Empty, () =>
                    {
                        MessageBox.Query("Based on Music Sharp 0.2.0", "\nMusic Sharp is a lightweight CLI\n music player written in C#.\n\nDeveloped by Mark-James McDougall\nand licensed under the GPL v3.\n ", "Close");
                    }),
                }),
            });        
            
            top.Add(win, menu);                     // Add the layout elements
            Application.Run();                      // Run the app
        }
    }
}
