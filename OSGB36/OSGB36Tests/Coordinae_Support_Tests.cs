using NUnit.Framework;
using OSGB36.Extensions;
using OSGB36.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36Tests
{
    [TestFixture]
    public class Coordinae_Support_Tests
    {
        [TestCase(0.5235987756,30)]
        [TestCase(0.7853981634,45)]
        [TestCase(1.0471975512, 60)]
        [TestCase(1.5707963268, 90)]
        [TestCase(2.0943951024, 120)]
        [TestCase(2.3561944902, 135)]
        [TestCase(2.6179938780, 150)]
        [TestCase(3.1415926536, 180)]
        [TestCase(4.7123889804, 270)]
        [TestCase(6.2831853072, 360)]
        public void Test_Conversion_Of_Radians_To_Degrees(double a,double b)
        {
            ICoodrinateSupport C = new CoordinateSupport();
            var d = Math.Round(C.RadiansToDegrees(a),1);

            Assert.AreEqual(b, d);
        }

        [TestCase(0.5235987756, 30)]
        [TestCase(0.7853981634, 45)]
        [TestCase(1.0471975512, 60)]
        [TestCase(1.5707963268, 90)]
        [TestCase(2.0943951024, 120)]
        [TestCase(2.3561944902, 135)]
        [TestCase(2.6179938780, 150)]
        [TestCase(3.1415926536, 180)]
        [TestCase(4.7123889804, 270)]
        [TestCase(6.2831853072, 360)]
        public void Test_Conversion_Of_Degrees_to_Radians(double a, double b)
        {
            ICoodrinateSupport C = new CoordinateSupport();
            var d = Math.Round(C.DegreeesToRadians(b),10);
            Assert.AreEqual(a, d);
        }
    }
}
