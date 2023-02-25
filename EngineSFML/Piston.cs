
using SFML.Graphics;
using SFML.System;

namespace EngineSFML
{
    class Piston
    {
        public Piston()
        {
            construct();
        }

        Vector2f CenterPosition = new Vector2f(250, 375);

        double t = 0, a = 0;

        RectangleShape piston = new RectangleShape()
        {
            FillColor = new Color(Color.Black),
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
        };

        RectangleShape PistonRod = new RectangleShape()
        {
            
        };

        public CircleShape Cranc = new CircleShape()
        {
            Radius = 25,
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),
        };

        public void construct()
        {
            Cranc.Position = new Vector2f(250 - Cranc.Radius, 375 - Cranc.Radius);

            piston.Size = new Vector2f(130, 30);
            piston.Position = new Vector2f(
                Cranc.Position.X - piston.Size.X / 4,
                Cranc.Position.Y - 150
                );
        }

        public void update(double rpm)
        {
            a = (rpm * 360) / 6000 / 100;

            Cranc.Position = new Vector2f(
                CenterPosition.X - Cranc.Radius + (float)(45 * Math.Cos(t)),
                CenterPosition.Y - Cranc.Radius + (float)(45 * Math.Sin(t))
                );

            if (t >= 360)
            {
                t = 0;
            }
            else
            {
                t += a;
            }

            piston.Position = new Vector2f(
                CenterPosition.X - (piston.Size.X + piston.OutlineThickness * 2) / 2,
                Cranc.Position.Y - 150
    );
        }

        public void paint(RenderWindow r)
        {
            r.Draw(Cranc);
            r.Draw(piston);
        }
    }
}
