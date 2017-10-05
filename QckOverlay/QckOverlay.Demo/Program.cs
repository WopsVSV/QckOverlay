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
        private static readonly Font myFont = new Font("Calibri", 12);

        static void Main(string[] args)
        {
            // Creates the overlay object ('Steam' is the process name)
            var overlay = new Overlay("Steam")
            {
                Opacity = 0.8,
                FPS = 30,
                ChangeChecksPerSecond = 200
            };

            overlay.Paint += Overlay_Paint; // Assigned a paint event

            overlay.BeginRendering(); // Starts the rendering

            Console.ReadKey(true);
        }

        // Whatever you want to draw in this paint event ^^
        private static void Overlay_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, 2, 2, 190, 32);
            e.Graphics.DrawString("QckOverlay Demonstration ", myFont, Brushes.BlueViolet, 8, 8);
        }
    }
}
