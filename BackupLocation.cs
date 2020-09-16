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
     * BackupLocation is a parent class. Most standard files can be implemented with profile
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

        public virtual void backup(string dest)
        {
            if (canUse && bolChecked)
            {

                //XXXXX Replaced with with Path.Combine
                //copy(dest + "\\" + separated[separated.Length - 1], realDirectory, false);
            /*    if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }*/
                copy(dest, realDirectory, false);
                if(!copiedFiles && Directory.Exists(dest))
                {
                    Directory.Delete(dest, true);
                }
            }
        }

        public virtual void restore(string backupLoc)
        {
            //string[] separated = realDirectory.Split('\\');
            copy(realDirectory, backupLoc, true);
        }

        //Need to change this method to update directory structure, removing '\\?\'
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

//public static class KnownFolder
//{
//    public static Guid AddNewPrograms = new Guid("de61d971-5ebc-4f02-a3a9-6c82895e5c04");
//    public static Guid AdminTools = new Guid("724EF170-A42D-4FEF-9F26-B60E846FBA4F");
//    public static Guid AppUpdates = new Guid("a305ce99-f527-492b-8b1a-7e76fa98d6e4");
//    public static Guid CDBurning = new Guid("9E52AB10-F80D-49DF-ACB8-4330F5687855");
//    public static Guid ChangeRemovePrograms = new Guid("df7266ac-9274-4867-8d55-3bd661de872d");
//    public static Guid CommonAdminTools = new Guid("D0384E7D-BAC3-4797-8F14-CBA229B392B5");
//    public static Guid CommonOEMLinks = new Guid("C1BAE2D0-10DF-4334-BEDD-7AA20B227A9D");
//    public static Guid CommonPrograms = new Guid("0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8");
//    public static Guid CommonStartMenu = new Guid("A4115719-D62E-491D-AA7C-E74B8BE3B067");
//    public static Guid CommonStartup = new Guid("82A5EA35-D9CD-47C5-9629-E15D2F714E6E");
//    public static Guid CommonTemplates = new Guid("B94237E7-57AC-4347-9151-B08C6C32D1F7");
//    public static Guid ComputerFolder = new Guid("0AC0837C-BBF8-452A-850D-79D08E667CA7");
//    public static Guid ConflictFolder = new Guid("4bfefb45-347d-4006-a5be-ac0cb0567192");
//    public static Guid ConnectionsFolder = new Guid("6F0CD92B-2E97-45D1-88FF-B0D186B8DEDD");
//    public static Guid Contacts = new Guid("56784854-C6CB-462b-8169-88E350ACB882");
//    public static Guid ControlPanelFolder = new Guid("82A74AEB-AEB4-465C-A014-D097EE346D63");
//    public static Guid Cookies = new Guid("2B0F765D-C0E9-4171-908E-08A611B84FF6");
//    public static Guid Desktop = new Guid("B4BFCC3A-DB2C-424C-B029-7FE99A87C641");
//    public static Guid Documents = new Guid("FDD39AD0-238F-46AF-ADB4-6C85480369C7");
//    public static Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
//    public static Guid Favorites = new Guid("1777F761-68AD-4D8A-87BD-30B759FA33DD");
//    public static Guid Fonts = new Guid("FD228CB7-AE11-4AE3-864C-16F3910AB8FE");
//    public static Guid Games = new Guid("CAC52C1A-B53D-4edc-92D7-6B2E8AC19434");
//    public static Guid GameTasks = new Guid("054FAE61-4DD8-4787-80B6-090220C4B700");
//    public static Guid History = new Guid("D9DC8A3B-B784-432E-A781-5A1130A75963");
//    public static Guid InternetCache = new Guid("352481E8-33BE-4251-BA85-6007CAEDCF9D");
//    public static Guid InternetFolder = new Guid("4D9F7874-4E0C-4904-967B-40B0D20C3E4B");
//    public static Guid Links = new Guid("bfb9d5e0-c6a9-404c-b2b2-ae6db6af4968");
//    public static Guid LocalAppData = new Guid("F1B32785-6FBA-4FCF-9D55-7B8E7F157091");
//    public static Guid LocalAppDataLow = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
//    public static Guid LocalizedResourcesDir = new Guid("2A00375E-224C-49DE-B8D1-440DF7EF3DDC");
//    public static Guid Music = new Guid("4BD8D571-6D19-48D3-BE97-422220080E43");
//    public static Guid NetHood = new Guid("C5ABBF53-E17F-4121-8900-86626FC2C973");
//    public static Guid NetworkFolder = new Guid("D20BEEC4-5CA8-4905-AE3B-BF251EA09B53");
//    public static Guid OriginalImages = new Guid("2C36C0AA-5812-4b87-BFD0-4CD0DFB19B39");
//    public static Guid PhotoAlbums = new Guid("69D2CF90-FC33-4FB7-9A0C-EBB0F0FCB43C");
//    public static Guid Pictures = new Guid("33E28130-4E1E-4676-835A-98395C3BC3BB");
//    public static Guid Playlists = new Guid("DE92C1C7-837F-4F69-A3BB-86E631204A23");
//    public static Guid PrintersFolder = new Guid("76FC4E2D-D6AD-4519-A663-37BD56068185");
//    public static Guid PrintHood = new Guid("9274BD8D-CFD1-41C3-B35E-B13F55A758F4");
//    public static Guid Profile = new Guid("5E6C858F-0E22-4760-9AFE-EA3317B67173");
//    public static Guid ProgramData = new Guid("62AB5D82-FDC1-4DC3-A9DD-070D1D495D97");
//    public static Guid ProgramFiles = new Guid("905e63b6-c1bf-494e-b29c-65b732d3d21a");
//    public static Guid ProgramFilesX64 = new Guid("6D809377-6AF0-444b-8957-A3773F02200E");
//    public static Guid ProgramFilesX86 = new Guid("7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E");
//    public static Guid ProgramFilesCommon = new Guid("F7F1ED05-9F6D-47A2-AAAE-29D317C6F066");
//    public static Guid ProgramFilesCommonX64 = new Guid("6365D5A7-0F0D-45E5-87F6-0DA56B6A4F7D");
//    public static Guid ProgramFilesCommonX86 = new Guid("DE974D24-D9C6-4D3E-BF91-F4455120B917");
//    public static Guid Programs = new Guid("A77F5D77-2E2B-44C3-A6A2-ABA601054A51");
//    public static Guid Public = new Guid("DFDF76A2-C82A-4D63-906A-5644AC457385");
//    public static Guid PublicDesktop = new Guid("C4AA340D-F20F-4863-AFEF-F87EF2E6BA25");
//    public static Guid PublicDocuments = new Guid("ED4824AF-DCE4-45A8-81E2-FC7965083634");
//    public static Guid PublicDownloads = new Guid("3D644C9B-1FB8-4f30-9B45-F670235F79C0");
//    public static Guid PublicGameTasks = new Guid("DEBF2536-E1A8-4c59-B6A2-414586476AEA");
//    public static Guid PublicMusic = new Guid("3214FAB5-9757-4298-BB61-92A9DEAA44FF");
//    public static Guid PublicPictures = new Guid("B6EBFB86-6907-413C-9AF7-4FC2ABF07CC5");
//    public static Guid PublicVideos = new Guid("2400183A-6185-49FB-A2D8-4A392A602BA3");
//    public static Guid QuickLaunch = new Guid("52a4f021-7b75-48a9-9f6b-4b87a210bc8f");
//    public static Guid Recent = new Guid("AE50C081-EBD2-438A-8655-8A092E34987A");
//    public static Guid RecycleBinFolder = new Guid("B7534046-3ECB-4C18-BE4E-64CD4CB7D6AC");
//    public static Guid ResourceDir = new Guid("8AD10C31-2ADB-4296-A8F7-E4701232C972");
//    public static Guid RoamingAppData = new Guid("3EB685DB-65F9-4CF6-A03A-E3EF65729F3D");
//    public static Guid SampleMusic = new Guid("B250C668-F57D-4EE1-A63C-290EE7D1AA1F");
//    public static Guid SamplePictures = new Guid("C4900540-2379-4C75-844B-64E6FAF8716B");
//    public static Guid SamplePlaylists = new Guid("15CA69B3-30EE-49C1-ACE1-6B5EC372AFB5");
//    public static Guid SampleVideos = new Guid("859EAD94-2E85-48AD-A71A-0969CB56A6CD");
//    public static Guid SavedGames = new Guid("4C5C32FF-BB9D-43b0-B5B4-2D72E54EAAA4");
//    public static Guid SavedSearches = new Guid("7d1d3a04-debb-4115-95cf-2f29da2920da");
//    public static Guid SEARCH_CSC = new Guid("ee32e446-31ca-4aba-814f-a5ebd2fd6d5e");
//    public static Guid SEARCH_MAPI = new Guid("98ec0e18-2098-4d44-8644-66979315a281");
//    public static Guid SearchHome = new Guid("190337d1-b8ca-4121-a639-6d472d16972a");
//    public static Guid SendTo = new Guid("8983036C-27C0-404B-8F08-102D10DCFD74");
//    public static Guid SidebarDefaultParts = new Guid("7B396E54-9EC5-4300-BE0A-2482EBAE1A26");
//    public static Guid SidebarParts = new Guid("A75D362E-50FC-4fb7-AC2C-A8BEAA314493");
//    public static Guid StartMenu = new Guid("625B53C3-AB48-4EC1-BA1F-A1EF4146FC19");
//    public static Guid Startup = new Guid("B97D20BB-F46A-4C97-BA10-5E3608430854");
//    public static Guid SyncManagerFolder = new Guid("43668BF8-C14E-49B2-97C9-747784D784B7");
//    public static Guid SyncResultsFolder = new Guid("289a9a43-be44-4057-a41b-587a76d7e7f9");
//    public static Guid SyncSetupFolder = new Guid("0F214138-B1D3-4a90-BBA9-27CBC0C5389A");
//    public static Guid System = new Guid("1AC14E77-02E7-4E5D-B744-2EB1AE5198B7");
//    public static Guid SystemX86 = new Guid("D65231B0-B2F1-4857-A4CE-A8E7C6EA7D27");
//    public static Guid Templates = new Guid("A63293E8-664E-48DB-A079-DF759E0509F7");
//    public static Guid TreeProperties = new Guid("5b3749ad-b49f-49c1-83eb-15370fbd4882");
//    public static Guid UserProfiles = new Guid("0762D272-C50A-4BB0-A382-697DCD729B80");
//    public static Guid UsersFiles = new Guid("f3ce0f7c-4901-4acc-8648-d5d44b04ef8f");
//    public static Guid Videos = new Guid("18989B1D-99B5-455B-841C-AB7C74E4DDFC");
//    public static Guid Windows = new Guid("F38BF404-1D43-42F2-9305-67DE0B28FC23");
//}

