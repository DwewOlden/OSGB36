using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Interfaces
{
    /// <summary>
    /// Outlines the functionality used to convert between degrees / radians
    /// </summary>
    public interface ICoodrinateSupport
    {
        /// <summary>
        /// Convert a decimal degree into radians
        /// </summary>
        /// <param name="pDecimalDegrees">The decimal degreee to be converted</param>
        /// <returns>The passed value as radians</returns>
        double DegreeesToRadians(double pDecimalDegrees);

        /// <summary>
        /// Convert a radian value to decimal degrees
        /// </summary>
        /// <param name="pRadians">The radian value to be converted</param>
        /// <returns>The passed value as a decimal degree</returns>
        double RadiansToDegrees(double pRadians);
    }
}
