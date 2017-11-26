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
    public class Easting_Norting_Test
    {
        [TestCase(10,20,30,true)]
        [TestCase(20, 50, 70, true)]
        [TestCase(20, 230, 80, true)]
        public void EqualityTest(double e,double n, double h,bool e1)
        {
            EastingNorthing k = new EastingNorthing(e, n, h);
            EastingNorthing k2 = new EastingNorthing(e, n, h);

            Assert.AreEqual(k, k2);
        }

        [TestCase(10, 20, 30,11,20,30, true)]
        [TestCase(20, 50, 70,20,51,70, true)]
        [TestCase(20, 230, 80,20,230,81, true)]
        [TestCase(20, 230, 80, 1, 1, 1, true)]
        public void EqualityTest_2(double e, double n, double h, double e2, double n2, double h2, bool e1)
        {
            EastingNorthing k = new EastingNorthing(e, n, h);
            EastingNorthing k2 = new EastingNorthing(e2, n2, h2);

            Assert.AreNotEqual(k, k2);
        }

        [Test]
        public void ToString_Test()
        {
            EastingNorthing k = new EastingNorthing(10, 20,30 );
            string outcome  = k.ToString();
            string expected = "Easting:10 Northing:20 Height:30";

            Assert.AreEqual(expected, outcome);

        }

    }
}
