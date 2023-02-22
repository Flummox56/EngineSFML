using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace EngineSFML
{
    class Piston
    {
        public Piston()
        {

        }

        RectangleShape piston = new RectangleShape()
        {
            FillColor = new Color(Color.Black),
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
        };

        RectangleShape PistonRod = new RectangleShape()
        {

        };
    }
}
