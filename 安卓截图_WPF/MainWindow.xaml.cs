using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Cache;
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

namespace 安卓截图_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string Desktop;
        bool readfile = true;
        bool isshot = false;
        public MainWindow()
        {
            InitializeComponent();
            Desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            Debug.WriteLine(Desktop);

        }

        private async void screenshotImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            do
            {


                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;//true表示不显示黑框，false表示显示dos界面
                try
                {
                    p.Start();
                    p.StandardInput.WriteLine("del " + Desktop + "\\screenshot.png");
                    p.StandardInput.WriteLine("adb shell /system/bin/screencap -p /storage/sdcard0/screenshot.png");
                    p.StandardInput.WriteLine("adb pull /storage/sdcard0/screenshot.png " + Desktop);
                    p.StandardInput.WriteLine("adb shell rm /storage/sdcard0/screenshot.png");

                    p.Close();


                    await Task.Delay(500);
                    readfile = true;
                    int i = 0;
                    while (readfile)
                    {
                        await Task.Delay(100);
                        try
                        {
                            if (i > 100)
                            {
                                readfile = false;
                            }
                            else
                            {
                                i++;
                            }
                            BinaryReader binReader = new BinaryReader(File.Open(Desktop + "\\screenshot.png", FileMode.Open));
                            FileInfo fileInfo = new FileInfo(Desktop + "\\screenshot.png");
                            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                            binReader.Close();

                            // Init bitmap
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = new MemoryStream(bytes);
                            bitmap.EndInit();
                            screenshotImage.Source = bitmap;

                            readfile = false;
                            Debug.WriteLine("OK");

                        }
                        catch (Exception)
                        {
                            Debug.WriteLine("Exception");
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            while (isshot == true);
        }



        private void shotCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (shotCheckBox.IsChecked == true)
                isshot = true;
            else if (shotCheckBox.IsChecked == false)
                isshot = false;
        }


    }

}
