using ABI.Windows.Foundation;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSFML
{
    public class Engine
    {
        public double Rpm,
                      maxRpmPoint,
                      idleRpmPoint,
                      power,
                      torque;
        
        public bool woundUp,
                    accelPressed,
                    starterRotation;

        Vector2f[] PowerGraphPoints = 
        {
            new Vector2f(1000, 20), 
            new Vector2f(1500, 60), 
            new Vector2f(4500, 180), 
            new Vector2f(6000, 180), 
            new Vector2f(7000, 170)
        };

        Vector2f[] TorqueGraphPoints =
{
            new Vector2f(1000, 200),
            new Vector2f(1500, 360),
            new Vector2f(4500, 360),
            new Vector2f(6500, 260)
        };

        public Engine(int rpm, int maxrpmpoint, int idlerpmpoint)
        {
            woundUp = false;
            Rpm = rpm;
            maxRpmPoint = maxrpmpoint;
            idleRpmPoint = idlerpmpoint;
        }



        public void updateStats()
        {
            
        }

        public void updateRpm(Object sender, System.Timers.ElapsedEventArgs e)
        {
            if (woundUp)
            {
                if (accelPressed)
                {
                    if (Rpm < maxRpmPoint + 600)
                    {
                        Rpm += 100;
                    }
                    else
                    {
                        Rpm = maxRpmPoint;
                    }
                }
                else
                {
                    if (Rpm > idleRpmPoint)
                    {
                        Rpm -= 250;
                    }
                    else
                    {
                        Rpm = idleRpmPoint;
                    }
                }
            }
            else
            {
                if (starterRotation == true)
                {
                    Rpm += 50;
                    if (Rpm >= idleRpmPoint)
                    {
                        woundUp = true;
                    }
                }
                else
                {
                    if (Rpm > 50)
                    {
                        Rpm -= 50;
                    }
                    else
                    {
                        Rpm = 0;
                    }
                }
            }

            updateStats();
        }

        public void updateEngine()
        {
            System.Timers.Timer t = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 10,
                Enabled = true
            };
            t.Elapsed += updateRpm;
            Thread.Sleep(-1);
        }
    }
}
