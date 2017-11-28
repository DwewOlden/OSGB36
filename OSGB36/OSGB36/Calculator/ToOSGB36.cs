using OSGB36.Coordinate;
using OSGB36.Interfaces;
using OSGB36.ShiftSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Calculator
{
    /// <summary>
    /// Code to perform the calculation from lat-lon to easting-northing
    /// </summary>
    public class ToOSGB36 : ICalculationMethod
    {
        private ICoodrinateSupport mCoordinateSupport;

        private IShifts mShifts;

        public ToOSGB36(IShifts pShifts, ICoodrinateSupport pCoordinateSupport)
        {
            mShifts = pShifts;
            mCoordinateSupport = pCoordinateSupport;
        }


        /// <summary>
        /// Calling this method will convert from the passed value to the OSGB36
        /// </summary>
        /// <param name="pCoordinate">The latitude and longtitude to be converted </param>
        /// <returns>The location as a OSGB36 coordinate</returns>
        public ICoordinate Calculate(ICoordinate pCoordinate)
        {
            if (pCoordinate.GetType() != typeof(LatLon))
                throw new CoordinateTypeException("the wrong coordinate type has been passed");
            else
            {
                LatLon p = (LatLon)pCoordinate;
                return Calculate(p.Latitude,p.Longtitude,p.Height);
            }


        }

        /// <summary>
        /// Calling this method will convert from the passed value to the OSGB36
        /// </summary>
        /// <param name="pHorizontal">The point on the horizontal axis (easting or latitude)</param>
        /// <param name="pVertical">The point on the vertical axis (northing or longtitude)</param>
        /// <param name="pHeight">The height to be converted</param>
        /// <returns>The location as a OSGB36 coordinate</returns>
        public ICoordinate Calculate(double pVertical, double pHorizontal, double pHeight)
        {
            if (((pHorizontal < -7) || (pHorizontal > 8)) && ((pVertical < 49) || (pVertical > 60)))
                throw new ArgumentOutOfRangeException("the location falls outside of the bounds of the United Kingdom");
            else
                return InternalCalculate(pHorizontal, pVertical, pHeight);
        }

        /// <summary>
        /// Calling this method will convert from the passed value to the OSGB36 convertor
        /// </summary>
        /// <param name="pHorizontal">The point on the horizontal axis (easting or latitude)</param>
        /// <param name="pVertical">The point on the vertical axis (northing or longtitude)</param>
        /// <param name="pHeight">The height to be converted</param>
        /// <returns>The location as a OSGB36 coordinate</returns>
        private ICoordinate InternalCalculate(double pHorizontal, double pVertical, double pHeight)
        {
            double lat = mCoordinateSupport.DegreeesToRadians(pVertical);
            double lon = mCoordinateSupport.DegreeesToRadians(pHorizontal);
            double ht = pHeight;

            double a = OSGB35Constants.a;
            a *= OSGB35Constants.f0;
            double b = OSGB35Constants.b;
            b *= OSGB35Constants.f0;

            double n = (a - b) / (a + b);
            double e2 = ((a * a) - (b * b)) / (a * a);
            double nu = (a / Math.Pow(1 - e2 * Math.Pow(Math.Sin(lat), 2.0), 0.5));
            double rho = ((a * (1 - e2)) / Math.Pow(1 - e2 * Math.Pow(Math.Sin(lat), 2.0), 1.5));
            double n2 = (nu / rho) - 1;

            double dLat = lat - OSGB35Constants.lat0;      // difference in latitude
            double sLat = lat + OSGB35Constants.lat0;      // sum of latitude

            double M = MeridianArc.ComputeMeridanArc(dLat, sLat, n, b);

            double P = lon - OSGB35Constants.lon0;          // difference in longitude

            // The following are taken from [1] pp10-11
            double I = M + OSGB35Constants.n0;
            double II = (nu / 2) * Math.Sin(lat) * Math.Cos(lat);
            double III = (nu / 24) * Math.Sin(lat) * Math.Pow(Math.Cos(lat), 3.0) * (5 - Math.Pow(Math.Tan(lat), 2.0) + (9 * n2));
            double IIIA = (nu / 720) * Math.Sin(lat) * Math.Pow(Math.Cos(lat), 5.0) *
                               (61 - (58 * Math.Pow(Math.Tan(lat), 2.0)) + Math.Pow(Math.Tan(lat), 4.0));
            double IV = nu * Math.Cos(lat);
            double V = (nu / 6) * Math.Pow(Math.Cos(lat), 3.0) * ((nu / rho) - Math.Pow(Math.Tan(lat), 2.0));
            double VI = (nu / 120) * Math.Pow(Math.Cos(lat), 5.0) * (5 - (18 * Math.Pow(Math.Tan(lat), 2.0)) +
                                Math.Pow(Math.Tan(lat), 4.0) + (14 * n2) - (58 * Math.Pow(Math.Tan(lat), 2.0) * n2));

            double y = I + (Math.Pow(P, 2.0) * II) + (Math.Pow(P, 4.0) * III) + (Math.Pow(P, 6.0) * IIIA);
            double x = OSGB35Constants.e0 + (P * IV) + (Math.Pow(P, 3.0) * V) + (Math.Pow(P, 5.0) * VI);

            Shift s = (Shift)mShifts.CalculateShift(x, y);

            double lLat = x + s.Easting;
            double lLon = y + s.Northing;
            double lHeight = pHeight - s.Height;

            EastingNorthing x1 = new EastingNorthing(lLat, lLon, lHeight);
            return x1;

        }
    }
}
