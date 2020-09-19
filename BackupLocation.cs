using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace KCBackup
{
    /**
     * BackupLocation is an abstract parent class. Most standard files can be implemented with profile
     * but anything that needs special attention can extend it and override the "special"
     * method. All special methods will be run, but many may be empty
     */
    abstract class BackupLocation
    {
        public string name { get; }
        //The orignal directory may contain some text to replace. It is replaced and stored into the real directory
        public string originalDirectory { get; }
        protected string realDirectory { get; set; }
        //This will be used for things not required in every configuration such as the firefox version number.
        protected string extraConfigWrites = "";
        protected Boolean canUse = true;
        protected Boolean bolChecked;
        //a boolean that is set to true as long as at least one file copies
        private Boolean copiedFiles = false;
        //the type string is used to indicate which subclass the instantiated object is so that it can be stored
        //in the config file
        protected BackupType type;
        /**constructs a BackupLocation and sets the name, the checked boolean, the original directory path, and the real directory which
         * is the orignal directory with anything between two '%' translated into the correct path
         */
        public BackupLocation(string pName, string pDir, Boolean pChecked)
        {
            name = pName;
            bolChecked = pChecked;
            originalDirectory = pDir;
            if (originalDirectory == null)
            {
                canUse = false;
                MessageBox.Show("Profile: " + name + " has an invalid directory path and will be ignored");
            }
            realDirectory = pathTranslate(originalDirectory);
            if (realDirectory == null)
            {
                canUse = false;
                MessageBox.Show("Profile: " + name + " has an invalid directory path and will be ignored");
            }
        }
        /**
         * takes a relative path and translates anything found between two '%' into the coorelating absolute path.
         * If there is not a translation for the abstract path, returns null
         */
        protected string pathTranslate(string path)
        {
            //a list of the individual folders from the path separated
            string[] separated = path.Split('\\');

            for (int i = 0; i < separated.Length; i++)
            {
                if (separated[i].StartsWith("%") && separated[i].EndsWith("%"))
                {
                    //checks if the string contains 'user' and if it's length is 6. If both of these are true along with the higher if statement, the string must be '%user%'
                    if (separated[i].ToLower().Contains("userprofile") && separated[i].Length == "userprofile".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Profile);//Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    }
                    else if (separated[i].ToLower().Contains("appdatalocal") && separated[i].Length == "appdataLocal".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.LocalAppData);//Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    }
                    else if (separated[i].ToLower().Contains("pictures") && separated[i].Length == "Pictures".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Pictures);//Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    }
                    else if (separated[i].ToLower().Contains("appdataroaming") && separated[i].Length == "appdataroaming".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.RoamingAppData);//Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    }
                    else if (separated[i].ToLower().Contains("documents") && separated[i].Length == "documents".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Documents);//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }
                    else if (separated[i].ToLower().Contains("downloads") && separated[i].Length == "downloads".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Downloads);//GetDownloadPath();
                    }
                    else if (separated[i].ToLower().Contains("desktop") && separated[i].Length == "desktop".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Desktop);//Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    }
                    else if (separated[i].ToLower().Contains("favorites") && separated[i].Length == "favorites".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Favorites);//Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                    }
                    else if (separated[i].ToLower().Contains("music") && separated[i].Length == "music".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Music);//Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                    }
                    else if (separated[i].ToLower().Contains("videos") && separated[i].Length == "videos".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Videos);//Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                    }

                    else if (separated[i].ToLower().Contains("publicuserprofile") && separated[i].Length == "publicuserprofile".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.Public);//Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    }
                    else if (separated[i].ToLower().Contains("publicpictures") && separated[i].Length == "publicPictures".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.PublicPictures);//Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    }
                    else if (separated[i].ToLower().Contains("publicdocuments") && separated[i].Length == "publicdocuments".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.PublicDocuments);//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }
                    else if (separated[i].ToLower().Contains("publicdownloads") && separated[i].Length == "publicdownloads".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.PublicDownloads);//GetDownloadPath();
                    }
                    else if (separated[i].ToLower().Contains("publicdesktop") && separated[i].Length == "publicdesktop".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.PublicDesktop);//Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    }
                    else if (separated[i].ToLower().Contains("publicmusic") && separated[i].Length == "publicmusic".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.PublicMusic);//Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                    }
                    else if (separated[i].ToLower().Contains("publicvideos") && separated[i].Length == "publicvideos".Length + 2)
                    {
                        separated[i] = KnownFolders.GetPath(KnownFolder.PublicVideos);//Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                    }
                    else
                    {
                        //if this string is not one to replace, return null
                        return null;
                    }
                }
            }
            string reconstructed = string.Join("\\", separated);
            Console.WriteLine(reconstructed);
            return reconstructed;
        }
        /**
         * writes all the data that might be needed to restore the directory in this object to the configuration file.
         */
        public void writeToConfig(string configLocation)
        {
            if (bolChecked && canUse && copiedFiles)
            {
                FileStream fs = new FileStream(configLocation, FileMode.Append);
                byte[] configWrite = Encoding.Default.GetBytes("+" + name + "\n" + "-DIRECTORY=" + originalDirectory + "\n" + "-TYPE=" + type.ToString() + "\n" + extraConfigWrites);
                fs.Write(configWrite, 0, configWrite.Length);
                fs.Flush();
                fs.Close();
            }
        }

       /* public int getDirectorySize()
        {
            return Directory.
        }*/
        //this method is not used here, but may be used by a child class. Virtual methods can be overridden in a child class
        public virtual void specialActions()
        {
        }
        /**
         * backs up the directory this object contains to the given destination
         */
        public virtual void backup(string dest)
        {
            if (canUse && bolChecked)
            {
                copy(dest, realDirectory, false);
                if(!copiedFiles && Directory.Exists(dest))
                {
                    Directory.Delete(dest, true);
                }
            }
        }
        /**
         * restores information from the given location to the directory stored in this object
         */
        public virtual void restore(string backupLoc)
        {
            //string[] separated = realDirectory.Split('\\');
            copy(realDirectory, backupLoc, true);
        }

        /**
         * copies all of the files in a directory and every subdirectory to a separate location
         */
        private void copy(string tempDest, string tempSource, bool restore)
        {
            string dest;
            if (!tempDest.StartsWith(@"\\?\"))
            {
                dest = @"\\?\" + tempDest;
            }
            else
            {
                dest = tempDest;
            }
            string source;
            if (!tempSource.StartsWith(@"\\?\"))
            {
                source = @"\\?\" + tempSource;
            }
            else
            {
                source = tempSource;
            }
            string logfile = FormBackup.backupRoot + "\\log.txt";//Changed broken "frmBackup" to "FormBackup"
            if (!(File.Exists(logfile)))
            {
                if (!Directory.Exists(FormBackup.backupRoot))//Changed broken "frmBackup" to "FormBackup"
                {
                    Directory.CreateDirectory(FormBackup.backupRoot);//Changed broken "frmBackup" to "FormBackup"
                }
                File.Create(logfile).Close();
            }
            if (bolChecked && canUse)
            {
                string[] split = source.Split('\\');
                try
                {
                    for (int i = 0; i < Directory.GetDirectories(source).Length; i++)
                    {
                        string dir = "";
                        try
                        {
                            dir = Directory.GetDirectories(source)[i];
                            //avoids recursing down the directory to which we are currently backing up
                            //otherwise we will loop infinitely. All other backups done by this program
                            //will copy
                            if (dir != @"\\?\" + FormBackup.backupRoot + FormBackup.currentBackup)
                            {

                                string dirName = Path.GetFileName(dir);
                                if (!Directory.Exists(Path.Combine(dest, dirName)))
                                {
                                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                                }
                                copy(Path.Combine(dest, dirName), dir, restore);
                            }
                        }
                        catch (Exception e)
                        {
                            FileStream fs = new FileStream(logfile, FileMode.Append);
                            byte[] configWrite = Encoding.Default.GetBytes(e.Message + "\n" + "directory: " + dir + "\n\n");
                            fs.Write(configWrite, 0, configWrite.Length);
                            fs.Flush();
                            fs.Close();
                        }
                    }
                }
                catch(Exception e)
                {
                    FileStream fs = new FileStream(logfile, FileMode.Append);
                    byte[] configWrite = Encoding.Default.GetBytes("The directory at " + source + "could not be accessed. It is likely that you do not have the required permissions" + "\n\n");
                    fs.Write(configWrite, 0, configWrite.Length);
                    fs.Flush();
                    fs.Close();
                }
                try
                {
                    for (int i = 0; i < Directory.GetFiles(source).Length; i++)
                    {
                        string file = Directory.GetFiles(source)[i];
                        if (!(file.EndsWith("desktop.ini", StringComparison.OrdinalIgnoreCase) || file.EndsWith("thumbs.db", StringComparison.OrdinalIgnoreCase)))
                        {
                            try
                            {
                                //Not checking for directory causes a problem when back up a directory with no sub-directories
                                //in which the backup destination direcory does not exist and can't be copied to.
                                if (!Directory.Exists(dest))
                                {
                                    Directory.CreateDirectory(dest);
                                }
                                if (restore)
                                {
                                    if (File.Exists(Path.Combine(dest, Path.GetFileName(file))))
                                    {
                                        if (!FileMethods.fileCompare(file, Path.Combine(dest, Path.GetFileName(file))))
                                        {
                                            FileMethods.fileRename(Path.Combine(dest, Path.GetFileName(file)));
                                            File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
                                        }
                                    }
                                    else
                                    {
                                        File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
                                    }
                                }
                                else
                                {
                                    File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
                                }
                                copiedFiles = true;
                            }
                            catch (Exception e)
                            {
                                FileStream fs = new FileStream(logfile, FileMode.Append);
                                byte[] configWrite = Encoding.Default.GetBytes(e.Message + "\n" + "File: " + file + "\n\n");
                                fs.Write(configWrite, 0, configWrite.Length);
                                fs.Flush();
                                fs.Close();
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    FileStream fs = new FileStream(logfile, FileMode.Append);
                    byte[] configWrite = Encoding.Default.GetBytes("The directory at " + source + "could not be accessed. It is likely that you do not have the required permissions" + "\n\n");
                    fs.Write(configWrite, 0, configWrite.Length);
                    fs.Flush();
                    fs.Close();
                }
            }
            //by checking that this option was checked, we only display the error message if there was a problem with the directory
            else if (bolChecked)
            {
                MessageBox.Show("Could not back up folder " + name);
            }
        }
        /**
         * Taken from https://stackoverflow.com/questions/10667012/getting-downloads-folder-in-c and modified/hardcoded to only work with the downloads
         * folder, which is the folder that I need. Uses a shell32 dll to get the path of downloads using its guid
         */
        //public static string GetDownloadPath()
        //{
        //    int result = SHGetKnownFolderPath(new Guid("374DE290-123F-4565-9164-39C4925E467B"),
        //        (uint)0x00004000, new IntPtr(0), out IntPtr outPath);
        //    if (result >= 0)
        //    {
        //        string path = Marshal.PtrToStringUni(outPath);
        //        Marshal.FreeCoTaskMem(outPath);
        //        return path;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Could not get path to Downloads");
        //        return null;
        //    }
        //}

        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath(
        [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken,
        out IntPtr ppszPath);
        
        
    }
    /*
     * this enum is for stroing the class type a backup object is so that I can ensure 
     * consistency in the config file
     */
    public enum BackupType
    {
        userfiles,
        programfiles,
        firefox,
        chrome,
        extrauserfiles,
        desktop,
        strayfolder,
        publicuserfiles,
        publicdesktop
    }
}