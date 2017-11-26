using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Interfaces
{
    public interface ICalculationMethod
    {
        ICoordinate Calculate(ICoordinate pCoordinate);

        ICoordinate Calculate(double pHorizontal, double pVertical, double pHeight);
    }
}
