using OSGB36.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.ShiftSystem
{
    /// <summary>
    /// Default implemention of the shift interface
    /// </summary>
    public class Shift : IShift
    {
        /// <summary>
        /// The easting shift
        /// </summary>
        private float _e;

        /// <summary>
        /// The northing shift
        /// </summary>
        private float _n;

        /// <summary>
        /// The shift height
        /// </summary>
        private float _h;

        /// <summary>
        /// Get the shft easting
        /// </summary>
        public float Easting { get { return _e; } }

        /// <summary>
        /// Get the shift northing
        /// </summary>
        public float Northing { get { return _n; } }

        /// <summary>
        /// Get the shift height
        /// </summary>
        public float Height { get { return _h; } }

        /// <summary>
        /// Defaut constructor
        /// </summary>
        public Shift()
        {
            _e = 0;
            _n = 0;
            _h = 0;
        }

        /// <summary>
        /// Overloaded constructor, populates the object with the passed value
        /// </summary>
        /// <param name="pEastshift">The shift in easting</param>
        /// <param name="pNorthShift">The shift in northing</param>
        /// <param name="pHeightShift">The shift in height</param>
        public Shift(float pEastshift, float pNorthShift,float pHeightShift)
        {
            _e = pEastshift;
            _n = pNorthShift;
            _h = pHeightShift;
        }

        /// <summary>
        /// Output the shift information as a formatted string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Easting:{0} Northing:{1} Height:{2}", _e, _n, _h);
        }

        /// <summary>
        /// Produces a hashcode for the shift
        /// </summary>
        /// <returns></returns>
        /// <remarks>It would be very difficult to produce a hash that would not have a large number of collisions</remarks>
        public override int GetHashCode()
        {
            return (int)((_e * _n) * _h);
        }

        /// <summary>
        /// Check two shifts for equality
        /// </summary>
        /// <param name="obj">The target shift</param>
        /// <returns>True if the two shifts are equal on easting northing and height, false if they are not</returns>
        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType())
                return false;

            Shift l = (Shift)obj;

            if ((l._e == _e) && (l._n == _n) && (l._h == _h))
                return true;
            else
                return false;
        }
    }
}
