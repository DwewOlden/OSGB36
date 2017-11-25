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
        public void Test_Conversion_Of_Radians_To_Degrees(double a,double b)
        {
            ICoodrinateSupport C = new CoordinateSupport();
            var d = Math.Round(C.RadiansToDegrees(a),5);

            Assert.AreEqual(b, d);


        }
    }
}
