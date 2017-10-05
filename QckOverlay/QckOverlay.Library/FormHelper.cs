using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace QckOverlay.Library
{
    public static class FormHelper
    {
        #region P/Invoke
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern int GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, Flags.SetWindowPosFlags uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
        #endregion

        /// <summary>
        /// Relocates the form on the screen
        /// </summary>
        public static void RelocateForm(IntPtr handle, int x, int y)
        {
            SetWindowPos(handle, (IntPtr)0, x, y, 0, 0, Flags.SetWindowPosFlags.IgnoreZOrder | Flags.SetWindowPosFlags.IgnoreResize | Flags.SetWindowPosFlags.ShowWindow);
        }

        /// <summary>
        /// Resizes the form
        /// </summary>
        public static void ChangeSize(this Form form, int width, int height)
        {
            form.Size = new Size(width, height);
        }

        /// <summary>
        /// Makes the form transparent
        /// </summary>
        public static void SetTransparent(this Form form)
        {
            form.BackColor = Color.Wheat;
            form.TransparencyKey = Color.Wheat;
            int initialStyle = GetWindowLongPtr(form.Handle, -20);
            SetWindowLongPtr(form.Handle, -20, initialStyle | 0x80000 | 0x20);
        }
    }
}
