using SpaceShooterGame.GameComponents.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooterGame.GameComponents
{
    public class BlueAndYellowRocket : Rocket
    {
        public BlueAndYellowRocket()
        {
            posX = 0;
            posY = 0;
            fired = false;
            image = Image.FromFile(Application.StartupPath + @"\rocket.png");
        }

        public override bool CheckForCollision(FlyingObject flyingObject) =>
            (posX < flyingObject.PosX + flyingObject.Image.Width) && (posX + image.Width > flyingObject.PosX) && (posY + image.Height > flyingObject.PosY) && (flyingObject.PosY + flyingObject.Image.Height > posY);
            

        public override void CheckIfTargetMissed()
        {
            if (posY < 0)
                SetIniatialState();
        }

        public override void MovePosY(int distanceInPixels)
        {
            if (fired == true)
            {
                posY += -distanceInPixels;
            }
        }

        public override void SetIniatialState()
        {
            posX = -100;
            fired = false;
        }
    }
}
