using GameLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.SpaceShooterLibrary
{
    public class RedSpaceship : Shooter
    {
        public RedSpaceship()
        {
            shooterImages = new Image[12];

            for (int i = 0; i <= 11; i++)
            {
                shooterImages[i] = Image.FromFile(Environment.CurrentDirectory + @"\space-ship-images\spaceship" + i.ToString() + "-removebg-preview.png");
            }

            shooterImage = shooterImages[0];
        }

        public override void MoveLeft(int distanceInPixels)
        {
            posX -= distanceInPixels;
        }

        public override void MoveRight(int distanceInPixels)
        {
            posX += distanceInPixels;
        }

    }
}
