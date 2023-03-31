using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace GameLibrary.Abstractions
{
    public abstract class Shooter
    {
        protected Image[] shooterImages;
        protected Image shooterImage;
        protected int posX = 0;
        protected int posY = 0;

        public Image[] SpaceshipAnimationImages { get => shooterImages; }
        public Image SpaceshipImage { get => shooterImage; set => shooterImage = value; }
        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }

        public abstract void MoveLeft(int distanceInPixels);
        public abstract void MoveRight(int distanceInPixels);

    }
}
