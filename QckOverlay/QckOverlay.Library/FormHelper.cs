using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        private static extern int GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out InternalRect lpInternalRect);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, Flags.SetWindowPosFlags uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct InternalRect
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public struct Rect
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public Rect(int x, int y, int w, int h)
            {
                X = x;
                Y = y;
                Width = w;
                Height = h;
            }
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
            try
            {
                int initialStyle = GetWindowLongPtr(form.Handle, -20);
                SetWindowLongPtr(form.Handle, -20, initialStyle | 0x80000 | 0x20);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                Console.WriteLine(e.ToString());
            }
           
        }

        /// <summary>
        /// Gets the _internalRect of a window
        /// </summary>
        public static Rect GetWindowRect(this IntPtr windowPtr)
        {
            GetWindowRect(windowPtr, out _internalRect);

            _rect.X = _internalRect.Left;
            _rect.Y = _internalRect.Top;
            _rect.Width = _internalRect.Right - _internalRect.Left + 1;
            _rect.Height = _internalRect.Bottom - _internalRect.Top + 1;

            return _rect;
        }
        private static InternalRect _internalRect;
        private static Rect _rect = new Rect(0,0,1,1); // Basic rect
    }
}
