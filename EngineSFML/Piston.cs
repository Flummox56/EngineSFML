
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
            FillColor = new Color(Color.White),
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
        };

        RectangleShape pistonRod = new RectangleShape()
        {
            FillColor = new Color(Color.Black),
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
        };

        CircleShape Cranc = new CircleShape()
        {
            Radius = 20,
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),
        };

        public void construct()
        {
            Cranc.Position = new Vector2f(250 - Cranc.Radius, 375 - Cranc.Radius);

            piston.Size = new Vector2f(120, 50);
            piston.Position = new Vector2f(
                Cranc.Position.X - piston.Size.X / 4,
                Cranc.Position.Y - 150
                );

            pistonRod.Size = new Vector2f(20, 150);
            pistonRod.Origin = new Vector2f(10, 0);
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
                CenterPosition.X - (piston.Size.X) / 2,
                Cranc.Position.Y - 150
                );

            pistonRod.Position = new Vector2f(piston.Position.X + piston.Size.X / 2, piston.Position.Y + piston.Size.Y / 2);

            double ac = (Cranc.Position.Y + Cranc.Radius) - pistonRod.Position.Y;
            double bc = (Cranc.Position.X + Cranc.Radius) - pistonRod.Position.X;
            double d = bc / ac;

            pistonRod.Rotation = -(float)(Math.Atan(d) * 57.3);
        }

        public void paint(RenderWindow r)
        {
            r.Draw(pistonRod);
            r.Draw(Cranc);
            r.Draw(piston);
        }
    }
}
