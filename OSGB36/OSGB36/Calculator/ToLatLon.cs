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
    // <summary>
    /// Code to perform the calculation from lat-lon to easting-northing
    /// </summary>
    public class ToLatLon : ICalculationMethod
    {
        private ICoodrinateSupport mCoordinateSupport;

        private IShifts mShifts;

        public ToLatLon(IShifts pShifts, ICoodrinateSupport pCoordinateSupport)
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
            if (pCoordinate.GetType() != typeof(EastingNorthing))
                throw new CoordinateTypeException("the wrong coordinate type has been passed");
            else
            {
                EastingNorthing p = (EastingNorthing)pCoordinate;
                return Calculate(p.Easting,p.Northing,p.Height);
            }
           
        }

        /// <summary>
        /// Calling this method will convert from the passed value to the OSGB36
        /// </summary>
        /// <param name="pHorizontal">The point on the horizontal axis (easting or latitude)</param>
        /// <param name="pVertical">The point on the vertical axis (northing or longtitude)</param>
        /// <param name="pHeight">The height to be converted</param>
        /// <returns>The location as a OSGB36 coordinate</returns>
        public ICoordinate Calculate(double pHorizontal, double pVertical, double pHeight)
        {
            if (((pHorizontal < 0) || (pHorizontal > 1000000)) && ((pVertical < 0) || (pVertical > 1000000)))
                throw new ArgumentOutOfRangeException("the location falls outside of the bounds of the United Kingdom");
            else
                return InternalCalculate(pVertical, pHorizontal, pHeight);

        }

        /// <summary>
        /// Calling this method will convert from the passed value to the LatLon
        /// </summary>
        /// <param name="pHorizontal">The point on the horizontal axis (easting or latitude)</param>
        /// <param name="pVertical">The point on the vertical axis (northing or longtitude)</param>
        /// <param name="pHeight">The height to be converted</param>
        /// <returns>The location as a LatLon coordinate</returns>
        private ICoordinate InternalCalculate(double pHorizontal, double pVertical, double pHeight)
        {
            double eS = 0, nS = 0;
            double ht_dif = 0;
            double E = 0, N = 0;

            Shift s = (Shift)mShifts.CalculateShift(pHorizontal, pVertical);
            eS = s.Easting;
            nS = s.Northing;
            ht_dif = Math.Round(s.Height,3);

            double diff = 0, diffn = 0, NeweS = 0, NewnS = 0;
            int iterations = 0;

            do
            {
                E = pHorizontal - eS;         // use shifts to calculate OSGRS80 Grid coordinates
                N = pVertical - nS;

                s = (Shift)mShifts.CalculateShift(pHorizontal, pVertical);
                NeweS = s.Easting;
                NewnS = s.Northing;
                ht_dif = s.Height;
                
                diff = NeweS - eS;
                diffn = NewnS - nS;

                eS = NeweS;
                nS = NewnS;

                iterations = iterations + 1;

            } while ((iterations < 20) && ((Math.Abs(diff) > 0.00001)) || (Math.Abs(diffn) > 0.00001));

            double a = OSGB35Constants.a;
            a *= OSGB35Constants.f0;
            double b = OSGB35Constants.b;
            b *= OSGB35Constants.f0;

            
            double n = (a - b) / (a + b);
            double e2 = ((a * a) - (b * b)) / (a * a);
            double latEst = ((N - OSGB35Constants.n0) / a) + OSGB35Constants.lat0;

            double latNew, dLat, sLat, M;
            iterations = 0;

            do
            {
                dLat = latEst - OSGB35Constants.lat0;      // difference in latitude
                sLat = latEst + OSGB35Constants.lat0;      // sum of latitude

                M = MeridianArc.ComputeMeridanArc(dLat, sLat, n, b);
                latNew = ((N - OSGB35Constants.n0 - M) / a) + latEst;
                latEst = latNew;
                diff = N - OSGB35Constants.n0 - M;
            } while ((iterations++ < 10) && (diff > 0.00001));

            double nu = (a / Math.Pow(1 - e2 * Math.Pow(Math.Sin(latEst), 2.0), 0.5));
            double rho = ((a * (1 - e2)) / Math.Pow(1 - e2 * Math.Pow(Math.Sin(latEst), 2.0), 1.5));
            double n2 = (nu / rho) - 1;

            double VII = Math.Tan(latEst) / (2 * nu * rho);
            double VIII = (Math.Tan(latEst) / (24 * rho * Math.Pow(nu, 3.0))) * (5 + (3 * Math.Pow(Math.Tan(latEst), 2.0)) +
                               n2 - (9 * Math.Pow(Math.Tan(latEst), 2.0) * n2));
            double IX = (Math.Tan(latEst) / (720 * rho * Math.Pow(nu, 5.0))) * (61 + (90 * Math.Pow(Math.Tan(latEst), 2.0)) +
                               (45 * Math.Pow(Math.Tan(latEst), 4.0)));
            double X = 1 / (Math.Cos(latEst) * nu);
            double XI = (1 / (6 * (Math.Cos(latEst) * Math.Pow(nu, 3.0))) * ((nu / rho) + (2 * Math.Pow(Math.Tan(latEst), 2.0))));
            double XII = (1 / (120 * (Math.Cos(latEst) * Math.Pow(nu, 5.0))) * (5 + (28 * Math.Pow(Math.Tan(latEst), 2.0)) + (24 * Math.Pow(Math.Tan(latEst), 4.0))));
            double XIIA = (1 / (5040 * (Math.Cos(latEst) * Math.Pow(nu, 7.0))) * (61 + (662 * Math.Pow(Math.Tan(latEst), 2.0)) +
                               (1320 * Math.Pow(Math.Tan(latEst), 4.0)) + (720 * Math.Pow(Math.Tan(latEst), 6.0))));

            double y = E - OSGB35Constants.e0;
            double k1 = latEst - (y * y * VII) + (Math.Pow(y, 4.0) * VIII) - (Math.Pow(y, 6.0) * IX);
            double k2 = OSGB35Constants.lon0 + (y * X) - (Math.Pow(y, 3.0) * XI) + (Math.Pow(y, 5.0) * XII) - (Math.Pow(y, 7.0) * XIIA);
            double k3 = pHeight + ht_dif;
            // [TestCase(49.92226393730, -6.29977752014, 100.000, 91492.146, 11318.804, 46.519)]
        double xr1 = mCoordinateSupport.RadiansToDegrees(k1);
            double xr2 = mCoordinateSupport.RadiansToDegrees(k2);

            return new LatLon(xr1, xr2, k3);
        }

        
    }
}
