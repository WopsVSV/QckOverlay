using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using QckOverlay.Library;

namespace QckOverlay.Demo
{
    class Program
    {
        private static readonly Font CalibriFont = new Font("Calibri", 12, FontStyle.Bold);

        static void Main(string[] args)
        {
            try
            {
                // Creates the overlay object
                var overlay = new Overlay("processname")
                {
                    Opacity = 0.8,
                    FPS = 30,
                    CPS = 400,
                    AlwaysDraw = false
                };

                overlay.Paint += Overlay_Paint; // Assigned a paint event

                overlay.BeginRendering(); // Starts the rendering

            } catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            Console.ReadKey(true);
        }

        // Drawing paint event
        private static void Overlay_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, 8, 32, 148, 30);
            e.Graphics.DrawString("Hello world.", CalibriFont, Brushes.BlueViolet, 12, 36);
        }
    }
}
