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
                      torque,
                      slowСoefficient;
        
        public bool woundUp,
                    accelPressed,
                    starterRotation,
                    ignition,
                    slowMode;

        double[,] PowerCoeff = new double[4, 2] { { 0.08, -60 }, { 0.043, -3.5}, { 0, 190 }, { -0.04, 430 } };

        double[,] TorqueCoeff = new double[3, 2] { { 0.32, -120 }, { 0, 360 }, { -0.04, 540 } };

        public Engine(int rpm, int maxrpmpoint, int idlerpmpoint)
        {
            woundUp = false;
            Rpm = rpm;
            maxRpmPoint = maxrpmpoint;
            idleRpmPoint = idlerpmpoint;
            ignition = false;
            slowMode = false;
            slowСoefficient = 1;
        }

        public void updateStats()
        {
            if (Rpm < 1000)
            {
                power = 0;
                torque = 0;
            }
            else if (Rpm >= 1000 && Rpm < 1500)
            {
                power = PowerCoeff[0, 0] * Rpm + PowerCoeff[0, 1];
                torque = TorqueCoeff[0, 0] * Rpm + TorqueCoeff[0, 1];
            }
            else if (Rpm >= 1500 && Rpm < 4500)
            {
                power = PowerCoeff[1, 0] * Rpm + PowerCoeff[1, 1];
                torque = TorqueCoeff[1, 0] * Rpm + TorqueCoeff[1, 1];
            }
            else if (Rpm >= 4500 && Rpm < 6000)
            {
                power = PowerCoeff[2, 0] * Rpm + PowerCoeff[2, 1];
                torque = TorqueCoeff[2, 0] * Rpm + TorqueCoeff[2, 1];
            }
            else if (Rpm >= 6000)
            {
                power = PowerCoeff[3, 0] * Rpm + PowerCoeff[3, 1];
                torque = TorqueCoeff[2, 0] * Rpm + TorqueCoeff[2, 1];
            }

            if (slowMode)
            {
                slowСoefficient = 100;
            }
            else
            {
                slowСoefficient = 1;
            }
        }

        public void updateRpm(Object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ignition)
            {
                if (woundUp)
                {
                    if (accelPressed)
                    {
                        if (Rpm < maxRpmPoint + 600)
                        {
                            Rpm += 50 / slowСoefficient;
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
                            Rpm -= 100 / slowСoefficient;
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
                        Rpm += 50 / slowСoefficient;
                        if (Rpm >= idleRpmPoint)
                        {
                            woundUp = true;
                        }
                    }
                    else
                    {
                        if (Rpm > 50)
                        {
                            Rpm -= 50 / slowСoefficient;
                        }
                        else
                        {
                            Rpm = 0;
                        }
                    }
                }
            }
            else
            {
                woundUp = false;

                if (Rpm > 0)
                {
                    Rpm -= 125 / slowСoefficient;
                }
                else
                {
                    Rpm = 0 / slowСoefficient;
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