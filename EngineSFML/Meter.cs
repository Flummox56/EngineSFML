using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSFML
{
    public class Meter
    {
        float divValue;
        string title;

        public Text t = new Text("", new Font("arial.ttf"), 15);

        public Meter(Vector2f position, int radius, string name, float maxValue)
        {
            construct(position, radius);

            divValue = maxValue / 300;
            title = name;
        }

        CircleShape c = new CircleShape()
        {
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black)
        };

        CircleShape c2 = new CircleShape()
        {
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black)
        };

        RectangleShape pointer = new RectangleShape()
        {
            Size = new Vector2f(2, 3),
            Rotation = 30,
            FillColor = new Color(Color.Red),
            OutlineThickness = 2,
            OutlineColor = new Color(Color.Red)
        };

        void construct(Vector2f position, int radius)
        {
            c.Position = new Vector2f(position.X - radius, position.Y - radius);
            c.Radius = radius;

            c2.Radius = radius / 2 + c2.OutlineThickness;
            c2.Position = new Vector2f(position.X - c2.Radius, position.Y - c2.Radius);

            pointer.Size = new Vector2f(pointer.Size.X, radius);
            pointer.Position = c.Position + new Vector2f(radius, radius);
            pointer.Rotation = 35;
        }

        internal void update(double currValue)
        {
            pointer.Rotation = (35 + (float)currValue / divValue);

            int integerValue = (int)currValue;
            t.DisplayedString = title + "\n" + integerValue.ToString();

            t.Position = new Vector2f(
                        pointer.Position.X - ((float)(t.DisplayedString.Length / 4) * t.CharacterSize),
                        pointer.Position.Y - t.CharacterSize
            );
        }

        internal void paint(RenderWindow r)
        {
            r.Draw(c);
            r.Draw(pointer);
            r.Draw(c2);
            r.Draw(t);
        }
    }
}
