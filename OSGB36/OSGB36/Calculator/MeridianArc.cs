using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Calculator
{
    /// <summary>
    /// This class contains the code to compute the meridan arc values
    /// </summary>
    public class MeridianArc
    {
        /// <summary>
        /// Computes the meridan arc value
        /// </summary>
        /// <param name="dLat">The difference of latitude value</param>
        /// <param name="sLat">The sum of latitude value</param>
        /// <param name="n">Ratio of the ellipsode difference</param>
        /// <param name="b">Ellipsode axis value by scaling value</param>
        /// <returns>The meriden arc value</returns>
        protected internal static double ComputeMeridanArc(double dLat, double sLat, double n, double b)
        {
            double M;                       // compute Meridian arc (eqn 8.1 [1])
            M = ((1 + n + ((5 * n * n) / 4) + ((5 * n * n * n) / 4)) * dLat) -
                   (((3 * n) + (3 * n * n) + ((21 * n * n * n) / 8)) * Math.Sin(dLat) * Math.Cos(sLat)) +
                   ((((15 * n * n) / 8) + ((15 * n * n * n) / 8)) * Math.Sin(2 * dLat) * Math.Cos(2 * sLat)) -
                   (((35 * n * n * n) / 24) * Math.Sin(3 * dLat) * Math.Cos(3 * sLat));

            M *= b;

            return M;
        }
    }
}
