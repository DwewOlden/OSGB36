using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Interfaces
{
    /// <summary>
    /// Interface containing the members used in the OSTN02 shift system
    /// </summary>
    public interface IShift
    {
        /// <summary>
        /// The OSTN02 Easting Shift
        /// </summary>
        float Easting { get; }

        /// <summary>
        /// The OSTN02 Northing Shift
        /// </summary>
        float Northing { get; }

        /// <summary>
        /// The OSTN02 / GM02 Height Shift
        /// </summary>
        float Height { get; }

    }
}
