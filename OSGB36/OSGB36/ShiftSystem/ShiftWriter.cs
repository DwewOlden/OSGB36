using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGB36.ShiftSystem
{
    /// <summary>
    /// A class that takes the path to a OSGB36 shift file and extracts the shift information alone to a
    /// smaller binary file.
    /// </summary>
    /// <remarks>This is not designed to be a regular operation, as soon as the file is captured it should 
    /// be safe for a number of years. The source file can be downloaded from the ordnance survey site</remarks>
    public class ShiftWriter
    {
        /// <summary>
        /// The column in the line containing the easting shift information
        /// </summary>
        internal const int EASTING_INDEX = 3;

        /// <summary>
        /// The column in the line containing the northing shift information
        /// </summary>
        internal const int NORTHING_INDEX = 4;

        /// <summary>
        /// The column in the line containing the height shift information
        /// </summary>
        internal const int HEIGHT_INDEX = 5;

        /// <summary>
        /// The number of columns in the text file
        /// </summary>
        internal const int NUMBER_OF_COLUMNS = 7;

        /// <summary>
        /// Reads the informaition from the OS provided file
        /// </summary>
        internal StreamReader mReader;

        /// <summary>
        /// Writes the binary information to back to disk
        /// </summary>
        internal BinaryWriter mWriter;
        
        /// <summary>
        /// The name of the OS provided file containing the shift information
        /// </summary>
        internal string mOSFilename = @"C:\temp\OSTN15_OSGM15_DataFile.txt";

        /// <summary>
        /// The name of the file where the shift information will be saved in binary format.
        /// </summary>
        internal string mProcessedFile = @"C:\temp\OSTN02dat.dat";

        public ShiftWriter()
        {
            InitalizeIO();
        }

        /// <summary>
        /// Checks if the file (OS) exists. If it does then it opens it for reading along with a binary writer for the 
        /// output.
        /// </summary>
        private void InitalizeIO()
        {
            if (!File.Exists(mOSFilename))
                throw new FileNotFoundException("the OS data file does not exist at the specified path");

            mReader = new StreamReader(mOSFilename);
            mWriter = new BinaryWriter(File.Open(mProcessedFile, FileMode.Create));
        }

        public bool ProcessOSSourceFile()
        {
            try
            {
                string line = mReader.ReadLine();
                if (!line.StartsWith("Point_ID"))
                    return false;

                line = mReader.ReadLine();
               
                while (line != null)
                {
                    bool lineProcessedOK = ProcessALine(line);
                    if (!lineProcessedOK)
                        return false;

                    line = mReader.ReadLine();
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseIOStreams();
            }
        }

        /// <summary>
        /// Is passed a line, normally read from the OS data file and extract the shift information, saving the shift information
        /// in binary format to the writer.
        /// </summary>
        /// <param name="line">The link read from the OS file to be processed</param>
        /// <returns>True of the line could be processed, false if it could not.</returns>
        private bool ProcessALine(string line)
        {
            if ((string.IsNullOrEmpty(line)) || (string.IsNullOrWhiteSpace(line)))
                return false;

            string[] lineParts = line.Split(',');
            if (lineParts.Length != NUMBER_OF_COLUMNS)
                throw new ArgumentOutOfRangeException("there are not enougth columns in one of the lines of the file");
            try
            {
                Single _e = Convert.ToSingle(lineParts[EASTING_INDEX]);
                Single _n = Convert.ToSingle(lineParts[NORTHING_INDEX]);
                Single _h = Convert.ToSingle(lineParts[HEIGHT_INDEX]);


                mWriter.Write(_e);
                mWriter.Write(_n);
                mWriter.Write(_h);
                mWriter.Flush();
                
            } catch (IOException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }


            return true;
        }

        /// <summary>
        /// Makes sure both the input and output streams are closed
        /// </summary>
        private void CloseIOStreams()
        {
            mReader.Close();
            mReader.Dispose();
            mReader = null;

            mWriter.Close();
            mWriter.Dispose();
            mWriter = null;
        }
    }
}
