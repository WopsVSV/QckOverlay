using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace QckOverlay.Library
{
    /// <summary>
    /// Class responsible for rendering the form on the screen.
    /// </summary>
    public class Renderer
    {
        private readonly OverlayForm overlayForm;
        private readonly WindowFixer windowFixer;
        private readonly Timer windowTimer;
        private readonly Timer windowDrawer;

        /// <summary>
        /// How many frames per second the overlay is going to be rendered with
        /// </summary>
        public int FPS
        {
            get => fps;
            set
            {
                if (value > 100) fps = 100;
                if (value < 1) fps = 1;
                fps = value;
                if (windowDrawer != null) windowDrawer.Interval = 1000 / fps;
            }
        }
        public int fps = 30;

        /// <summary>
        /// How many times per second the overlay is going to be checked for movements/resizes
        /// </summary>
        public int ChecksPerSecond {
            get => checksPerSecond;
            set {
                if (value > 100) checksPerSecond = 100;
                if (value < 1) checksPerSecond = 1;
                checksPerSecond = value;
                if (windowTimer != null) windowTimer.Interval = 1000 / checksPerSecond;
            }
        }
        private int checksPerSecond = 100;

        /// <summary>
        /// Checks if the renderer is rendering
        /// </summary>
        public bool IsRendering { get; set; } = false;

        /// <summary>
        /// Changes the opacity of the form
        /// </summary>
        public double Opacity
        {
            get => overlayForm.Opacity;
            set => overlayForm.Opacity = value;
        }

        /// <summary>
        /// Constructor for the renderer. Creates the new form and keeps it hidden.
        /// </summary>
        public Renderer(IntPtr windowHandle)
        {
            // Creates the overlay form
            overlayForm = new OverlayForm();
            overlayForm.ChangeSize(1, 1);
            overlayForm.SetTransparent();

            // Creates the window fixer and attaches it to the process' window and to the overlay
            windowFixer = new WindowFixer(overlayForm, windowHandle);
            windowTimer = new Timer();
            windowTimer.Interval = 10;
            windowTimer.Tick += windowFixer.Tick;

            // Creates the window drawer
            windowDrawer = new Timer();
            windowDrawer.Interval = 1000 / FPS;
            windowDrawer.Tick += delegate { overlayForm.Invalidate(); };
        }

        /// <summary>
        /// Starts rendering the overlay
        /// </summary>
        public void Start()
        {
            overlayForm.OverlayPaint += OverlayForm_OverlayPaint;
            windowTimer.Start();
            windowDrawer.Start();
            Application.Run(overlayForm);
        }

        private void OverlayForm_OverlayPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Aqua, 50,50,100,100);
        }

        /// <summary>
        /// Stops rendering the overlay
        /// </summary>
        public void Stop()
        {
            windowTimer.Stop();
            windowDrawer.Stop();
            Application.Exit();
        }
    }
}
