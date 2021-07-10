using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace fakevirtualbg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenVideoBtn_Click(object sender, RoutedEventArgs e)
        {
            //init an openfiledialog and get the filepath of the video
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video files (*.mp4;*.mov)|*.mp4;*.mov";
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
            //first, reverse the video
            Process p = new Process();
            p.StartInfo.FileName = @"Resources\ffmpeg.exe";
            p.StartInfo.Arguments = "-i " + "\"" + filepathText.Text + "\"" + " -vf reverse " + "\"" + System.IO.Path.Combine
                (savepathText.Text, "output" + System.IO.Path.GetExtension(filepathText.Text)) + "\"";

            MessageBox.Show(p.StartInfo.Arguments.ToString());
            p.Start();
        }
    }
}
