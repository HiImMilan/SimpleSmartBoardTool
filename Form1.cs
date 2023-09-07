using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SimpleSmartBoardTool
{
    public partial class Form1 : Form
    {
        bool _mouseHold;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseHold = true;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseHold = false;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseHold)
                Location = PointToScreen(e.Location);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // In case teacher had multiple screens, or simple using projector in Extend mode
            Screen currentScreen = Screen.FromControl(this);

            Bitmap screenshot = new Bitmap(currentScreen.Bounds.Width, currentScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(Point.Empty, Point.Empty, screenshot.Size); 
            }
            
            string path = Path.Combine(Application.StartupPath, DateTime.Now.ToString("dd-MM-yyy--HH-mm-ss") + ".png");

            screenshot.Save(path, ImageFormat.Png);
            System.Diagnostics.Process.Start("mspaint.exe", path);

        }

        private void seeSavedScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath);
        }
    }
}
