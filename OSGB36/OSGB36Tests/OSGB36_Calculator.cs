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

        [TestCase(49.92226393730, -6.29977752014, 100.000)]
        [TestCase(49.96006137820, -5.20304609998, 124.269)]
        [TestCase(50.43885825610, -4.10864563561, 215.251)]
        [TestCase(50.57563665000, -1.29782277240, 94.688)]
        [TestCase(50.93127937910, -1.45051433700, 100.405)]
        [TestCase(51.40078220140, -3.55128349240, 112.371)]
        [TestCase(51.37447025550, 1.44454730409, 99.439)]
        [TestCase(51.42754743020, -2.54407618349, 104.018)]
        [TestCase(51.48936564950, -0.11992557180, 66.057)]
        [TestCase(51.85890896400, -4.30852476960, 81.351)]
        [TestCase(51.89436637350, 0.89724327012, 75.274)]
        [TestCase(50.57563665000, -1.29782277240, 94.688)]
        [TestCase(52.25529381630, -2.15458614387, 101.526)]
        [TestCase(52.25160951230, -0.91248956970, 131.594)]
        [TestCase(52.75136687170, 0.40153547065, 66.431)]
        [TestCase(52.96219109410, -1.19747655922, 93.825)]
        [TestCase(53.34480280190, -2.64049320810, 88.411)]
        [TestCase(53.41628516040, -4.28918069756, 100.776)]
        [TestCase(53.41630925420, -4.28917792869, 100.854)]
        [TestCase(53.77911025760, -3.04045490691, 64.940)]
        [TestCase(53.80021519630, -1.66379168242, 215.609)]
        [TestCase(54.08666318080, -4.63452168212, 84.366)]
        [TestCase(54.11685144290, -0.07773133187, 86.778)]
        [TestCase(54.32919541010, -4.38849118133, 94.503)]
        [TestCase(54.89542340420, -2.93827741149, 93.542)]
        [TestCase(54.97912273660, -1.61657685184, 125.878)]
        [TestCase(55.85399952950, -4.29649016251, 71.617)]
        [TestCase(55.92478265510, -3.29479219337, 119.032)]
        [TestCase(57.00606696050, -5.82836691850, 68.494)]
        [TestCase(57.13902518960, -2.04856030746, 108.611)]
        [TestCase(57.48625000720, -4.21926398555, 66.178)]
        [TestCase(57.81351838410, -8.57854456076, 100.001)]
        [TestCase(58.21262247180, -7.59255560556, 140.404)]
        [TestCase(58.51560361300, -6.26091455533, 115.026)]
        [TestCase(58.58120461280, -3.72631022121, 98.634)]
        [TestCase(59.03743871190, -3.21454001115, 100.000)]
        [TestCase(59.09335035320, -4.41757674598, 100.000)]
        [TestCase(59.09671617400, -5.82799339844, 140.716)]
        [TestCase(59.53470794490, -1.62516966058, 100.000)]
        [TestCase(59.85409913890, -1.27486910356, 149.890)]
        [TestCase(60.13308091660, -2.07382822798, 140.716)]
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
