using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QckOverlay.Library
{
    /// <summary>
    /// Class responsible for rendering the form on the screen.
    /// </summary>
    public class Renderer
    {
        private OverlayForm overlayForm;
        private WindowTimer windowTimer;

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
            
            // Creates the window timer and attaches it to the process' window and to the overlay
            windowTimer = new WindowTimer(overlayForm, windowHandle);
        }

        /// <summary>
        /// Starts rendering the overlay
        /// </summary>
        public void Start()
        {
            
        }

        /// <summary>
        /// Stops rendering the overlay
        /// </summary>
        public void Stop()
        {
            
        }
    }
}
