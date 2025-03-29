using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace TankGame
{
    class Shell
    {
        public string direction;
        public int shellLeft;
        public int shellTop;

        private int speed = 20;
        private PictureBox shell = new PictureBox();
        private Timer shellTimer = new Timer();

        public void MakeShell(Form form)
        {

            shell.BackColor = Color.White;
            shell.Size = new Size(8, 8);
            shell.Tag = "shell";
            shell.Left= shellLeft;
            shell.Top= shellTop;
            shell.BringToFront();

            form.Controls.Add(shell);

            shellTimer.Interval = speed;
            shellTimer.Tick += new EventHandler(ShellTimerEvent);
            shellTimer.Start();

        }

        private void ShellTimerEvent(object sender, EventArgs e)
        {
            if(direction == "left")
            {
                shell.Left -= speed;
            }

            if(direction == "right")
            {
                shell.Left += speed;
            }

            if(direction == "up")
            {
                shell.Top -= speed;
            }

            if (direction == "down")
            {
                shell.Top += speed;
            }

            if(shell.Left < 10 || shell.Left > 930 || shell.Top < 20 || shell.Top > 680)
            {
                shellTimer.Stop();
                shellTimer.Dispose();
                shell.Dispose();
                shellTimer = null;
                shell= null;

            }

        }

    }
}
