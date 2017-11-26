using NUnit.Framework;
using OSGB36.Calculator;
using OSGB36.Extensions;
using OSGB36.Interfaces;
using OSGB36.ShiftSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36Tests
{
    [TestFixture]
    public class OSGB36_Calculator
    {
        IShifts Shifts_ = null;

        [OneTimeSetUp]
        public void SetUp()
        {
            Shifts_ = new Shifts();
            Shifts_.LoadFromFile(@"C:\temp\OSTN02dat.dat");
        }

        [Test]
        public void Test_1()
        {
            double l = 49.92226393730;
            double l2 = -6.2997775201;
            double h = 100.000;

            ICoodrinateSupport q = new CoordinateSupport();
            
            ToOSGB36 b = new ToOSGB36(Shifts_,q);
            b.Calculate(l, l2, h);
            



        }
    }
}
