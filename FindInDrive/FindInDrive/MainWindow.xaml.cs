using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FindInDrive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        static StringBuilder objStringBuilder = new StringBuilder();
        static int totalFilesScanned = 0;
        static int totalSearchFound = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events

        /// <summary>
        /// This function searches the given text in the drive specified by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = string.Empty;
            txtOutput.Text = string.Empty;

            if (txtDriveName.Text.Trim().Equals(string.Empty) && (chkAllDrives.IsChecked != null && chkAllDrives.IsChecked == false))
            {
                lblError.Content = "Please Enter Valid Drive Path.";
            }
            else if (txtSearchText.Text.Trim().Equals(string.Empty))
            {
                lblError.Content = "Please Enter Valid Text For Search.";
            }
            else if (txtOutputFileLocation.Text.Trim().Equals(string.Empty) || Directory.Exists(txtOutputFileLocation.Text.Trim()) == false)
            {
                lblError.Content = "Please Enter Valid Output File Path.";
            }
            else
            {

                if (chkAllDrives.IsChecked != null && chkAllDrives.IsChecked == true)
                {
                    //Start with drives if you have to search the entire computer. 
                    string[] drives = System.Environment.GetLogicalDrives();

                    foreach (string dr in drives)
                    {
                        System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                        //Here we skip the drive if it is not ready to be read. This 
                        //is not necessarily the appropriate action in all scenarios. 
                        if (!di.IsReady)
                        {
                            log.Add("The drive " + di.Name + " could not be read");
                            continue;
                        }
                        System.IO.DirectoryInfo rootDir = di.RootDirectory;
                        TraverseTheDirectoryTree(rootDir);
                        break;
                    }
                }
                else
                {
                    System.IO.DirectoryInfo rootDir = new DirectoryInfo(txtDriveName.Text.Trim());
                    TraverseTheDirectoryTree(rootDir);
                }

                // Write out all the files that could not be processed.
                string exceptions = "Files and Drives with restricted access and exceptions:";
                if (log.Count == 0)
                {
                    exceptions = exceptions + System.Environment.NewLine;
                    exceptions = "No Such File or Drive Found.";
                }
                else
                {
                    foreach (string s in log)
                    {
                        exceptions = exceptions + System.Environment.NewLine;
                        exceptions = exceptions + s;
                    }
                }

                txtUnAuthorizedDrives.Text = exceptions;

                // Keep the result to a .txt File.
                AppendToFile(objStringBuilder);

                string output = string.Empty;

                if (chkAllDrives.IsChecked != null && chkAllDrives.IsChecked == true)
                {
                    output = "All the drives have been searched successfully !!" + System.Environment.NewLine;
                }
                else
                {
                    output = "The Drive : '" + txtDriveName.Text + "' has been searched successfully !! " + System.Environment.NewLine;
                }

                output = output + "Total " + Convert.ToString(totalFilesScanned) + " files were searched and " + Convert.ToString(totalSearchFound) +
                    " files contain '" + txtSearchText.Text.Trim() + "' string." + System.Environment.NewLine;

                output = output + "Search Results can be found at : '" + txtOutputFileLocation.Text.Trim() + @"MySearchResult.txt'";

                txtOutput.Text = output;

            }
        }

        /// <summary>
        /// If 'Search All The Drives' option is checked then the drive name entered by the user is not considered 
        /// and all the drives that are present on the computer are searched.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAllDrives_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAllDrives.IsChecked == true)
            {
                txtDriveName.IsEnabled = false;
            }
            else
            {
                txtDriveName.IsEnabled = true;
            }
        }

        #endregion Events

        #region Helper Functions

        /// <summary>
        /// This is a function that parses the directory tree struture using recursion.
        /// </summary>
        /// <param name="root">The root of the directory tree, from where the search begins.</param>
        public void TraverseTheDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, get all the files that are commonly used in a .Net project and skip all unwanted files like .mp3 and .jpeg 
            try
            {
                files = root.GetFiles("*.*").Where(name => name.Extension == ".html" || name.Extension == ".aspx" || name.Extension == ".js" || name.Extension == ".css" || name.Extension == ".cs" || name.Extension == ".config").ToArray();
            }


            // This is thrown if even one of the files requires permissions greater 
            // than the application provides. 
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse. 
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                log.Add(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    totalFilesScanned = totalFilesScanned + 1;

                    if (SearchFile(fi.FullName))
                    {
                        objStringBuilder.Append(fi.FullName);
                        objStringBuilder.Append(Environment.NewLine);
                        totalSearchFound = totalSearchFound + 1;
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    TraverseTheDirectoryTree(dirInfo);
                }
            }
        }

        /// <summary>
        /// A function to append the search result to a .txt file
        /// </summary>
        /// <param name="objStringBuilder">List of all files that contain the text to be searched.</param>
        public void AppendToFile(StringBuilder objStringBuilder)
        {
            string path = txtOutputFileLocation.Text.Trim() + @"\MySearchResult.txt";

            if (!File.Exists(path))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(path))
                {

                }
            }

            // This text is always added, making the file longer over time 
            // if it is not deleted. 
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(objStringBuilder.ToString());
            }
        }

        /// <summary>
        /// A function to check if the text entered by the user is present in the file.
        /// </summary>
        /// <param name="fileName">Name of the file in which the user entered search text needs to be searched.</param>
        /// <returns></returns>
        public bool SearchFile(string fileName)
        {
            try
            {
                if (chkIsCaseSensitive.IsChecked != null && chkIsCaseSensitive.IsChecked == true)
                {
                    if (File.Exists(fileName))
                    {
                        string readText = File.ReadAllText(fileName);
                        return readText.Contains(txtSearchText.Text.Trim());
                    }
                }
                else
                {
                    if (File.Exists(fileName))
                    {
                        string readText = File.ReadAllText(fileName).ToLower();
                        return readText.Contains(txtSearchText.Text.Trim().ToLower());
                    }
                }
                

                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion Helper Functions

    }
}
