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
        
        Piston p = new Piston();

        Indicator WoundUpIndicator = new Indicator(new Vector2f(562.5f, 187.5f), "Wound Up");
        Indicator IgnitionIndicator = new Indicator(new Vector2f(562.5f, 62.5f), "Ignition");
        Indicator SlowModeIndicator = new Indicator(new Vector2f(687.5f, 62.5f), "Slow Mode");
        Indicator StarterIndicator = new Indicator(new Vector2f(687.5f, 187.5f), "Starter");

        Meter RpmMeter = new Meter(new Vector2f(875, 125), 85, "Rpm", 8000);
        Meter PowerMeter = new Meter(new Vector2f(625, 375), 85, "Power", 200);
        Meter TorqueMeter = new Meter(new Vector2f(875, 375), 85, "Torque", 400);

        List<Sprite> pistonSprites = new List<Sprite>();

        Vertex[] Va = new Vertex[6];
        
        public Picture(uint maxFPS)
        {
            rw.SetFramerateLimit(maxFPS);

            Va[0] = new Vertex(new Vector2f(500, 0));
            Va[1] = new Vertex(new Vector2f(500, 500));
            Va[2] = new Vertex(new Vector2f(750, 0));
            Va[3] = new Vertex(new Vector2f(750, 500));
            Va[4] = new Vertex(new Vector2f(500, 250));
            Va[5] = new Vertex(new Vector2f(1000, 250));
        }

        public RenderWindow rw = new RenderWindow(vm, "Engine Simulation", Styles.Titlebar)
        {
            Position = new Vector2i(0, 0),
            Size = new Vector2u(1000, 500)
        };

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
            RpmMeter.paint(rw);
            PowerMeter.paint(rw);
            TorqueMeter.paint(rw);

            p.paint(rw);

            WoundUpIndicator.paint(rw);
            IgnitionIndicator.paint(rw);
            SlowModeIndicator.paint(rw);
            StarterIndicator.paint(rw);

            rw.Draw(Va, PrimitiveType.Lines);
        }

        bool clicked = false,
             clicked2 = false;
        
        int tickCount = 0,
            tickCount2 = 0;

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
                if (Program.eng.ignition)
                {
                    Program.eng.starterRotation = true;
                }
            }
            else
            {
                Program.eng.starterRotation = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (!clicked)
                { 
                    Program.eng.ignition = !Program.eng.ignition;
                    clicked= true;
                }
            }

            if (clicked) 
            {
                if (tickCount == 10)
                {
                    tickCount = 0;
                    clicked = false;
                }
                else
                {
                    tickCount++;
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.RControl))
            {
                if (!clicked2)
                {
                    Program.eng.slowMode = !Program.eng.slowMode;
                    clicked2 = true;
                }
            }

            if (clicked2)
            {
                if (tickCount2 == 10)
                {
                    tickCount2 = 0;
                    clicked2 = false;
                }
                else
                {
                    tickCount2++;
                }
            }

            RpmMeter.update(Program.eng.Rpm);
            PowerMeter.update(Program.eng.power);
            TorqueMeter.update(Program.eng.torque);

            p.update(Program.eng.Rpm);

            WoundUpIndicator.update(Program.eng.woundUp);
            IgnitionIndicator.update(Program.eng.ignition);
            SlowModeIndicator.update(Program.eng.slowMode);
            StarterIndicator.update(Program.eng.starterRotation);
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
    }
}
