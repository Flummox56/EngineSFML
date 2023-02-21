using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Timers;

namespace EngineSFML
{
    public class Picture
    {
        public static VideoMode vm = new VideoMode(1000, 500, 16);

        int spriteNum = 0;
        Text t = new Text("a", new Font("arial.ttf"), 30);

        Meter RpmMeter = new Meter(new Vector2f(875, 125), 85, "Rpm", 8000);
        Meter PowerMeter = new Meter(new Vector2f(625, 375), 85, "Power", 70);
        Meter TorqueMeter = new Meter(new Vector2f(875, 375), 85, "Torque", 150);

        List<Sprite> pistonSprites = new List<Sprite>();

        public Picture(uint maxFPS)
        {
            rw.SetFramerateLimit(maxFPS);
        }

        public RenderWindow rw = new RenderWindow(vm, "Целиндропляс 1.2.0 тест")
        {
            Position = new Vector2i(3000, 0),
            Size = new Vector2u(1000, 500)
        };

        Sprite sprite = new Sprite()
        {
            Scale = new Vector2f(200, 400),
            Position = new Vector2f(50, 50)
        };

        public void makeSprites()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2i[] PoseVectMas = {new Vector2i(80, 10), new Vector2i(359, 10), new Vector2i(637, 10), new Vector2i(915, 10)};
                Vector2i[] SizeVectMas = {new Vector2i(200, 400), new Vector2i(200, 400), new Vector2i(200, 400), new Vector2i(200, 400)};

                IntRect ir = new IntRect(PoseVectMas[i], SizeVectMas[i]);

                Texture t = new Texture("231221632.png", ir);

                t.Smooth = true;

                Sprite sp = new Sprite(t);

                pistonSprites.Add(sp);
            }
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
            rw.Draw(sprite);

            RpmMeter.paint(rw);
            PowerMeter.paint(rw);
            TorqueMeter.paint(rw);
        }   

        public void updateShapes(Object sender, System.Timers.ElapsedEventArgs args)
        {
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

            t.Position = new Vector2f(500, 400);
            t.DisplayedString = "Rpm\n" + Program.eng.Rpm;

            RpmMeter.update(Program.eng.Rpm);
            PowerMeter.update(Program.eng.power);
            TorqueMeter.update(Program.eng.torque);
            
            this.updateSprite();
        }

        public void updatePicture()
        {
            System.Timers.Timer t2 = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 10,
                Enabled = true
            };
            t2.Elapsed += this.updateShapes;
        }

        public void updateSprite()
        {
            if (Program.eng.Rpm == 0)
            {
                Thread.Sleep(100);
            }
            else
            {
                double wait = (1000 / (Program.eng.Rpm * 2));
                Thread.Sleep((int)(wait));
                if (spriteNum < 3)
                {
                    spriteNum++;
                }
                else
                {
                    spriteNum = 0;
                }
            }

            sprite = pistonSprites[spriteNum];
        }
    }
}
