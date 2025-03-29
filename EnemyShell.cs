using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace TankGame
{
    class EnemyShell
    {
        public string enemyDir;
        public int enemyShellLeft;
        public int enemyShellTop;

        private int speed = 10;
        private PictureBox enemyShell = new PictureBox();
        private Timer enemyShellTimer = new Timer();

        private static List<PictureBox> shellCounter = new List<PictureBox>();

        public void MakeEnemyShell(Form form)
        {
            if( shellCounter.Count > 0 )
            {
                return; 
            }

            enemyShell.BackColor = Color.White;
            enemyShell.Size = new Size(8, 8);
            enemyShell.Tag = "enemyShell";
            enemyShell.Left = enemyShellLeft;
            enemyShell.Top = enemyShellTop;
            enemyShell.BringToFront();

            form.Controls.Add(enemyShell);
            shellCounter.Add(enemyShell);

            enemyShellTimer.Interval = speed;
            enemyShellTimer.Tick += new EventHandler(ShellTimerEvent);
            enemyShellTimer.Start();

        }

        private void ShellTimerEvent(object sender, EventArgs e)
        {
            if (enemyDir == "left")
            {
                enemyShell.Left -= speed;
            }

            if (enemyDir == "right")
            {
                enemyShell.Left += speed;
            }

            if (enemyDir == "up")
            {
                enemyShell.Top -= speed;
            }

            if (enemyDir == "down")
            {
                enemyShell.Top += speed;
            }

            if (enemyShell.Left < 10 || enemyShell.Left > 930 || enemyShell.Top < 20 || enemyShell.Top > 680)
            {
                enemyShellTimer.Stop();
                enemyShellTimer.Dispose();
                shellCounter.Remove(enemyShell);
                enemyShell.Dispose();
                enemyShellTimer = null;
                enemyShell = null;

            }

        }

    }
}
