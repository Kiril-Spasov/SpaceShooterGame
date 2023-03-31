using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;
using System.Globalization;
using SpaceShooterGame.GameComponents;
using SpaceShooterGame.GameComponents.Abstractions;

namespace SpaceShooterGame
{
    public partial class FormSpaceShooter : Form
    {
        private Spaceship _spaceship;
        private FlyingObject _flyingObject;
        public FormSpaceShooter(Spaceship spaceship, FlyingObject flyingObject)
        {
            InitializeComponent();
            //Use double buffering to reduce the flicker.
            this.DoubleBuffered = true;

            _spaceship = spaceship;
            _flyingObject = flyingObject;
        }

        //Store images
        Image rocket;
        Image background;
        Image explosion;


        //Used for random positions.
        Random r = new Random();

        SoundPlayer explosionSound = new SoundPlayer();

        //Used for spaceship animation counter.
        int animationCount = 0;

        bool hit;

        //Store position and directions.
        //int spaceShipX, spaceShipY;
        int rocketX, rocketY, rocketDirY;
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
            _spaceship.PosX = this.Width / 3;
            _spaceship.PosY = this.Height - _spaceship.PosY - 75;


            background = Image.FromFile(Application.StartupPath + @"\space-background.png");

            rocket = Image.FromFile(Application.StartupPath + @"\rocket.png");
            explosion = Image.FromFile(Application.StartupPath + @"\explosion.png");

            //Load sound.
            explosionSound.SoundLocation = Application.StartupPath + @"\explosion.wav";
            explosionSound.LoadAsync();

            //Set start pos for the spaceship.
            _spaceship.PosX = this.Width / 3;
            _spaceship.PosY = this.Height - _spaceship.SpaceshipImage.Height - 75;

            //Set pos of the screen.
            rocketX = -100;

            //Direction is set to 0 until we pres the button to shoot.
            rocketDirY = 0;

            //Set pos off the screen.
            explosionX = -100;

            //Set random initial X pos.
            _flyingObject.PosX = r.Next(0, this.Width);
            _flyingObject.PosY = 0;

            //Set random initial X direction.
            _flyingObject.DirectionX = r.Next(-15, 15);
            _flyingObject.DirectionY = 15;

            hit = false;
            startFlag = false;
        }

        private void FormSpaceShooter_Paint(object sender, PaintEventArgs e)
        {
            DrawGame(e.Graphics);
        }

        private void DrawGame(Graphics g)
        {
            g.DrawImage(background, 0, 0);

            g.DrawString($"Score: " + score.ToString(), font, brush, 400, 10);
            g.DrawString($"Time left: " + timeLeft.ToString(), font, brush, 500, 10);

            g.DrawImage(_spaceship.SpaceshipImage, _spaceship.PosX, _spaceship.PosY, 200, 150);

            g.DrawImage(_flyingObject.Image, _flyingObject.PosX, _flyingObject.PosY);

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

            _flyingObject.PosX = r.Next(0, 500);
            _flyingObject.PosY = 0;

            _flyingObject.DirectionX = r.Next(-15, 15);
            _flyingObject.DirectionY = 15;

            hit = false;
        }

        private void FormSpaceShooter_KeyDown(object sender, KeyEventArgs e)
        {
            if (startFlag)
            {
                if (e.KeyCode == Keys.A)
                {
                    _spaceship.MoveLeft(5);
                }
                else if (e.KeyCode == Keys.D)
                {
                    _spaceship.MoveRight(5);
                }

                if (e.KeyCode == Keys.W)
                {
                    rocketX = _spaceship.PosX + 77;
                    rocketY = _spaceship.PosY - 15;
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

            _flyingObject.PosX += _flyingObject.DirectionX;
            _flyingObject.PosY += _flyingObject.DirectionY;

            //Check if stones hit the wall, then bounce back.
            if (_flyingObject.PosX < 0)
            {
                _flyingObject.DirectionX = -_flyingObject.DirectionX;
            }
            else if (_flyingObject.PosX > ClientRectangle.Width - _flyingObject.Image.Width)
            {
                _flyingObject.DirectionX = -_flyingObject.DirectionX;
            }
            else if (_flyingObject.PosY < 0)
            {
                _flyingObject.DirectionY = -_flyingObject.DirectionY;
            }
            else if (_flyingObject.PosY > ClientRectangle.Height - _flyingObject.Image.Height)
            {
                _flyingObject.DirectionY = -_flyingObject.DirectionY;
            }

            //Check if rocket reached the top without collision.
            if (rocketY < 0)
            {
                rocketX = -100;
                rocketDirY = 0;
            }
            //Check for collision.
            else if ((rocketX < _flyingObject.PosX + _flyingObject.Image.Width) && (rocketX + rocket.Width > _flyingObject.PosX) && (rocketY + rocket.Height > _flyingObject.PosY) && (_flyingObject.PosY + _flyingObject.Image.Height > rocketY) && hit == false)
            {
                hit = true;
                score++;
                explosionX = _flyingObject.PosX;
                explosionY = _flyingObject.PosY;
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
            _spaceship.SpaceshipImage = _spaceship.SpaceshipImages[animationCount];
            animationCount++;

            if (animationCount >= _spaceship.SpaceshipImages.Length - 1)
                animationCount = 0;

            Invalidate();
        }
    }
}
