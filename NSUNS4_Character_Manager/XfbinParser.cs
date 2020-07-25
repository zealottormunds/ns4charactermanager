using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
    public class XfbinParser
    {
        // Header

        public static int HeaderSize = 0x44;

        public static byte[] GetHeader(byte[] fileBytes)
        {
            return Main.b_ReadByteArray(fileBytes, 0, HeaderSize);
        }

        // Nucc

        public static int GetNuccSectionIndex()
        {
            return HeaderSize;
        }

        public static int GetNuccSectionSize(byte[] fileBytes)
        {
            return Main.b_byteArrayToIntRev(Main.b_ReadByteArray(fileBytes, 0x20, 4));
        }

        // Path

        public static int GetPathSectionIndex(byte[] fileBytes)
        {
            return GetNuccSectionIndex() + GetNuccSectionSize(fileBytes);
        }

        public static int GetPathSectionSize(byte[] fileBytes)
        {
            return Main.b_byteArrayToIntRev(Main.b_ReadByteArray(fileBytes, 0x28, 4));
        }

        public static int GetPathCount(byte[] fileBytes)
        {
            return Main.b_byteArrayToIntRev(Main.b_ReadByteArray(fileBytes, 0x24, 4)) - 1;
        }

        public static List<string> GetPathList(byte[] fileBytes)
        {
            List<string> pathList = new List<string>();

            int pathCount = GetPathCount(fileBytes);
            int index = GetPathSectionIndex(fileBytes) + 1;

            for(int x = 0; x < pathCount; x++)
            {
                string actualPath = Main.b_ReadString(fileBytes, index);
                pathList.Add(actualPath);
                index = index + actualPath.Length + 1;
            }

            return pathList;
        }

        // Name

        public static int GetNameSectionIndex(byte[] fileBytes)
        {
            return GetPathSectionIndex(fileBytes) + GetPathSectionSize(fileBytes);
        }

        public static int GetNameSectionSize(byte[] fileBytes)
        {
            return Main.b_byteArrayToIntRev(Main.b_ReadByteArray(fileBytes, 0x30, 4));
        }

        public static List<string> GetNameList(byte[] fileBytes)
        {
            List<string> nameList = new List<string>();

            int nameCount = GetPathCount(fileBytes);
            int index = GetNameSectionIndex(fileBytes) + 1;

            for (int x = 0; x < nameCount; x++)
            {
                string actualName = Main.b_ReadString(fileBytes, index);
                nameList.Add(actualName);
                index = index + actualName.Length + 1;
            }

            return nameList;
        }

        // Bin 1

        public static int GetBin1SectionIndex(byte[] fileBytes)
        {
            int ind = GetNameSectionIndex(fileBytes) + GetNameSectionSize(fileBytes);
            while (ind % 4 != 0) ind++;
            return ind;
        }

        public static int GetBin1SectionSize(byte[] fileBytes)
        {
            return Main.b_byteArrayToIntRev(Main.b_ReadByteArray(fileBytes, 0x38, 0x4));
        }

        // Bin 2

        public static int GetBin2SectionIndex(byte[] fileBytes)
        {
            int ind = GetBin1SectionIndex(fileBytes) + GetBin1SectionSize(fileBytes);
            return ind;
        }

        public static int GetBin2SectionSize(byte[] fileBytes)
        {
            return GetPathCount(fileBytes) * 0x10;
        }

        // File Section

        public static int GetFileSectionIndex(byte[] fileBytes)
        {
            return GetBin2SectionIndex(fileBytes) + GetBin2SectionSize(fileBytes);
        }

        // Write string section
        public static byte[] WriteStringSection(List<string> strings)
        {
            byte[] actual = { };

            for(int x = 0; x < strings.Count; x++)
            {
                actual = Main.b_AddBytes(actual, new byte[] { 0 });
                actual = Main.b_AddString(actual, strings[x]);
                actual = Main.b_AddBytes(actual, new byte[] { 0 });
            }

            return actual;
        }

        // Find string
        public static int FindString(byte[] fileBytes, string toFind, int startIndex = 0)
        {
            return FindBytes(fileBytes, Encoding.ASCII.GetBytes(toFind), startIndex);
        }

        // Find bytes
        public static int FindBytes(byte[] fileBytes, byte[] toFind, int startIndex = 0)
        {
            int ind = -1;

            for(int x = startIndex; x < fileBytes.Length; x++)
            {
                bool found = true;
                int actual = 0;
                for(int y = 0; y < toFind.Length; y++)
                {
                    if(fileBytes[x + y] != toFind[actual])
                    {
                        //MessageBox.Show(actual.ToString("X2") + ": " + fileBytes[x + y].ToString("X2") + " != " + toFind[actual].ToString("X2"));
                        found = false;
                        y = toFind.Length;
                    }
                    else
                    {
                        //MessageBox.Show(actual.ToString("X2") + ": " + fileBytes[x + y].ToString("X2") + " == " + toFind[actual].ToString("X2"));
                        actual++;
                    }
                }

                if(found)
                {
                    ind = x;
                    x = fileBytes.Length;
                }
            }

            return ind;
        }
    }
}
