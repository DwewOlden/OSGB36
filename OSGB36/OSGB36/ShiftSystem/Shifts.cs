using OSGB36.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace OSGB36.ShiftSystem
{
    /// <summary>
    /// Reads the shift information from disk and store it in an internal collection
    /// </summary>
    public class Shifts : IShifts
    {
        /// <summary>
        /// Value defined by the OS as being the number of 'rows' in the file { a row being three values of E,N,H }
        /// </summary>
        private const int OSTN02_ELEMENTS = 876951;

        /// <summary>
        /// The elements in the list
        /// </summary>
        private IShift[] mElements;

        /// <summary>
        /// The number of elements in the list
        /// </summary>
        private int mElementsCount = -1;


        public IShift this[int pIndex]
        {
            get
            {
                ValidateIndex(pIndex);
                return mElements[pIndex];
            }
        }

        /// <summary>
        /// Gets the easting value from the member with the passed index
        /// </summary>
        /// <param name="pIndex">The index we want to retreive</param>
        /// <returns>The value from the list</returns>
        public float GetEastingValue(int pIndex)
        {
            ValidateIndex(pIndex);
            return mElements[pIndex].Easting;
        }

        /// <summary>
        /// Gets the height value from the member with the passed index
        /// </summary>
        /// <param name="pIndex">The index we want to retreive</param>
        /// <returns>The value from the list</returns>
        public float GetHeightValue(int pIndex)
        {
            ValidateIndex(pIndex);
            return mElements[pIndex].Height;
        }

        /// <summary>
        /// Gets the northing value from the member with the passed index
        /// </summary>
        /// <param name="pIndex">The index we want to retreive</param>
        /// <returns>The value from the list</returns>
        public float GetNorthingValue(int pIndex)
        {
            ValidateIndex(pIndex);
            return mElements[pIndex].Northing;
        }

        /// <summary>
        /// Loads the data from the passed file and populates the internal structure
        /// </summary>
        /// <param name="pFilename">The name of the file to be loaded</param>
        /// <returns>True if the method was loaded successfully, false if it was not</returns>
        public bool LoadFromFile(string pFilename)
        {
            // If the file does not exist in the passed path then return false,
            if (File.Exists(pFilename) == false)
                return false;
            else
            {

                // Note the odd number, this is to remain consistent with the OS documention on this subject.
                mElements = new IShift[OSTN02_ELEMENTS + 1];
                return CompleteReadingOperation(pFilename);
            }
        }

        /// <summary>
        /// Opens the file and performs the reading operation.
        /// </summary>
        /// <param name="pFilename">The name of the file to be loaded</param>
        /// <returns>True if the method was loaded successfully, false if it was not</returns>
        private bool CompleteReadingOperation(string pFilename)
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(pFilename, FileMode.Open));
                mElementsCount++;

                if (reader.BaseStream.CanRead)
                {
                    for (int Index = 1; Index <= OSTN02_ELEMENTS; Index++)
                    {
                        Shift s = new Shift(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        mElements[Index] = s;
                        mElementsCount++;
                    }
                }
                else
                    return false;

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Enumeration Overloads
        public IEnumerator<IShift> GetEnumerator()
        {
            foreach (IShift s in mElements)
                if ((s != null))
                    yield return s;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Checks the passed index does not exceed the number of elements in the colleciton
        /// </summary>
        /// <param name="pIndex">The index we are checking for</param>
        private void ValidateIndex(int pIndex)
        {
            if (pIndex > this.mElementsCount)
                throw new ArgumentOutOfRangeException("pIndex", "the reqested index exceeds the the number of elements in the collection");
        }
    }
}
