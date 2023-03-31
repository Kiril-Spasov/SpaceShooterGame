using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooterGame.GameComponents.Abstractions
{
    public abstract class Spaceship
    {
        protected Image[] spaceshipImages;
        protected Image spaceshipImage;
        protected int posX = 0;
        protected int posY = 0;

        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
        public Image SpaceshipImage { get => spaceshipImage; set => spaceshipImage = value; }
        public Image[] SpaceshipImages { get => spaceshipImages; set => spaceshipImages = value; }


        public abstract void MoveLeft(int distanceInPixels);
        public abstract void MoveRight(int distanceInPixels);
    }
}
