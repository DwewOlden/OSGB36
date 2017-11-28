using OSGB36.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Coordinate
{
    public class LatLon:ICoordinate 
    {
        /// <summary>
        /// The latitude of the coordinate
        /// </summary>
        protected readonly double _latitude;

        /// <summary>
        /// The longtitude of the coordinate.
        /// </summary>
        protected readonly double _longtitude;

        /// <summary>
        /// The distance above see level of the location.
        /// </summary>
        protected readonly double _height;

        /// <summary>
        /// The distance in meters east of the reference point.
        /// </summary>
        /// <remarks>
        /// Longtitude goes across - see easting
        /// </remarks>
        public double Longtitude
        {
            get
            {
                return _longtitude;
            }
        }

        /// <summary>
        /// The distance in meters north of the the reference point.
        /// </summary>
        /// <remarks>
        /// Latitude goes up - see  northing
        /// </remarks>
        public double Latitude
        {
            get
            {
                return _latitude;
            }
        }

        /// <summary>
        /// The distance above see level (reference point Newlyn).
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pLatitude">The latitude of the location</param>
        /// <param name="pLongtitude">The longtitude of the location</param>
        /// <param name="pHeight">The height above sea level of the location</param>
        public LatLon(double pLatitude, double pLongtitude, double pHeight)
        {
            // Round the the locations down to 5 decimal places - that will be mm level
            // as the major component are meters. Thats why its in a different class.

            _latitude = Math.Round(pLatitude,8);
            _longtitude = Math.Round(pLongtitude,8);
            _height = Math.Round(pHeight,3);
        }

        public override string ToString()
        {
            return string.Format("Latitude:{0} Longtitude:{1} Height:{2}", _latitude, _longtitude, _height);
        }

        public override int GetHashCode()
        {
            return (((int)_latitude + (int)_longtitude) + ((int)_height * 3));
        }

        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            else
            {
                LatLon e = (LatLon)obj;
                if ((Math.Round(e._latitude, 5) == Math.Round(_latitude, 5)) && (Math.Round(e._longtitude, 5) == Math.Round(_longtitude, 5)) &&
                    (Math.Round(e._height, 5) == Math.Round(_height, 5)))
                    return true;
                else
                    return false;
            }
        }
    }
}
