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

        /// <summary>
        /// Uses the contents of the lookup shift calculator to retirece a interpolated
        /// shift position.
        /// </summary>
        /// <param name="pLookupX">The x part of the coordinate</param>
        /// <param name="pLookupY">The y part of the coodrinate</param>
        /// <returns>A shift lookup or null if it is out of bounds / error occurs</returns>
        public IShift CalculateShift(double pLookupX, double pLookupY)
        {
            int eIndex = (int)(Math.Floor(pLookupX / 1000));
            int nIndex = (int)(Math.Floor(pLookupY / 1000));
            
            Shift s0 = (Shift)LookupShift(eIndex, nIndex);             // shifts
            Shift s1 = (Shift)LookupShift((eIndex + 1), nIndex);
            Shift s2 = (Shift)LookupShift((eIndex + 1), (nIndex + 1));
            Shift s3 = (Shift)LookupShift(eIndex, (nIndex + 1));

            double X0 = 0, Y0 = 0;

            X0 = eIndex * 1000;
            Y0 = nIndex * 1000;

            double dX = pLookupX - X0;
            double dY = pLookupY - Y0;
            double t = dX / 1000;
            double u = dY / 1000;

            double eS = ((1 - t) * (1 - u) * (s0.Easting)) + ((t) * (1 - u) * s1.Easting) + ((t) * (u) * s2.Easting) + ((1 - t) * (u) * s3.Easting);
            double nS = ((1 - t) * (1 - u) * (s0.Northing)) + ((t) * (1 - u) * s1.Northing) + ((t) * (u) * s2.Northing) + ((1 - t) * (u) * s3.Northing);
            double hS = ((1 - t) * (1 - u) * (s0.Height)) + ((t) * (1 - u) * s0.Height) + ((t) * (u) * s0.Height) + ((1 - t) * (u) * s0.Height);

            return new Shift((float)eS, (float)nS, (float)hS);
        }

        private IShift LookupShift(int ei, int ni)
        {
            // Calculate the index in the shift array that will provide the information 
            // required
            int record_number = (ei + (ni * 701) + 1);
            return this[record_number];
        }
    }
}
