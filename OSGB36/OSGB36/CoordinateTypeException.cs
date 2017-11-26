using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36
{
    public class CoordinateTypeException:Exception
    {
        public CoordinateTypeException(string message) : base(message)
        {

        }
    }
}
