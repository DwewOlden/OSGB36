using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.Interfaces
{
    /// <summary>
    /// The template for a collection that will store the shift information in a IEnumerable of IShift
    /// </summary>
    public interface IShifts:IEnumerable<IShift>
    {
        /// <summary>
        /// Loads the information about the shifts from a file on disk
        /// </summary>
        /// <param name="pFilename">The name of the file containing the shifts</param>
        /// <returns>True if the collection was correctly read, false if it was not</returns>
        bool LoadFromFile(string pFilename);

        /// <summary>
        /// Gets the value from the shift lookup table
        /// </summary>
        /// <param name="pIndex">The index of the shift</param>
        /// <returns>The easting value of the required index</returns>
        float GetEastingValue(int pIndex);

        /// <summary>
        /// Gets the value from the shift lookup table
        /// </summary>
        /// <param name="pIndex">The index of the shift</param>
        /// <returns>The height value of the required index</returns>
        float GetNorthingValue(int pIndex);

        /// <summary>
        /// Gets the value from the shift lookup table
        /// </summary>
        /// <param name="pIndex">The index of the shift</param>
        /// <returns>The height value of the required index</returns>
        float GetHeightValue(int pIndex);

        
    }
}
