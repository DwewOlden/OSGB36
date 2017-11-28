﻿using NUnit.Framework;
using OSGB36.Calculator;
using OSGB36.Coordinate;
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
    [SingleThreaded]
    public class OSGB36_Calculator
    {
        IShifts Shifts_ = null;

        [OneTimeSetUp]
        public void SetUp()
        {
            Shifts_ = new Shifts();
            Shifts_.LoadFromFile(@"C:\temp\OSTN02dat.dat");
        }

        [TestCase(49.92226393730, -6.29977752014, 100.000, 91492.146, 11318.804, 46.519)]
        [TestCase(49.96006137820, -5.20304609998, 124.269, 170370.718, 11572.405, 71.264)]
        [TestCase(50.43885825610, -4.10864563561, 215.251, 250359.811, 62016.569, 163.097)]
        [TestCase(50.57563665000, -1.29782277240, 94.688, 449816.371, 75335.861, 48.589)]
        [TestCase(50.93127937910, -1.45051433700, 100.405, 438710.920, 114792.250, 54.056)]
        [TestCase(51.40078220140, -3.55128349240, 112.371, 292184.870, 168003.465, 60.646)]
        [TestCase(51.37447025550, 1.44454730409, 99.439, 639821.835, 169565.858, 55.149)]
        [TestCase(51.42754743020, -2.54407618349, 104.018, 362269.991, 169978.690, 54.485)]
        [TestCase(51.48936564950, -0.11992557180, 66.057, 530624.974, 178388.464, 20.544)]
        [TestCase(51.85890896400, -4.30852476960, 81.351, 241124.584, 220332.641, 27.613)]
        [TestCase(51.89436637350, 0.89724327012, 75.274, 599445.590, 225722.826, 30.207)]
        [TestCase(52.25529381630, -2.15458614387, 101.526, 389544.190, 261912.153, 51.998)]
        [TestCase(52.25160951230, -0.91248956970, 131.594, 474335.969, 262047.755, 83.982)]
        [TestCase(52.75136687170, 0.40153547065, 66.431, 562180.547, 319784.995, 20.912)]
        [TestCase(52.96219109410, -1.19747655922, 93.825, 454002.834, 340834.943, 45.275)]
        [TestCase(53.34480280190, -2.64049320810, 88.411, 357455.843, 383290.436, 36.779)]
        [TestCase(53.41628516040, -4.28918069756, 100.776, 247958.971,393492.909,46.335)]
        [TestCase(53.41630925420, -4.28917792869, 100.854, 247959.241,393495.583,46.413)]
        [TestCase(53.77911025760, -3.04045490691, 64.940, 331534.564, 431920.794, 12.658)]
        [TestCase(53.80021519630, -1.66379168242, 215.609, 422242.186, 433818.701, 165.912)]
        [TestCase(54.08666318080, -4.63452168212, 84.366, 227778.330,468847.388,29.335)]
        [TestCase(54.11685144290, -0.07773133187, 86.778, 525745.670,470703.214,41.232)]
        [TestCase(54.32919541010, -4.38849118133, 94.503, 244780.636,495254.887,39.891)]
        [TestCase(54.89542340420, -2.93827741149, 93.542, 339921.145,556034.761,41.107)]
        [TestCase(54.97912273660, -1.61657685184, 125.878, 424639.355,565012.703,76.574)]
        [TestCase(55.85399952950, -4.29649016251, 71.617, 256340.925,664697.269,17.459)]
        [TestCase(55.92478265510, -3.29479219337, 119.032, 319188.434,670947.534,66.388)]
        [TestCase(57.00606696050, -5.82836691850, 68.494, 167634.202,797067.144,13.192)]
        [TestCase(57.13902518960, -2.04856030746, 108.611, 397160.491,805349.736,58.933)]
        [TestCase(57.48625000720, -4.21926398555, 66.178, 267056.768,846176.972,13.260)]
        [TestCase(57.81351838410, -8.57854456076, 100.001, 9587.909,899448.996,42.013)]
        [TestCase(58.21262247180, -7.59255560556, 140.404, 71713.132,938516.404,83.732)]
        [TestCase(58.51560361300, -6.26091455533, 115.026, 151968.652,966483.780,58.921)]
        [TestCase(58.58120461280, -3.72631022121, 98.634, 299721.891,967202.992,46.021)]
        [TestCase(59.03743871190, -3.21454001115, 100.000, 330398.323,1017347.016,47.956)]
        [TestCase(59.09335035320, -4.41757674598, 100.000, 261596.778,1025447.602,46.445)]
        [TestCase(59.09671617400, -5.82799339844, 140.716, 180862.461,1029604.114,85.349)]
        [TestCase(59.53470794490, -1.62516966058, 100.000, 421300.525, 1072147.239, 51.049)]
        [TestCase(59.85409913890, -1.27486910356, 149.890, 440725.073, 1107878.448, 100.989)]
        [TestCase(60.13308091660, -2.07382822798, 140.716, 395999.668, 1138728.951, 90.015)]
        public void To_OSGB36(double a, double b, double c, double e, double f, double g)
        {
            ICoodrinateSupport q = new CoordinateSupport();            
            ToOSGB36 k = new ToOSGB36(Shifts_,q);

            var Outcome = k.Calculate(a, b, c);
            EastingNorthing Expected = new EastingNorthing(e, f, g);
   
            Assert.AreEqual(Expected,Outcome);
        }

        [TestCase(49.92226393730, -6.29977752014, 100.000, 91492.146, 11318.804, 46.519)]
        [TestCase(49.96006137820, -5.20304609998, 124.269, 170370.718, 11572.405, 71.264)]
        [TestCase(50.43885825610, -4.10864563561, 215.251, 250359.811, 62016.569, 163.097)]
        [TestCase(50.57563665000, -1.29782277240, 94.688, 449816.371, 75335.861, 48.589)]
        [TestCase(50.93127937910, -1.45051433700, 100.405, 438710.920, 114792.250, 54.056)]
        [TestCase(51.40078220140, -3.55128349240, 112.371, 292184.870, 168003.465, 60.646)]
        [TestCase(51.37447025550, 1.44454730409, 99.439, 639821.835, 169565.858, 55.149)]
        [TestCase(51.42754743020, -2.54407618349, 104.018, 362269.991, 169978.690, 54.485)]
        [TestCase(51.48936564950, -0.11992557180, 66.057, 530624.974, 178388.464, 20.544)]
        [TestCase(51.85890896400, -4.30852476960, 81.351, 241124.584, 220332.641, 27.613)]
        [TestCase(51.89436637350, 0.89724327012, 75.274, 599445.590, 225722.826, 30.207)]
        [TestCase(52.25529381630, -2.15458614387, 101.526, 389544.190, 261912.153, 51.998)]
        [TestCase(52.25160951230, -0.91248956970, 131.594, 474335.969, 262047.755, 83.982)]
        [TestCase(52.75136687170, 0.40153547065, 66.431, 562180.547, 319784.995, 20.912)]
        [TestCase(52.96219109410, -1.19747655922, 93.825, 454002.834, 340834.943, 45.275)]
        [TestCase(53.34480280190, -2.64049320810, 88.411, 357455.843, 383290.436, 36.779)]
        [TestCase(53.41628516040, -4.28918069756, 100.776, 247958.971, 393492.909, 46.335)]
        [TestCase(53.41630925420, -4.28917792869, 100.854, 247959.241, 393495.583, 46.413)]
        [TestCase(53.77911025760, -3.04045490691, 64.940, 331534.564, 431920.794, 12.658)]
        [TestCase(53.80021519630, -1.66379168242, 215.609, 422242.186, 433818.701, 165.912)]
        [TestCase(54.08666318080, -4.63452168212, 84.366, 227778.330, 468847.388, 29.335)]
        [TestCase(54.11685144290, -0.07773133187, 86.778, 525745.670, 470703.214, 41.232)]
        [TestCase(54.32919541010, -4.38849118133, 94.503, 244780.636, 495254.887, 39.891)]
        [TestCase(54.89542340420, -2.93827741149, 93.542, 339921.145, 556034.761, 41.107)]
        [TestCase(54.97912273660, -1.61657685184, 125.878, 424639.355, 565012.703, 76.574)]
        [TestCase(55.85399952950, -4.29649016251, 71.617, 256340.925, 664697.269, 17.459)]
        [TestCase(55.92478265510, -3.29479219337, 119.032, 319188.434, 670947.534, 66.388)]
        [TestCase(57.00606696050, -5.82836691850, 68.494, 167634.202, 797067.144, 13.192)]
        [TestCase(57.13902518960, -2.04856030746, 108.611, 397160.491, 805349.736, 58.933)]
        [TestCase(57.48625000720, -4.21926398555, 66.178, 267056.768, 846176.972, 13.260)]
        [TestCase(57.81351838410, -8.57854456076, 100.001, 9587.909, 899448.996, 42.013)]
        [TestCase(58.21262247180, -7.59255560556, 140.404, 71713.132, 938516.404, 83.732)]
        [TestCase(58.51560361300, -6.26091455533, 115.026, 151968.652, 966483.780, 58.921)]
        [TestCase(58.58120461280, -3.72631022121, 98.634, 299721.891, 967202.992, 46.021)]
        [TestCase(59.03743871190, -3.21454001115, 100.000, 330398.323, 1017347.016, 47.956)]
        [TestCase(59.09335035320, -4.41757674598, 100.000, 261596.778, 1025447.602, 46.445)]
        [TestCase(59.09671617400, -5.82799339844, 140.716, 180862.461, 1029604.114, 85.349)]
        [TestCase(59.53470794490, -1.62516966058, 100.000, 421300.525, 1072147.239, 51.049)]
        [TestCase(59.85409913890, -1.27486910356, 149.890, 440725.073, 1107878.448, 100.989)]
        [TestCase(60.13308091660, -2.07382822798, 140.716, 395999.668, 1138728.951, 90.015)]
        public void Test_ToLatLon(double a, double b, double c, double e, double f, double g)
        {
            ICoodrinateSupport q = new CoordinateSupport();
            ToLatLon k = new ToLatLon(Shifts_, q);

            LatLon Outcome = (LatLon)k.Calculate(f, e, g);
            LatLon Expected = new LatLon(a, b, c);

            Assert.AreEqual(Expected.Latitude, Outcome.Latitude, 0.01);
            Assert.AreEqual(Expected.Longtitude, Outcome.Longtitude, 0.01);
            Assert.AreEqual(Expected.Height, Outcome.Height, 0.01);
        }

        [TestCase(49.92226393730, -6.29977752014, 100.000, 91492.146, 11318.804, 46.519)]
        [TestCase(59.53470794490, -1.62516966058, 100.000, 421300.525, 1072147.239, 51.049)]
        [TestCase(59.85409913890, -1.27486910356, 149.890, 440725.073, 1107878.448, 100.989)]
        [TestCase(60.13308091660, -2.07382822798, 140.716, 395999.668, 1138728.951, 90.015)]
        public void Test_ToLatLon_AsCoodrinate(double a, double b, double c, double e, double f, double g)
        {
            ICoodrinateSupport q = new CoordinateSupport();
            ToLatLon k = new ToLatLon(Shifts_, q);

            EastingNorthing n = new EastingNorthing(f, e, g);
            var Outcome = (LatLon)k.Calculate(n);
            LatLon Expected = new LatLon(a, b, c);

            Assert.AreEqual(Expected.Latitude, Outcome.Latitude, 0.01);
            Assert.AreEqual(Expected.Longtitude, Outcome.Longtitude, 0.01);
            Assert.AreEqual(Expected.Height, Outcome.Height, 0.01);
        }

        [TestCase(49.92226393730, -6.29977752014, 100.000, 91492.146, 11318.804, 46.519)]
        [TestCase(59.85409913890, -1.27486910356, 149.890, 440725.073, 1107878.448, 100.989)]
        [TestCase(60.13308091660, -2.07382822798, 140.716, 395999.668, 1138728.951, 90.015)]
        public void Test_ToOSGB36_AsCoodrinate(double a, double b, double c, double e, double f, double g)
        {
            ICoodrinateSupport q = new CoordinateSupport();
            ToOSGB36 k = new ToOSGB36(Shifts_, q);

            LatLon n = new LatLon(a, b, c);
            var Outcome = k.Calculate(n);
            EastingNorthing Expected = new EastingNorthing(e, f, g);

            Assert.AreEqual(Expected, Outcome);
        }
    }
}
