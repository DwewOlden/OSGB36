using OSGB36.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Extensions
{
    /// <summary>
    /// Contains functionality that will convert between degrees and radians and vice-versa
    /// </summary>
    public class CoordinateSupport:ICoodrinateSupport
    {
        
        /// <summary>
        /// Convert a decimal degree into radians
        /// </summary>
        /// <param name="pDecimalDegrees">The decimal degreee to be converted</param>
        /// <returns>The passed value as radians</returns>
        public double DegreeesToRadians(double pDecimalDegrees)
        {
            return (pDecimalDegrees * Math.PI) / 180;
        }
        
        /// <summary>
        /// Convert a radian value to decimal degrees
        /// </summary>
        /// <param name="pRadians">The radian value to be converted</param>
        /// <returns>The passed value as a decimal degree</returns>
        public double RadiansToDegrees(double pRadians)
        {
            return (pRadians * 180) / Math.PI;
        }
    }
}
