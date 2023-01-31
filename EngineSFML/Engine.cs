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
                      throttlePos = 0;
                      //multiplicator;
        
        public bool woundUp,
                    accelPressed,
                    starterRotation;

        public Engine(int rpm, int maxrpmpoint, int idlerpmpoint)
        {
            Rpm = rpm;
            maxRpmPoint = maxrpmpoint;
            idleRpmPoint = idlerpmpoint;
        }
        public void useStarter()
        {
            
        }

        public void updateStats(Object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.Write("хуй");

            /*if (woundUp)
            {
                if (accelPressed)
                {
                    Rpm *= 1.1;
                }
                else
                {
                    if (Rpm > idleRpmPoint)
                    {
                        if (Rpm > idleRpmPoint + 500)
                        {
                            Rpm -= 400;
                        }
                        else
                        {
                            Rpm -= 200;
                        }
                    }
                    else
                    {
                        Rpm += 100;
                    }
                }
            }
            else
            {
                if (Rpm > 500)
                {
                    Rpm -= 500;
                }
                else
                {
                    Rpm = 0;
                }
            }*/
        }

        public void updateEngine()
        {
            System.Timers.Timer t = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 1000,
                Enabled = true
            };
            t.Elapsed += updateStats;
            Thread.Sleep(-1);
        }
    }
}
