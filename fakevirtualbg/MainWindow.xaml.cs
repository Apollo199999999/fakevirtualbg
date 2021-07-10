using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace fakevirtualbg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //init a directory
        string TempDir = "C:\\FakeVirtualBGTemp";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenVideoBtn_Click(object sender, RoutedEventArgs e)
        {
            //init an openfiledialog and get the filepath of the video
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video file (*.mp4)|*.mp4;";
            if (openFileDialog.ShowDialog() == true)
            {
                filepathText.Text = openFileDialog.FileName;
            }
                
        }

        private void VideoSaveLoc_Click(object sender, RoutedEventArgs e)
        {
            //get the directory to save the video by using an openfolderdialog
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                savepathText.Text = dialog.SelectedPath;
            }
        }

        private void GenerateVidBtn_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory("C:\\FakeVirtualBGTemp\\");

            //copy the required files to the temp dir
            File.Copy(filepathText.Text, "C:\\FakeVirtualBGTemp\\original.mp4");
            File.Copy(@"Resources\ffmpeg.exe", "C:\\FakeVirtualBGTemp\\ffmpeg.exe");
            File.Copy(@"Resources\ffplay.exe", "C:\\FakeVirtualBGTemp\\ffplay.exe");
            File.Copy(@"Resources\ffprobe.exe", "C:\\FakeVirtualBGTemp\\ffprobe.exe");
            File.Copy(@"Resources\ffmpegScript.bat", "C:\\FakeVirtualBGTemp\\ffmpegScript.bat");

            //Start the batch script
            Process p = new Process();
            p.StartInfo.FileName = "C:\\FakeVirtualBGTemp\\ffmpegScript.bat";
            p.Start();
            p.WaitForExit();

            //copy the output file
            File.Copy("C:\\FakeVirtualBGTemp\\output.mp4", Path.Combine(savepathText.Text, "output.mp4"));

            //open file explorer and select the output file
            Process.Start("explorer.exe", "/select, " + Path.Combine(savepathText.Text, "output.mp4"));

            //delete the temp directory
            Directory.Delete("C:\\FakeVirtualBGTemp\\", true);

            //exit the application
            Application.Current.Shutdown();

        }
    }
}
