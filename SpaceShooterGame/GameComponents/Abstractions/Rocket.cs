using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooterGame.GameComponents.Abstractions
{
    public abstract class Rocket
    {
        private protected Image image;
        private protected int posX;
        private protected int posY;
        private protected bool fired;

        public Image Image { get => image; set => image = value; }
        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
        public bool Fired { get => fired; set => fired = value; }

        public abstract void MovePosY(int distanceInPixels);
        public abstract void CheckIfTargetMissed();
        public abstract void SetIniatialState();
        public abstract bool CheckForCollision(FlyingObject flyingObject);

    }
}