//public enum KNOWN_FOLDER_FLAG
//{
//    KF_FLAG_CREATE = 32768,
//    KF_FLAG_DONT_VERIFY = 16384,
//    KF_FLAG_DONT_UNEXPAND = 8192,
//    KF_FLAG_NO_ALIAS = 4096,
//    KF_FLAG_INIT = 2048,
//    KF_FLAG_DEFAULT_PATH = 1024,
//    KF_FLAG_NOT_PARENT_RELATIVE = 512,
//    KF_FLAG_SIMPLE_IDLIST = 256,
//    KF_FLAG_ALIAS_ONLY = 214783648
//}

//Other list of known folders
// # Define known folder GUIDs
//$KnownFolders = @{
//    'AddNewPrograms' = 'de61d971-5ebc-4f02-a3a9-6c82895e5c04';
//    'AdminTools' = '724EF170-A42D-4FEF-9F26-B60E846FBA4F';
//    'AppUpdates' = 'a305ce99-f527-492b-8b1a-7e76fa98d6e4';
//    'CDBurning' = '9E52AB10-F80D-49DF-ACB8-4330F5687855';
//    'ChangeRemovePrograms' = 'df7266ac-9274-4867-8d55-3bd661de872d';
//    'CommonAdminTools' = 'D0384E7D-BAC3-4797-8F14-CBA229B392B5';
//    'CommonOEMLinks' = 'C1BAE2D0-10DF-4334-BEDD-7AA20B227A9D';
//    'CommonPrograms' = '0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8';
//    'CommonStartMenu' = 'A4115719-D62E-491D-AA7C-E74B8BE3B067';
//    'CommonStartup' = '82A5EA35-D9CD-47C5-9629-E15D2F714E6E';
//    'CommonTemplates' = 'B94237E7-57AC-4347-9151-B08C6C32D1F7';
//    'ComputerFolder' = '0AC0837C-BBF8-452A-850D-79D08E667CA7';
//    'ConflictFolder' = '4bfefb45-347d-4006-a5be-ac0cb0567192';
//    'ConnectionsFolder' = '6F0CD92B-2E97-45D1-88FF-B0D186B8DEDD';
//    'Contacts' = '56784854-C6CB-462b-8169-88E350ACB882';
//    'ControlPanelFolder' = '82A74AEB-AEB4-465C-A014-D097EE346D63';
//    'Cookies' = '2B0F765D-C0E9-4171-908E-08A611B84FF6';
//    'Desktop' = @('B4BFCC3A-DB2C-424C-B029-7FE99A87C641');
//    'Documents' = @('FDD39AD0-238F-46AF-ADB4-6C85480369C7','f42ee2d3-909f-4907-8871-4c22fc0bf756');
//    'Downloads' = @('374DE290-123F-4565-9164-39C4925E467B','7d83ee9b-2244-4e70-b1f5-5393042af1e4');
//    'Favorites' = '1777F761-68AD-4D8A-87BD-30B759FA33DD';
//    'Fonts' = 'FD228CB7-AE11-4AE3-864C-16F3910AB8FE';
//    'Games' = 'CAC52C1A-B53D-4edc-92D7-6B2E8AC19434';
//    'GameTasks' = '054FAE61-4DD8-4787-80B6-090220C4B700';
//    'History' = 'D9DC8A3B-B784-432E-A781-5A1130A75963';
//    'InternetCache' = '352481E8-33BE-4251-BA85-6007CAEDCF9D';
//    'InternetFolder' = '4D9F7874-4E0C-4904-967B-40B0D20C3E4B';
//    'Links' = 'bfb9d5e0-c6a9-404c-b2b2-ae6db6af4968';
//    'LocalAppData' = 'F1B32785-6FBA-4FCF-9D55-7B8E7F157091';
//    'LocalAppDataLow' = 'A520A1A4-1780-4FF6-BD18-167343C5AF16';
//    'LocalizedResourcesDir' = '2A00375E-224C-49DE-B8D1-440DF7EF3DDC';
//    'Music' = @('4BD8D571-6D19-48D3-BE97-422220080E43','a0c69a99-21c8-4671-8703-7934162fcf1d');
//    'NetHood' = 'C5ABBF53-E17F-4121-8900-86626FC2C973';
//    'NetworkFolder' = 'D20BEEC4-5CA8-4905-AE3B-BF251EA09B53';
//    'OriginalImages' = '2C36C0AA-5812-4b87-BFD0-4CD0DFB19B39';
//    'PhotoAlbums' = '69D2CF90-FC33-4FB7-9A0C-EBB0F0FCB43C';
//    'Pictures' = @('33E28130-4E1E-4676-835A-98395C3BC3BB','0ddd015d-b06c-45d5-8c4c-f59713854639');
//    'Playlists' = 'DE92C1C7-837F-4F69-A3BB-86E631204A23';
//    'PrintersFolder' = '76FC4E2D-D6AD-4519-A663-37BD56068185';
//    'PrintHood' = '9274BD8D-CFD1-41C3-B35E-B13F55A758F4';
//    'Profile' = '5E6C858F-0E22-4760-9AFE-EA3317B67173';
//    'ProgramData' = '62AB5D82-FDC1-4DC3-A9DD-070D1D495D97';
//    'ProgramFiles' = '905e63b6-c1bf-494e-b29c-65b732d3d21a';
//    'ProgramFilesX64' = '6D809377-6AF0-444b-8957-A3773F02200E';
//    'ProgramFilesX86' = '7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E';
//    'ProgramFilesCommon' = 'F7F1ED05-9F6D-47A2-AAAE-29D317C6F066';
//    'ProgramFilesCommonX64' = '6365D5A7-0F0D-45E5-87F6-0DA56B6A4F7D';
//    'ProgramFilesCommonX86' = 'DE974D24-D9C6-4D3E-BF91-F4455120B917';
//    'Programs' = 'A77F5D77-2E2B-44C3-A6A2-ABA601054A51';
//    'Public' = 'DFDF76A2-C82A-4D63-906A-5644AC457385';
//    'PublicDesktop' = 'C4AA340D-F20F-4863-AFEF-F87EF2E6BA25';
//    'PublicDocuments' = 'ED4824AF-DCE4-45A8-81E2-FC7965083634';
//    'PublicDownloads' = '3D644C9B-1FB8-4f30-9B45-F670235F79C0';
//    'PublicGameTasks' = 'DEBF2536-E1A8-4c59-B6A2-414586476AEA';
//    'PublicMusic' = '3214FAB5-9757-4298-BB61-92A9DEAA44FF';
//    'PublicPictures' = 'B6EBFB86-6907-413C-9AF7-4FC2ABF07CC5';
//    'PublicVideos' = '2400183A-6185-49FB-A2D8-4A392A602BA3';
//    'QuickLaunch' = '52a4f021-7b75-48a9-9f6b-4b87a210bc8f';
//    'Recent' = 'AE50C081-EBD2-438A-8655-8A092E34987A';
//    'RecycleBinFolder' = 'B7534046-3ECB-4C18-BE4E-64CD4CB7D6AC';
//    'ResourceDir' = '8AD10C31-2ADB-4296-A8F7-E4701232C972';
//    'RoamingAppData' = '3EB685DB-65F9-4CF6-A03A-E3EF65729F3D';
//    'SampleMusic' = 'B250C668-F57D-4EE1-A63C-290EE7D1AA1F';
//    'SamplePictures' = 'C4900540-2379-4C75-844B-64E6FAF8716B';
//    'SamplePlaylists' = '15CA69B3-30EE-49C1-ACE1-6B5EC372AFB5';
//    'SampleVideos' = '859EAD94-2E85-48AD-A71A-0969CB56A6CD';
//    'SavedGames' = '4C5C32FF-BB9D-43b0-B5B4-2D72E54EAAA4';
//    'SavedSearches' = '7d1d3a04-debb-4115-95cf-2f29da2920da';
//    'SEARCH_CSC' = 'ee32e446-31ca-4aba-814f-a5ebd2fd6d5e';
//    'SEARCH_MAPI' = '98ec0e18-2098-4d44-8644-66979315a281';
//    'SearchHome' = '190337d1-b8ca-4121-a639-6d472d16972a';
//    'SendTo' = '8983036C-27C0-404B-8F08-102D10DCFD74';
//    'SidebarDefaultParts' = '7B396E54-9EC5-4300-BE0A-2482EBAE1A26';
//    'SidebarParts' = 'A75D362E-50FC-4fb7-AC2C-A8BEAA314493';
//    'StartMenu' = '625B53C3-AB48-4EC1-BA1F-A1EF4146FC19';
//    'Startup' = 'B97D20BB-F46A-4C97-BA10-5E3608430854';
//    'SyncManagerFolder' = '43668BF8-C14E-49B2-97C9-747784D784B7';
//    'SyncResultsFolder' = '289a9a43-be44-4057-a41b-587a76d7e7f9';
//    'SyncSetupFolder' = '0F214138-B1D3-4a90-BBA9-27CBC0C5389A';
//    'System' = '1AC14E77-02E7-4E5D-B744-2EB1AE5198B7';
//    'SystemX86' = 'D65231B0-B2F1-4857-A4CE-A8E7C6EA7D27';
//    'Templates' = 'A63293E8-664E-48DB-A079-DF759E0509F7';
//    'TreeProperties' = '5b3749ad-b49f-49c1-83eb-15370fbd4882';
//    'UserProfiles' = '0762D272-C50A-4BB0-A382-697DCD729B80';
//    'UsersFiles' = 'f3ce0f7c-4901-4acc-8648-d5d44b04ef8f';
//    'Videos' = @('18989B1D-99B5-455B-841C-AB7C74E4DDFC','35286a68-3c57-41a1-bbb1-0eae73d76c95');
//    'Windows' = 'F38BF404-1D43-42F2-9305-67DE0B28FC23';

