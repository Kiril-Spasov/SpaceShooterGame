using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;


namespace SpaceShooterGame
{
    public partial class FormSpaceShooter : Form
    {
        public FormSpaceShooter()
        {
            InitializeComponent();
            //Use double buffering to reduce the flicker.
            this.DoubleBuffered = true;
        }

        //Store images
        Image[] spaceShipImages = new Image[12];
        Image stones;
        Image rocket;
        Image background;
        Image spaceShip;
        Image explosion;

        //Used for random positions.
        Random r = new Random();

        SoundPlayer explosionSound = new SoundPlayer();

        //Used for spaceship animation counter.
        int count = 0;

        bool hit;

        //Store position and directions.
        int spaceShipX, spaceShipY;
        int rocketX, rocketY, rocketDirY;
        int stonesX, stonesY, stonesDirX, stonesDirY;
        int explosionX, explosionY;

        int score;
        int timeLeft;
        bool startFlag;
        bool gameOver;

        Font font = new Font("Verdana", 12, FontStyle.Bold);
        Font endGame = new Font("Verdana", 24, FontStyle.Bold);
        SolidBrush brush = new SolidBrush(Color.Yellow);

        private void FormSpaceShooter_Load(object sender, EventArgs e)
        {
            //Load images.
            for (int i = 0; i <= 11; i++)
            {
                spaceShipImages[i] = Image.FromFile(Application.StartupPath + @"\space-ship-images\spaceship" + i.ToString() + "-removebg-preview.png");
            }
            spaceShip = spaceShipImages[0];

            background = Image.FromFile(Application.StartupPath + @"\space-background.png");
            stones = Image.FromFile(Application.StartupPath + @"\stones.png");
            rocket = Image.FromFile(Application.StartupPath + @"\rocket.png");
            explosion = Image.FromFile(Application.StartupPath + @"\explosion.png");

            //Load sound.
            explosionSound.SoundLocation = Application.StartupPath + @"\explosion.wav";
            explosionSound.LoadAsync();

            //Set start pos for the spaceship.
            spaceShipX = this.Width / 3;
            spaceShipY = this.Height - spaceShip.Height - 75;

            //Set pos of the screen.
            rocketX = -100;

            //Direction is set to 0 until we pres the button to shoot.
            rocketDirY = 0;

            //Set pos off the screen.
            explosionX = -100;

            //Set random initial X pos.
            stonesX = r.Next(0, this.Width);
            stonesY = 0;

            //Set random initial X direction.
            stonesDirX = r.Next(-15, 15);
            stonesDirY = 15;

            hit = false;
            startFlag = false;
        }

        private void FormSpaceShooter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGame(g);
        }

        private void DrawGame(Graphics g)
        {
            g.DrawImage(background, 0, 0);
            g.DrawString($"Score: " + score.ToString(), font, brush, 400, 10);
            g.DrawString($"Time left: " + timeLeft.ToString(), font, brush, 500, 10);

            g.DrawImage(spaceShip, spaceShipX, spaceShipY, 200, 150);
            g.DrawImage(stones, stonesX, stonesY);
            g.DrawImage(rocket, rocketX, rocketY, 45, 45);
            
            if (hit)
            {
                g.DrawImage(explosion, explosionX, explosionY, 45, 45);
                explosionSound.Play();
                //Set the initial values.
                Initialize();
            }

            if (gameOver)
            {
                g.DrawString($"GAME OVER", endGame, brush, 240, 180);
                g.DrawString($"(Press Start button to play again.)", font, brush, 200, 230);
            }
        }

        private void Initialize()
        {
            rocketX = -100;
            rocketDirY = 0;

            explosionX = -100;

            stonesX = r.Next(0, 500);
            stonesY = 0;

            stonesDirX = r.Next(-15, 15);
            stonesDirY = 15;

            hit = false;
        }

        private void FormSpaceShooter_KeyDown(object sender, KeyEventArgs e)
        {
            if (startFlag)
            {
                if (e.KeyCode == Keys.A)
                {
                    spaceShipX -= 5;
                }
                else if (e.KeyCode == Keys.D)
                {
                    spaceShipX += 5;
                }

                if (e.KeyCode == Keys.W)
                {
                    rocketX = spaceShipX + 77;
                    rocketY = spaceShipY - 15;
                    rocketDirY = 12;
                }
            }
            Invalidate();
        }

        //Timer for rocket and stones movement.
        private void TimerRocket_Tick(object sender, EventArgs e)
        {  
            //Pos Y for the rocket is 0 until we press the button.
            rocketY += -rocketDirY;

            stonesX += stonesDirX;
            stonesY += stonesDirY;

            //Check if stones hit the wall, then bounce back.
            if (stonesX < 0)
            {
                stonesDirX = -stonesDirX;
            }
            else if (stonesX > ClientRectangle.Width - stones.Width)
            {
                stonesDirX = -stonesDirX;
            }
            else if (stonesY < 0)
            {
                stonesDirY = -stonesDirY;
            }
            else if (stonesY > ClientRectangle.Height - stones.Height)
            {
                stonesDirY = -stonesDirY;
            }

            //Check if rocket reached the top without collision.
            if (rocketY < 0)
            {
                rocketX = -100;
                rocketDirY = 0;
            }
            //Check for collision.
            else if ((rocketX < stonesX + stones.Width) && (rocketX + rocket.Width > stonesX) && (rocketY + rocket.Height > stonesY) && (stonesY + stones.Height > rocketY) && hit == false)
            {
                hit = true;
                score++;
                explosionX = stonesX;
                explosionY = stonesY;
            }
            Invalidate();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            score = 0;
            timeLeft = 60;
            TimerRocket.Enabled = true;
            TimerCountdown.Enabled = true;
            startFlag = true;
            gameOver = false;
            axWindowsMediaPlayer1.URL = Application.StartupPath + @"\spacesound.mp3";
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            GameStop();
            axWindowsMediaPlayer1.close();
            Initialize();
        }

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            if (timeLeft == 0)
            {
                GameStop();
                Initialize();
            }
        }

        private void GameStop()
        {
            score = 0;
            timeLeft = 60;
            gameOver = true;
            TimerRocket.Enabled = false;
            TimerCountdown.Enabled = false;
            startFlag = false;
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            TimerCountdown.Enabled = !TimerCountdown.Enabled;
            TimerRocket.Enabled = !TimerRocket.Enabled;
        }

        private void TimerSpaceShip_Tick(object sender, EventArgs e)
        {
            spaceShip = spaceShipImages[count];
            count++;
            if (count > 11)
                count = 0;

            Invalidate();
        }
    }
}
