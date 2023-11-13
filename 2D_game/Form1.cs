using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D_game
{
    public enum PlayerAction
    {
        MoveUp,
        MoveRight,
        MoveDown,
        MoveLeft,
        NoAction,
        Fire
    }


    public partial class Form1 : Form
    {
        PlayerAction SolderAction { get; set; }
        PictureBox[] cloud;
        int backgroundspeed;
        Random rnd;
        int playerspeed;
        PictureBox[] bullets;
        int bulletspeed;
        PictureBox[] enemies;
        int sizeEnemy;
        int enemySpeed;
        public Form1()
        {
            InitializeComponent();
        }
        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < cloud.Length; i++)
            {
                cloud[i].Left += backgroundspeed;

                if (cloud[i].Left >= 1280)
                {
                    cloud[i].Left = cloud[i].Height;
                }
            }
            for (int i = cloud.Length; i < cloud.Length; i++)
            {
                cloud[i].Left += backgroundspeed - 10;

                if (cloud[i].Left >= 1280)
                {
                    cloud[i].Left = cloud[i].Left;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundspeed = 5;
            cloud = new PictureBox[14];
            rnd = new Random();
            playerspeed = 1;
            bullets = new PictureBox[1];
            bulletspeed = 10;
            enemies = new PictureBox[4];
            int sizeEnemy = rnd.Next(60, 80);
            enemySpeed = 3;

            Image easyEnemies = Image.FromFile("assets\\enemy.gif");

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new PictureBox();
                enemies[i].Size = new Size(sizeEnemy, sizeEnemy);
                enemies[i].SizeMode = PictureBoxSizeMode.Zoom;
                enemies[i].BackColor = Color.Transparent;
                enemies[i].Image = easyEnemies;
                enemies[i].Location = new Point(60 , 60);

                this.Controls.Add(enemies[i]);
            }

            for (int i =0; i < bullets.Length; i++)
            {
                bullets[i] = new PictureBox();
                bullets[i].BorderStyle = BorderStyle.None;
                bullets[i].Size = new Size(20, 5);
                bullets[i].BackColor = Color.White;

                this.Controls.Add(bullets[i]);
            }
            
            for (int i = 0; i < cloud.Length; i++)
            {
                cloud[i] = new PictureBox();
                cloud[i].BorderStyle = BorderStyle.None;
                cloud[i].Location = new Point(rnd.Next(-1000,1280),rnd.Next(28,80));
                if (i % 2 == 1)
                {
                    cloud[i].Size = new Size(rnd.Next(100, 230), rnd.Next(30, 75));
                    cloud[i].BackColor = Color.FromArgb(rnd.Next(50, 125), 240, 240, 240);
                }
                else
                {
                    cloud[i].Size = new Size(150,25);
                    cloud[i].BackColor = Color.FromArgb(rnd.Next(50, 125), 101, 67, 33);
                }
                this.Controls.Add(cloud[i]);
            }
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            

            Image animation = null;

            if (SolderAction == PlayerAction.NoAction)
            {
                animation = Properties.Resources._101;

            }
            else if (SolderAction == PlayerAction.MoveLeft)
            {
                myPlayer.Left -= playerspeed;
                animation = Properties.Resources._102;

            }
            else if (SolderAction == PlayerAction.MoveRight)
            {
                myPlayer.Left += playerspeed;
                animation = Properties.Resources._102;
            }
            else if (SolderAction == PlayerAction.Fire)
            {
                animation = Properties.Resources._103;
            }
            
            myPlayer.Image = animation;


        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (myPlayer.Left < 1150)
            {
                myPlayer.Left += playerspeed;
            }
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
           myPlayer.Top -= playerspeed;
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            myPlayer.Top += playerspeed;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.Left)
            {
                SolderAction = PlayerAction.MoveLeft;
                //LeftMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SolderAction = PlayerAction.MoveRight;
                //RightMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SolderAction = PlayerAction.MoveUp;
                //UpMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Down)
            {
                SolderAction = PlayerAction.MoveDown;
                //DownMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Space)
            {
                SolderAction = PlayerAction.Fire;
                //for (int i = 0; i < bullets.Length; i++)
                //{
                //    if (bullets[i].Left >1280)
                //    {
                //        bullets[i].Location = new Point(myPlayer.Location.X + 100 + i * 50, myPlayer.Location.Y + 50);  
                //    }
                //}
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            SolderAction = PlayerAction.NoAction;

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            myPlayer.Size = new Size(300, 300);
        }

        private void MoveBulletsTimer_Tick(object sender, EventArgs e)
        {
            
            bullets[0].Left += bulletspeed;
            
        }

        private void MoveEnemiesTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemies(enemies, enemySpeed);
        }

        private void MoveEnemies(PictureBox[] enemies, int speed)
        {
            for (int i = 0; i <  enemies.Length; i++)
            {
                enemies[i].Left -= speed + (int)(Math.Sin(enemies[i].Left * Math.PI / 180)) + (int)(Math.Cos(enemies[i].Left * Math.PI / 180));
            }
        }
    }
}
