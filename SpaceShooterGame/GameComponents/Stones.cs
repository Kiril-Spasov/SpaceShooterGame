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
    }
}
