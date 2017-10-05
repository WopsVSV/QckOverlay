using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QckOverlay.Library
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
        }

        public event PaintEventHandler OverlayPaint;

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            if (OverlayPaint != null) OverlayPaint(sender, e);
        }

    }
}
