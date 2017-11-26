using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Coordinate
{
    /// <summary>
    /// The OSGB36 is national (UK) cartesian coordinate system and represents the distance in meters from a fixed point 
    /// in metert in each direction (East and North). The height is also metric.
    /// </summary>
    public class EastingNorthing
    {
        /// <summary>
        /// The distance in meters east of the reference point.
        /// </summary>
        protected readonly double _easting;

        /// <summary>
        /// The distance in meters north of the the reference point.
        /// </summary>
        protected readonly double _northing;

        /// <summary>
        /// The distance above see level (reference point Newlyn).
        /// </summary>
        protected readonly double _height;

        /// <summary>
        /// The distance in meters east of the reference point.
        /// </summary>
        public double Easting
        {
            get
            {
                return _easting;
            }
        }

        /// <summary>
        /// The distance in meters north of the the reference point.
        /// </summary>
        public double Northing
        {
            get
            {
                return _northing;
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
        /// <param name="pEasting">The easting of the location</param>
        /// <param name="pNorthing">The northing of the location</param>
        /// <param name="pHeight">The height above sea level of the location</param>
        public EastingNorthing(double pEasting,double pNorthing,double pHeight)
        {
            _easting = pEasting;
            _northing = pNorthing;
            _height = pHeight;
        }

        public override string ToString()
        {
            return string.Format("Easting:{0} Northing{1} Height{2}", _easting, _northing, _height);
        }

        public override int GetHashCode()
        {
            return (((int)_easting + (int)_northing) + ((int)_height * 3));
        }

        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            else
            {
                EastingNorthing e = (EastingNorthing)obj;
                if ((Math.Round(e._easting, 3) == Math.Round(_easting, 3)) && (Math.Round(e._northing, 3) == Math.Round(_northing, 3)) &&
                    (Math.Round(e._height, 3) == Math.Round(_height, 3))
                    return true;
                else
                    return false;
            }
        }
    }
}
