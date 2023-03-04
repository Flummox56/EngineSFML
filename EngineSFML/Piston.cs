
using SFML.Graphics;
using SFML.System;
using System.Linq.Expressions;

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

        bool tactInput = true;
        bool tactOutput = false;

        Vertex[] Cylinder = new Vertex[4];

        ConvexShape InletValve = new ConvexShape(6)
        {
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),
            
        };

        ConvexShape ExhaustValve = new ConvexShape(6)
        {
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),

        };

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

        bool DrawGas = false;

        RectangleShape HotGas = new RectangleShape()
        {
            FillColor = new Color(255, 163, 0),
            OutlineColor = new Color(255, 163, 0),
            OutlineThickness = 0
        };

        CircleShape Cranc = new CircleShape()
        {
            Radius = 20,
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),
        };

        CircleShape Counterweight = new CircleShape()
        {
            Radius = 55,
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),
            Origin = new Vector2f(55, 55)
        };

        public void construct()
        {
            Cranc.Origin = new Vector2f(Cranc.Radius, Cranc.Radius);
            
            piston.Size = new Vector2f(120, 50);
            piston.Position = new Vector2f(
                CenterPosition.X - (piston.Size.X) / 2,
                Cranc.Position.Y - 160
                );

            HotGas.Size = new Vector2f(piston.Size.X + 10, 1);
            HotGas.Position = new Vector2f(piston.Position.X - 5, 170);

            Counterweight.Position = CenterPosition;

            pistonRod.Size = new Vector2f(20, 120);
            pistonRod.Origin = new Vector2f(10, 0);

            InletValve.SetPoint(0, new Vector2f(20, 0));
            InletValve.SetPoint(1, new Vector2f(25, 0));
            InletValve.SetPoint(2, new Vector2f(25, 50));
            InletValve.SetPoint(3, new Vector2f(45, 70));
            InletValve.SetPoint(4, new Vector2f(0, 70));
            InletValve.SetPoint(5, new Vector2f(20, 50));
            InletValve.Origin = new Vector2f((InletValve.GetPoint(3).X - InletValve.GetPoint(4).X) / 2,
                InletValve.GetPoint(3).Y);

            ExhaustValve.SetPoint(0, new Vector2f(20, 0));
            ExhaustValve.SetPoint(1, new Vector2f(25, 0));
            ExhaustValve.SetPoint(2, new Vector2f(25, 50));
            ExhaustValve.SetPoint(3, new Vector2f(45, 70));
            ExhaustValve.SetPoint(4, new Vector2f(0, 70));
            ExhaustValve.SetPoint(5, new Vector2f(20, 50));
            ExhaustValve.Origin = new Vector2f((ExhaustValve.GetPoint(3).X - ExhaustValve.GetPoint(4).X) / 2,
                ExhaustValve.GetPoint(3).Y);

            InletValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4, 165);
            ExhaustValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4 * 3, 165);
        }

        public void update(double rpm)
        {
            DrawGas = false;
            a = (rpm * 360) / 6000;
            if (Program.eng.slowMode)
            {
                a /= Program.eng.slowСoefficient;
            }

            Cranc.Position = new Vector2f(
                CenterPosition.X + (float)(35 * Math.Sin(Math.PI * t / 180)),
                CenterPosition.Y - (float)(35 * Math.Cos(Math.PI * t / 180))
                );

            if (t >= 720)
            {
                t = 0;
            }
            else
            {
                if (t >= 0 && t < 180)
                {
                    if (t < 90)
                    {
                        InletValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4, (float)(165 + 0.3 * t));
                    }
                    else
                    {
                        InletValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4, (float)(192 - 0.3 * (t - 90)));
                    }
                }
                else if (t >= 540 && t < 720)
                {
                    if (t < 630)
                    {
                        ExhaustValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4 * 3, (float)(165 + 0.3 * (t - 540)));
                    }
                    else
                    {
                        ExhaustValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4 * 3, (float)(192 - 0.3 * (t - 630)));
                    }
                }
                else if (t >= 360 && t < 540)
                {
                    DrawGas = true;
                }
                else
                {
                    InletValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4, 165);
                    ExhaustValve.Position = new Vector2f(piston.Position.X + piston.Size.X / 4 * 3, 165);
                }

                t += a;
            }

            HotGas.Size = new Vector2f(HotGas.Size.X, piston.Position.Y - 166 - 5);

            piston.Position = new Vector2f(
                CenterPosition.X - (piston.Size.X) / 2,
                Cranc.Position.Y - 160
                );

            pistonRod.Position = new Vector2f(piston.Position.X + piston.Size.X / 2, piston.Position.Y + piston.Size.Y / 2);

            double ac = (Cranc.Position.Y ) - pistonRod.Position.Y;
            double bc = (Cranc.Position.X ) - pistonRod.Position.X;
            double d = bc / ac;

            pistonRod.Rotation = -(float)(Math.Atan(d) * 57.3);

            Cylinder[0] = new Vertex(new Vector2f(piston.Position.X - 5, 275));
            Cylinder[1] = new Vertex(new Vector2f(piston.Position.X - 5, 170));
            Cylinder[2] = new Vertex(new Vector2f(piston.Position.X + piston.Size.X + 5, 170));
            Cylinder[3] = new Vertex(new Vector2f(piston.Position.X + piston.Size.X + 5, 275));
        }

        public void paint(RenderWindow r)
        {
            r.Draw(Counterweight);
            r.Draw(pistonRod);
            r.Draw(Cranc);
            r.Draw(piston);
            if (DrawGas && Program.eng.woundUp)
            {
                r.Draw(HotGas);
            }
            r.Draw(Cylinder, PrimitiveType.LineStrip);
            r.Draw(InletValve);
            r.Draw(ExhaustValve);
        }
    }
}
