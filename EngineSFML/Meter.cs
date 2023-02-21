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

            divValue = maxValue / 270;
            title = name;
        }

        public CircleShape c = new CircleShape()
        {
            OutlineColor = new Color(Color.White),
            OutlineThickness = 5,
            FillColor = new Color(Color.Black)
        };

        public RectangleShape pointer = new RectangleShape()
        {
            Size = new Vector2f(2, 3),
            Rotation = 90,
            FillColor = new Color(Color.Red),
            OutlineThickness = 2,
            OutlineColor = new Color(Color.Red)
        };

        void construct(Vector2f position, int radius)
        {
            c.Position = new Vector2f(position.X - radius, position.Y - radius);
            c.Radius = radius;

            pointer.Size = new Vector2f(pointer.Size.X, radius);
            pointer.Position = c.Position + new Vector2f(radius, radius);
            pointer.Rotation = 45;
        }

        internal void update(double currValue)
        {
            pointer.Rotation = (45 + (float)currValue / divValue);

            int integerValue = (int)currValue;
            t.DisplayedString = title + " \n" + integerValue.ToString();

            t.Position = new Vector2f(
                        pointer.Position.X - (t.CharacterSize * (float)(this.title.Length / 2)),
                        pointer.Position.Y + t.CharacterSize);
        }

        public void paint(RenderWindow r)
        {
            r.Draw(c);
            r.Draw(pointer);
            r.Draw(t);
        }
    }
}
