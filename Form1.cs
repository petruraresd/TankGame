using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankGame
{
    public partial class Form1: Form
    {

        bool goUp, goDown, goLeft, goRight, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int score;
        int speed = 10;
        int ammo = 5;
        int enemySpeed = 1;
        int ammoCounter;
        Random randNum = new Random();
        List<PictureBox> enemyList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            RestartGame();

        }

        private void player_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                if(playerHealth > 100)
                {
                    playerHealth = 100;
                }

                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }

            if (playerHealth < 25)
            {
                DropHealth();
            }

            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Score: " + score;

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goUp == true && player.Top > 45)
            {
                player.Top -= speed;
            }
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }

            foreach ( Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "ammo")
                {
                    if(player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += randNum.Next(3,5);
                    }
                }

                if (x is PictureBox && (string)x.Tag == "health")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        playerHealth += 25;
                        if (playerHealth > 100)
                        {
                            playerHealth = 100;
                        }
                    }
                }


                if ( x is PictureBox && (string)x.Tag == "enemy")
                {

                    /*if(player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth--;
                    }*/

                    double distance = GetDistance(x.Left, x.Top, player.Left, player.Top);

                    string enemyFacing = "down";

                    int enemyQuadrant = GetQuadrant(x.Left, x.Top);
                    int playerQuadrant = GetQuadrant(player.Left, player.Top);

                    /*if (x.Top > player.Top) enemyFacing = "up";
                    if (x.Top < player.Top) enemyFacing = "down";
                    if (x.Left > player.Left) enemyFacing = "left";
                    if (x.Left < player.Left) enemyFacing = "right";*/


                    if (distance > 100)
                    {
                        if (x.Left > player.Left)
                        {
                            x.Left -= enemySpeed;
                            ((PictureBox)x).Image = Properties.Resources.eLeft;
                            enemyFacing = "left";
                        }

                        if (x.Top > player.Top)
                        {
                            x.Top -= enemySpeed;
                            ((PictureBox)x).Image = Properties.Resources.eUp;
                            enemyFacing = "up";
                        }

                        if (x.Left < player.Left)
                        {
                            x.Left += enemySpeed;
                            ((PictureBox)x).Image = Properties.Resources.eRight;
                            enemyFacing = "right";
                        }
                        
                        if (x.Top < player.Top)
                        {
                            x.Top += enemySpeed;
                            ((PictureBox)x).Image = Properties.Resources.eDown;
                            enemyFacing = "down";
                        }

                        switch (playerQuadrant)
                        {
                            case 1:
                                {
                                    if (enemyQuadrant == 2 || enemyQuadrant == 4)
                                    {
                                        enemyFacing = "left";
                                        ((PictureBox)x).Image = Properties.Resources.eLeft;
                                    }

                                    if (enemyQuadrant == 3)
                                    {
                                        enemyFacing = "up";
                                        ((PictureBox)x).Image = Properties.Resources.eUp;
                                    }

                                    if (enemyQuadrant == 1)
                                    {
                                        if (x.Left > player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eLeft;
                                            enemyFacing = "left";
                                        }

                                        if (x.Top > player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eUp;
                                            enemyFacing = "up";
                                        }

                                        if (x.Left < player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eRight;
                                            enemyFacing = "right";
                                        }

                                        if (x.Top < player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eDown;
                                            enemyFacing = "down";
                                        }
                                    }
                                    break;
                                }


                            case 2:
                                {
                                    if (enemyQuadrant == 1 || enemyQuadrant == 3)
                                    {
                                        enemyFacing = "right";
                                        ((PictureBox)x).Image = Properties.Resources.eRight;
                                    }


                                    if (enemyQuadrant == 4)
                                    {
                                        enemyFacing = "up";
                                        ((PictureBox)x).Image = Properties.Resources.eUp;
                                    }

                                    if (enemyQuadrant == 2)
                                    {
                                        if (x.Left > player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eLeft;
                                            enemyFacing = "left";
                                        }

                                        if (x.Top > player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eUp;
                                            enemyFacing = "up";
                                        }

                                        if (x.Left < player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eRight;
                                            enemyFacing = "right";
                                        }

                                        if (x.Top < player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eDown;
                                            enemyFacing = "down";
                                        }
                                    }
                                    break;
                                }


                            case 3:
                                {
                                    if (enemyQuadrant == 2 || enemyQuadrant == 4)
                                    {
                                        enemyFacing = "left";
                                        ((PictureBox)x).Image = Properties.Resources.eLeft;
                                    }

                                    if (enemyQuadrant == 1)
                                    {
                                        enemyFacing = "down";
                                        ((PictureBox)x).Image = Properties.Resources.eDown;
                                    }

                                    if (enemyQuadrant == 3)
                                    {
                                        if (x.Left > player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eLeft;
                                            enemyFacing = "left";
                                        }

                                        if (x.Top > player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eUp;
                                            enemyFacing = "up";
                                        }

                                        if (x.Left < player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eRight;
                                            enemyFacing = "right";
                                        }

                                        if (x.Top < player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eDown;
                                            enemyFacing = "down";
                                        }
                                    }
                                    break;
                                }


                            case 4:
                                {
                                    if (enemyQuadrant == 1 || enemyQuadrant == 3)
                                    {
                                        enemyFacing = "right";
                                        ((PictureBox)x).Image = Properties.Resources.eRight;
                                    }

                                    if (enemyQuadrant == 2)
                                    {
                                        enemyFacing = "down";
                                        ((PictureBox)x).Image = Properties.Resources.eDown;
                                    }

                                    if (enemyQuadrant == 4)
                                    {
                                        if (x.Left > player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eLeft;
                                            enemyFacing = "left";
                                        }

                                        if (x.Top > player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eUp;
                                            enemyFacing = "up";
                                        }

                                        if (x.Left < player.Left)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eRight;
                                            enemyFacing = "right";
                                        }

                                        if (x.Top < player.Top)
                                        {
                                            ((PictureBox)x).Image = Properties.Resources.eDown;
                                            enemyFacing = "down";
                                        }
                                    }
                                    break;
                                }
                        }
                        

                        if (distance < 500)
                        {
                            ShootEnemyShell(enemyFacing, x.Left + x.Width / 2, x.Top + x.Height / 2);
                        }

                    }
 

                }

                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "shell")
                    {
                        if (x is PictureBox && (string)x.Tag == "enemy" && x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            enemyList.Remove(((PictureBox)x));
                            MakeEnemy();

                            if (ammoCounter < 3)
                            {
                                DropAmmo(x.Left, x.Top);
                                ammoCounter++;
                            }
                        }
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "enemyShell")
                    {
                        if (player.Bounds.IntersectsWith(j.Bounds))
                        {
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            playerHealth -= 10;
                        }
                    }
                }

            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (gameOver == true )
            {
                return;
            }

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.pLeft;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.pRight;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.pUp;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.pDown;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;

            }

            if (e.KeyCode == Keys.Up)
            {
               goUp = false;

            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;

            }

            if(e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                int left = player.Left + (player.Width / 2);
                int top = player.Top + (player.Height / 2);
                ShootShell(facing, left, top);
            }

            if (e.KeyCode == Keys.Enter && gameOver == true )
            {
                RestartGame();
            }

        }

        private void ShootShell(string direction, int left, int top)
        {

            Shell shootShell = new Shell();
            shootShell.direction = direction;
            shootShell.shellLeft = left;
            shootShell.shellTop = top;
            shootShell.MakeShell(this);

        }

        private void ShootEnemyShell(string direction, int left, int top)
        {

            EnemyShell shootEnemyShell = new EnemyShell();
            shootEnemyShell.enemyDir = direction;
            shootEnemyShell.enemyShellLeft = left;
            shootEnemyShell.enemyShellTop = top;
            shootEnemyShell.MakeEnemyShell(this);

        }

        /*private void EnemyShoot(object sender, EventArgs e)
        {
            foreach (PictureBox enemy in enemyList)
            {
                string enemyFacing = "down";
                if (enemy.Top > player.Top)
                {
                    enemyFacing = "up";
                }
                if (enemy.Top < player.Top)
                {
                    enemyFacing = "down";
                }
                if (enemy.Left > player.Left)
                {
                    enemyFacing = "left";
                }
                if (enemy.Left < player.Left)
                {
                    enemyFacing = "right";
                }

                ShootEnemyShell(enemyFacing, enemy.Left + enemy.Width / 2, enemy.Top + enemy.Height / 2);
            }
        }*/


        private void MakeEnemy()
        {

            PictureBox enemy = new PictureBox();
            enemy.Tag = "enemy";
            enemy.Image = Properties.Resources.eDown;
            enemy.Left = randNum.Next(0, this.ClientSize.Width - enemy.Width);
            enemy.Top = randNum.Next(30, this.ClientSize.Height - enemy.Height);
            enemy.SizeMode = PictureBoxSizeMode.AutoSize;
            enemyList.Add(enemy);
            this.Controls.Add(enemy);
            player.BringToFront();

        }

        private void DropAmmo(int left, int top)
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = left;
            ammo.Top = top;
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();
           
        }

        private void DropHealth()
        {
            bool healthExists = false;
            foreach (Control i in this.Controls)
            {
                if (i is PictureBox && (string)i.Tag == "health")
                {
                    healthExists = true;
                    break;
                }
            }

            if (!healthExists)
            {
                PictureBox health = new PictureBox();
                health.Image = Properties.Resources.health;
                health.SizeMode = PictureBoxSizeMode.AutoSize;
                health.Left = randNum.Next(10, this.ClientSize.Width - health.Width);
                health.Top = randNum.Next(30, this.ClientSize.Height - health.Height);
                health.Tag = "health";
                this.Controls.Add(health);

                health.BringToFront();
                player.BringToFront();
            }
        }


        private void RestartGame()
        {

            player.Image = Properties.Resources.pUp;

            foreach (PictureBox i in enemyList)
            {
                this.Controls.Remove(i);
            }

            foreach (Control i in this.Controls)
            {
                if (i is PictureBox && ( (string)i.Tag == "ammo" || (string)i.Tag == "health") )
                {
                        this.Controls.Remove(i);
                        ((PictureBox)i).Dispose();
                }
            }

                enemyList.Clear();

            for( int i = 0; i < 1; i++)
            {
                MakeEnemy();
            }

            gameOver = false;
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;

            playerHealth = 100;
            score = 0;
            ammo = 5;

            GameTimer.Start();


        }

        private double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private int GetQuadrant(int x, int y)
        {
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            if (x < centerX && y < centerY)
                return 1;
            else if (x >= centerX && y < centerY)
                return 2;
            else if (x < centerX && y >= centerY)
                return 3;
            else
                return 4;
        }


    }
}
