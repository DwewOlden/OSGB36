using NUnit.Framework;
using OSGB36.Interfaces;
using OSGB36.ShiftSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36Tests
{
    /// <summary>
    /// Tests that check the reading (and in effect) the writing of the shift information is correct.
    /// </summary>
    [TestFixture]
    [SingleThreaded]
    public class Shift_Reader_Tests
    {
        IShifts Shifts_ = null;

        [OneTimeSetUp]
        public void SaveTheFile()
        {
            ShiftWriter w = new ShiftWriter();
            bool outcome = w.ProcessOSSourceFile();
            Assert.AreEqual(true, outcome);

            Shifts_ = new Shifts();
            Shifts_.LoadFromFile(@"C:\temp\OSTN02dat.dat");
        }


        [TestCase(876951, 109.209, -53.064, 44.731)]
        [TestCase(337, 95.300, -81.982, 49.584)]
        [TestCase(27, 91.128, -81.882, 54.646)]
        [TestCase(150287, 94.301, -77.261, 53.181)]
        [TestCase(150306, 94.546, -77.163, 52.940)]
        [TestCase(308082, 95.652, -71.953, 52.094)]
        [TestCase(876951, 109.209, -53.064, 44.731)]
        public void Check_Shift_Values_Directly(int pIndex,double x,double y,double z)
        {
            Shift s = (Shift)Shifts_[pIndex];

            Assert.AreEqual(x,Math.Round(s.Easting,4));
            Assert.AreEqual(y, Math.Round(s.Northing,4));
            Assert.AreEqual(z, Math.Round(s.Height,4));
        }

        [TestCase(876951, 109.209, -53.064, 44.731)]
        [TestCase(337, 95.300, -81.982, 49.584)]
        [TestCase(27, 91.128, -81.882, 54.646)]
        [TestCase(150287, 94.301, -77.261, 53.181)]
        [TestCase(150306, 94.546, -77.163, 52.940)]
        [TestCase(308082, 95.652, -71.953, 52.094)]
        [TestCase(876951, 109.209, -53.064, 44.731)]
        public void Check_Shift_Values_Indirectly(int pIndex, double x, double y, double z)
        {
            Assert.AreEqual(x, Math.Round(Shifts_.GetEastingValue(pIndex), 4));
            Assert.AreEqual(y, Math.Round(Shifts_.GetNorthingValue(pIndex), 4));
            Assert.AreEqual(z, Math.Round(Shifts_.GetHeightValue(pIndex), 4));
        }

    }
}
