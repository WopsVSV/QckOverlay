using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace QckOverlay.Library
{
    /// <summary>
    /// Responsible for keeping the overlay and specified window at the same size and position
    /// </summary>
    public class WindowFixer
    {
        private OverlayForm overlay;
        private IntPtr windowHandle;

        /// <summary>
        /// Declares the overlay window and the attached-to window
        /// </summary>
        public WindowFixer(OverlayForm overlay, IntPtr windowHandle)
        {
            this.overlay = overlay;
            this.windowHandle = windowHandle;
        }

        /// <summary>
        /// Tick method for changing the size and/or position IF NECESSARY
        /// </summary>
        public void Tick(object sender, EventArgs eventArgs)
        {
            var windowRect = windowHandle.GetWindowRect();
            var overlayRect = overlay.Handle.GetWindowRect();

            //Console.WriteLine($"W-> X: {windowRect.X}   Y: {windowRect.Y}   W: {windowRect.Width}   H: {windowRect.Height}");

            // Check for same size
            if (overlay.Width != windowRect.Width || overlay.Height != windowRect.Height)
            {
                FormHelper.ChangeSize(overlay, windowRect.Width, windowRect.Height);
            }

            // Check for same position
            if (overlayRect.X != windowRect.X || overlayRect.Y != windowRect.Y)
            {
                FormHelper.RelocateForm(overlay.Handle, windowRect.X, windowRect.Y);
            }
        }
    }
}
