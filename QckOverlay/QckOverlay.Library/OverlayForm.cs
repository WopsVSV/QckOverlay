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

        Font f = new Font("Consolas", 12);

        public event PaintEventHandler OverlayPaint;
        //public PaintEventHandler OverlayPaint;

        private long x = 0;

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            if (OverlayPaint != null) OverlayPaint(sender, e);

            e.Graphics.FillRectangle(Brushes.Black, 2, 2, 220, 32);
            e.Graphics.DrawString("This is the overlay :) " + x, f, Brushes.BlueViolet, 8, 8);
            x++;

        }

    }
}
