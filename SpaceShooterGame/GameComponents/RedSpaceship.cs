using SpaceShooterGame.GameComponents.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooterGame.GameComponents
{
    public class RedSpaceship : Spaceship
    {
        public RedSpaceship()
        {
            spaceshipImages = new Image[12];

            for (int i = 0; i <= 11; i++)
            {
                spaceshipImages[i] = Image.FromFile(Environment.CurrentDirectory + @"\space-ship-images\spaceship" + i.ToString() + "-removebg-preview.png");
            }

            spaceshipImage = spaceshipImages[0];
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
