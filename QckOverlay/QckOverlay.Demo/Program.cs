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
        static void Main(string[] args)
        {
            Overlay overlay = new Overlay("Steam");
            overlay.Opacity = 0.8;
            overlay.FPS = 30;
            overlay.ChangeChecksPerSecond = 100;
            overlay.BeginRendering();

            Console.ReadKey(true);
        }
    }
}
