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
                      idleRpmPoint
                      //throttlePos = 0;
                      //multiplicator
                      ;
        
        public bool woundUp,
                    accelPressed,
                    starterRotation;

        public Engine(int rpm, int maxrpmpoint, int idlerpmpoint)
        {
            woundUp = false;
            Rpm = rpm;
            maxRpmPoint = maxrpmpoint;
            idleRpmPoint = idlerpmpoint;
        }
        public void useStarter()
        {
            
        }

        public void updateStats(Object sender, System.Timers.ElapsedEventArgs e)
        {
            //Console.WriteLine("1");

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
        }

        public void updateEngine()
        {
            System.Timers.Timer t = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 10,
                Enabled = true
            };
            t.Elapsed += updateStats;
            Thread.Sleep(-1);
        }
    }
}
