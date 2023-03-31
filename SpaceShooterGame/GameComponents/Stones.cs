using SpaceShooterGame.GameComponents.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooterGame.GameComponents
{
    public class Stones : FlyingObject
    {
        public Stones()
        {
            image = Image.FromFile(Application.StartupPath + @"\stones.png");
            posX = 0;
            posY = 0;
            directionX = 0;
            directionY = 0;
        }

        public override void BounceOfTheFormBorders(int formWidth, int formHeight)
        {
            if (posX < 0)
            {
                ChangeDirectionX();
            }
            else if (posX > formWidth - image.Width)
            {
                ChangeDirectionX();
            }
            else if (posY < 0)
            {
                ChangeDirectionY();
            }
            else if (posY > formHeight - image.Height)
            {
                ChangeDirectionY();
            }
        }

        public override void ChangeDirectionX()
        {
            directionX = -directionX;
        }

        public override void ChangeDirectionY()
        {
            directionY = -directionY;
        }
    }
}
