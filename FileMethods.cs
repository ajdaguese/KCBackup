using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace KCBackup
{
    static class FileMethods
    {
        /**
         * takes two directories and compares their subdirectories and files. If any are the same, 
         * change the name of the subdirectories in the second argument so that copying can be done
         * without losing data. In the future may include a file compare as well. Returns a boolean
         * indicating whether or not a rename operation was done.
         */
        public static bool CheckDirectoryNames(string sourceDirectory, string destinationDirectory)
        {
            bool operationCompleted = false;
            string[] sourceList = Directory.GetDirectories(sourceDirectory);
            string[] destinationList = Directory.GetDirectories(destinationDirectory);
            convertPathToFolder(sourceList);
            convertPathToFolder(destinationList);
            List<string> sameDirectories = binarySearch(sourceList, destinationList);
            if (sameDirectories.Count > 0)
            {
                operationCompleted = true;
                foreach (string dir in sameDirectories)
                {
                    Directory.Move(destinationDirectory + dir, destinationDirectory + dir + DateTime.Now.ToString("-MM-dd-yyyy-h-mm-ss-tt"));
                    Console.WriteLine(dir);
                }
            }
            return operationCompleted;
        }
        /**
         * using a binary search algorithm to get a list of directories shared between the methods passed.
         * Most binary searches are O(logn) efficiency, this one will be O(nlogn) because we have to run it
         * on every element from the source directory. Returns a list of directory names to change
         */
        private static List<string> binarySearch(string[] source, string[] destination)
        {
            List<string> directoriesToChange = new List<string>();
            foreach (string dir in source)
            {
                if (binarySearchHelper(dir, destination))
                {
                    directoriesToChange.Add(dir);
                }
            }
            return directoriesToChange;
        }
        /**
         * A helper method for the binary search allowing the algorithm to recurse. Most of the algorithm processing takes place in this method.
         * A binary search works by looking at the middle of a sorted list and deciding if the search term is larger or smaller than that middle
         * index. If the search term is larger, repeat with the lower half of the list, if it is smaller repeat with the upper half. If they are
         * the same the item for which you are searching has been found.
         */
        private static bool binarySearchHelper(string searchString, string[] stringList)
        {
            bool foundString = false;
            if (stringList.Length > 1)
            {
                int mid = (stringList.Length - 1) / 2;
                if (searchString.CompareTo(stringList[mid]) < 0)
                {
                    string[] subArray = new string[stringList.Length / 2];
                    Array.Copy(stringList, 0, subArray, 0, mid);
                    foundString = binarySearchHelper(searchString, subArray);
                }
                else if (searchString.CompareTo(stringList[mid]) > 0)
                {
                    string[] subArray = new string[(stringList.Length - (mid + 1))];
                    Array.Copy(stringList, mid + 1, subArray, 0, (stringList.Length - (mid + 1)));
                    foundString = binarySearchHelper(searchString, subArray);
                }
                else
                {
                    foundString = true;
                }
            }
            else
            {
                if (searchString.CompareTo(stringList[0]) == 0)
                {
                    foundString = true;
                }
            }
            return foundString;
        }
        /**
         * This method converts an entire path into just the name of the folder at the end of the path
         */
        public static void convertPathToFolder(string[] path)
        {
            for (int i = 0; i < path.Length; i++)
            {
                string[] temp = path[i].Split('\\');
                path[i] = "\\" + temp[temp.Length - 1];
            }
        }
        /**
         * This function takes two files as input and returns a boolean indicating whether or not they are equal
         * code retrieved from https://stackoverflow.com/questions/1358510/how-to-compare-2-files-fast-using-net
         * user 'chsh', but commented by me
         */
        public static bool fileCompare(string file1Path, string file2Path)
        {
            const int BYTES_TO_READ = sizeof(Int64);
            FileInfo fi1 = new FileInfo(file1Path);
            FileInfo fi2 = new FileInfo(file2Path);
            //if the files are not the same size, they cannot be equal to each other.
            if(fi1.Length != fi2.Length)
            {
                return false;
            }
            //if the same file is passed in, they are definitely the same
            if(String.Compare(file1Path, file2Path, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
            //Math.Ceiling = always rounds up, this gives the number of iterations
            //of file compares by taking the size of the files and dividing by the
            //number of bytes being read at a time
            int iterations = (int)Math.Ceiling((double)fi1.Length / BYTES_TO_READ);
            using (FileStream fs1 = fi1.OpenRead())
            using (FileStream fs2 = fi2.OpenRead())
            {
                //the byte arrays of the files
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];
                //loops through the file one BYTES_TO_READ (int64) at a time
                //if one does not equal the other, return false because we found
                //a difference in the files, otherwise continue for the whole file
                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);
                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /**
         * takes a file and renames it to filename-oldXXXX where XXXX is presumably the number of old copies, I.E 0001 and 0002.
         * If some old copies have been deleted, the file being passed in will still be renamed with a number one larger than
         * highest numbered copy.
         */
        public static string fileRename(string path)
        {
            string newLocation = "";
            if(File.Exists(path))
            {
                string directory = Path.GetDirectoryName(path);
                string extension = Path.GetExtension(path);
                string highestFileCopy = null;
                foreach(string file in Directory.GetFiles(directory))
                {
                    //if the filename without the extention is the same as the current filename with "-old(" at the end and the file being observed
                    //has the same extension as the file passed in
                    if((file.StartsWith(path.Substring(0, path.Length - extension.Length) + "-old(")) && Path.GetExtension(file) == extension)
                    {
                        //since the array will be ordered, we only need to keep track of the most recently gotten file
                        highestFileCopy = file;
                    }
                }
                //if there are no already existing old versions of the file in this directory
                if(highestFileCopy == null)
                {
                    newLocation = path.Substring(0, path.Length - extension.Length);
                    newLocation += "-old(0001)" + extension;
                    File.Move(path, newLocation);
                }
                else
                {
                    //temp is set to the 4 numbers after -old in the highestFileCopy. This is decided by taking the original paths length and then
                    //subtracting the extension length. This will give us an index of the start of "-old(XXXX).X" in the highestFileCopy. From there
                    //we add 5 to that index making the start of substring the start of the 4 numbers and then we copy for indicies to temp.
                    string temp = highestFileCopy.Substring(path.Length - extension.Length + 5, 4);
                    //temp = temp.Substring(5, 4);
                    int currentNumber = Int32.Parse(temp);
                    currentNumber++;
                    string numberAsString = currentNumber.ToString();
                    if(numberAsString.Length > 5)
                    {
                        return null;
                    }
                    for(int i = numberAsString.Length; i < 4; i++)
                    {
                        numberAsString = '0' + numberAsString;
                    }
                    newLocation = path.Substring(0, path.Length - extension.Length);
                    newLocation += "-old(" + numberAsString + ")" + extension;
                    File.Move(path, newLocation);
                }
                return newLocation;
            }
            else
            {
                return null;
            }
        }
    }
}
