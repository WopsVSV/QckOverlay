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
    /// <summary>
    /// The main overlay class which contains all needed functionality for the end-user
    /// </summary>
    public class Overlay
    {
        private Process process;
        private Renderer renderer;

        /// <summary>
        /// The graphics object used for drawing on the overlay
        /// </summary>
        public Graphics Graphics { get; set; }

        /// <summary>
        /// Whether or not the overlay is attached to a process
        /// </summary>
        public bool IsAttached => process != null;

        /// <summary>
        /// The attached window title
        /// </summary>
        public string AttachedWindowTitle => process.MainWindowTitle;

        /// <summary>
        /// Changes the opacity of the overlay
        /// </summary>
        public double Opacity
        {
            get { return renderer != null ? renderer.Opacity : 0; }
            set { if(renderer != null) renderer.Opacity = value; }
        }

        /// <summary>
        /// Enables the visual styles and creates the basic overlay form
        /// </summary>
        public Overlay()
        {
            // Enable features for the form to run properly
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
        public Overlay(string processName) : this() => Attach(processName);
        public Overlay(int processId) : this() => Attach(processId);

        /// <summary>
        /// Attaches to a running process, cannot be changed aftewards.
        /// </summary>
        public void Attach(Process proc)
        {
            // Checks if the process exists
            if (proc == null)
            {
                throw new Exception("Specified process is invalid.");
            }
            if (process != null)
            {
                throw new Exception($"Could not attach, since overlay is already attached to {process.ProcessName}.");
            }

            // Assigns the process
            process = proc;

            try
            {
                renderer = new Renderer(process.MainWindowHandle);
            }
            catch
            {
                throw new Exception("Could not create the renderer.");
            }
        }
        public void Attach(string processName)
        {
            // Checks if the process exists
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                throw new Exception("Could not find the specified process.");
            }

            Attach(processes[0]);
        }
        public void Attach(int processId)
        {
            // Checks if the process exists
            var process = Process.GetProcessById(processId);
            if (process == null)
            {
                throw new Exception("Could not find the specified process.");
            }

            Attach(process);
        }

        /// <summary>
        /// Begins the rendering of the overlay
        /// </summary>
        public void BeginRendering()
        {
            // Checks if it is attached & renderer is created
            if (renderer == null) {
                throw new Exception("Renderer not created. Please attach to a process.");
            }

            renderer.Start();
        }

        /// <summary>
        /// Stops the rendering of the overlay
        /// </summary>
        public void StopRendering()
        {
            if (renderer != null && renderer.IsRendering)
                renderer.Stop();
        }
    }
}

/* // FormHelper.RelocateForm(overlayForm.Handle, rect.Left, rect.Top);
 * //overlayForm.Size = new Size(rect.Right - rect.Left + 1, rect.Bottom - rect.Top + 1);
 *   //RECT rect;
            //GetWindowRect(process.MainWindowHandle, out rect);
              //Application.Run(overlayForm);
 * */
