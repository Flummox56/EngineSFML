using System;
using SFML.Graphics;
using SFML.System;

namespace EngineSFML
{
    class Indicator
    {

        CircleShape lamp = new CircleShape();

        Text text = new Text("", new Font("arial.ttf"), 15);

        public Indicator(Vector2f position, string title) 
        {
            lamp.Radius = 20;
            lamp.Origin = new Vector2f(lamp.Radius, lamp.Radius);
            lamp.Position = position;
            lamp.OutlineColor = Color.White;
            lamp.OutlineThickness = 5;
            lamp.FillColor = Color.Black;

            text.DisplayedString = title;
            text.Origin = new Vector2f(text.CharacterSize * text.DisplayedString.Length / (text.DisplayedString.Length / 2), text.CharacterSize);
            text.Position = new Vector2f(position.X, position.Y - lamp.Radius - (lamp.OutlineThickness * 2));
        }

        public void update(bool currValue)
        {
            if (currValue)
            {
                lamp.FillColor = Color.Green;
            }
            else
            {
                lamp.FillColor = Color.Red;
            }
        }

        public void paint(RenderWindow r)
        {
            r.Draw(lamp);
            r.Draw(text);
        }
    }
}
