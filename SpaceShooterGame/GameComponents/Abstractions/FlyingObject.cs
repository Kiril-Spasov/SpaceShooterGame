using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooterGame.GameComponents.Abstractions
{
    public abstract class FlyingObject
    {
        protected Image image;
        protected int posX;
        protected int posY;
        protected int directionX;
        protected int directionY;

        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
        public int DirectionX { get => directionX; set => directionX = value; }
        public int DirectionY { get => directionY; set => directionY = value; }

        public Image Image { get => image; set => image = value; }

        public abstract void BounceOfTheFormBorders(int formWidth, int formHeight);

        public abstract void ChangeDirectionX();

        public abstract void ChangeDirectionY();


    }
}
