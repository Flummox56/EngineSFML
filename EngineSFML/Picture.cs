using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Timers;

namespace EngineSFML
{
    public class Picture
    {
        public static VideoMode vm = new VideoMode(1000, 500);
        public static RenderWindow rw = new RenderWindow(vm, "It works!")
        {
            Position = new Vector2i(),
            Size = new Vector2u(1000, 500)
        };

        RectangleShape r = new RectangleShape()
        {
            Size = new Vector2f(200,400),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black),
            OutlineColor = new Color(Color.White)
        };

        CircleShape c = new CircleShape()
        {
            Radius = 135,
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black)
        };

        RectangleShape pointer = new RectangleShape()
        {
            Size = new Vector2f(3, 135),
            //Position = Program.pic.c.Position,
            Rotation = 90,
            FillColor = new Color(Color.Red),
            OutlineThickness = 2,
            OutlineColor = new Color(Color.Red)
        };

        public Picture(uint maxFPS)
        {
            rw.SetFramerateLimit(maxFPS);
        }
        
        public void frameChange()
        {
            while (rw.IsOpen)
            {
                rw.DispatchEvents();

                frameRedraw();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    rw.Close();
                }

                rw.Display();
                rw.Clear();
            }
        }

        public void frameRedraw()
        {
            r.Position = new Vector2f(
                (rw.Size.X / 4) - (r.Size.X / 2), 
                (rw.Size.Y / 2) - (r.Size.Y / 2)
                );
            rw.Draw(r);

            c.Position = new Vector2f(
                (rw.Size.X / 4 * 3) - c.Radius,
                (rw.Size.Y / 2) - c.Radius
                );
            rw.Draw(c);

            pointer.Position = new Vector2f(
                c.Position.X + c.Radius, 
                c.Position.Y + c.Radius
                );
            rw.Draw(pointer);
        }

        public void updateShapes(Object sender, System.Timers.ElapsedEventArgs args)
        {
            //Console.WriteLine("2");

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) 
            {
                Program.eng.accelPressed = true;
            }
            else
            {
                Program.eng.accelPressed = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.RShift))
            {
                Program.eng.starterRotation = true;
            }
            else
            {
                Program.eng.starterRotation = false;
            }
            
            pointer.Rotation = (90 + (float)Program.eng.Rpm / 33.3f);
        }

        public void updatePicture()
        {
            System.Timers.Timer t2 = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 10,
                Enabled = true
            };
            t2.Elapsed += updateShapes;
        }
    }
}
