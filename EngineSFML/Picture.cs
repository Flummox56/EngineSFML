using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

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

        public Picture(uint maxFPS)
        {
            rw.SetFramerateLimit(maxFPS);
        }
        
        public void updatePicture()
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
            r.Position = new Vector2f((rw.Size.X / 4) - (r.Size.X / 2), (rw.Size.Y / 2) - (r.Size.Y / 2));
            rw.Draw(r);
            c.Position = new Vector2f((rw.Size.X / 4 * 3) - c.Radius, (rw.Size.Y / 2) - c.Radius);
            rw.Draw(c);
        }
    }
}
