using NUnit.Framework;
using OSGB36.Coordinate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36Tests
{
    [TestFixture]
    public class Lat_Lon_Tests
    {
        [TestCase(10, 20, 30, true)]
        [TestCase(20, 50, 70, true)]
        [TestCase(20, 230, 80, true)]
        public void EqualityTest(double e, double n, double h, bool e1)
        {
            LatLon k = new LatLon(e, n, h);
            LatLon k2 = new LatLon(e, n, h);

            Assert.AreEqual(k, k2);
        }

        [TestCase(10, 20, 30, 11, 20, 30, true)]
        [TestCase(20, 50, 70, 20, 51, 70, true)]
        [TestCase(20, 230, 80, 20, 230, 81, true)]
        [TestCase(20, 230, 80, 1, 1, 1, true)]
        public void EqualityTest_2(double e, double n, double h, double e2, double n2, double h2, bool e1)
        {
            LatLon k = new LatLon(e, n, h);
            LatLon k2 = new LatLon(e2, n2, h2);

            Assert.AreNotEqual(k, k2);
        }

        [TestCase(10.123446, 20, 30, 10.12345, 20, 30, true)]
        public void EqualityTest_3(double e, double n, double h, double e2, double n2, double h2, bool e1)
        {
            LatLon k = new LatLon(e, n, h);
            LatLon k2 = new LatLon(e2, n2, h2);

            Assert.AreEqual(k, k2);
        }

        [Test]
        public void ToString_Test()
        {
            LatLon k = new LatLon(10, 20, 30);
            string outcome = k.ToString();
            string expected = "Latitude:10 Longtitude:20 Height:30";

            Assert.AreEqual(expected, outcome);

        }
    }
}
