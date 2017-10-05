using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using QckOverlay.Library;

namespace QckOverlay.Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            var overlay = new Overlay();
            overlay.Attach("HxD");
            overlay.Opacity = 0.5;
            overlay.BeginRendering();
            
            
            Console.ReadKey(true);
        }
    }
}
