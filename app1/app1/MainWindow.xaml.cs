using System;
using System.Collections.Generic;
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

namespace app1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static System.Diagnostics.Process p = new System.Diagnostics.Process();
        bool process_created = false;
        string com_port = "";

        public MainWindow()
        {
            InitializeComponent();
        }
        System.ComponentModel.BackgroundWorker app_reader;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!process_created)
            {
                //textbox.Text = "start button clicked";
                app_reader = new System.ComponentModel.BackgroundWorker();
                app_reader.WorkerSupportsCancellation = true;
                app_reader.DoWork += (obj, ea) => Taskasync();
                app_reader.RunWorkerAsync();
                Button.Content = "Disconnect";
            }
            else
            {
                Button.Content = "Connect";
                p.CancelOutputRead();
                p.CancelErrorRead();
                p.OutputDataReceived -= p_OutputDataReceived;
                p.ErrorDataReceived -= p_ErrorDataReceived;
                p.Kill();
                p.Close();
                app_reader.CancelAsync();
                process_created = false;
            }
        }
        private async void Taskasync()
        {
            if (!process_created)
            {
                System.Diagnostics.ProcessStartInfo startInfo = p.StartInfo;
                startInfo.FileName = "C:/Users/Mani/Desktop/thesis/cc++/build/app_console.exe";
                //startInfo.FileName = "./app_console.exe";
                this.Dispatcher.Invoke((Action)(() =>
                {//this refer to form in WPF application
                    com_port = com_box.Text;
                }));
                    startInfo.Arguments = com_port;
                System.Diagnostics.ProcessStartInfo info = p.StartInfo;
                info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;

                p.OutputDataReceived += p_OutputDataReceived;
                p.ErrorDataReceived += p_ErrorDataReceived;
                p.Start();
                process_created = true;
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                System.Diagnostics.Debug.WriteLine("process exitted");
            }
        }
        void p_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Data);
            this.Dispatcher.Invoke((Action)(() =>
            {//this refer to form in WPF application
                switch (e.Data)
                {
                        case "1":
                        imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/images/1.jpg"));
                        break;
                        case "2":
                        imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/images/2.png"));
                        break;
                        case "3":
                        imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/images/3.png"));
                        break;
                        case "error":
                        Button.Content = "Connect";
                        p.CancelOutputRead();
                        p.CancelErrorRead();
                        p.OutputDataReceived -= p_OutputDataReceived;
                        p.ErrorDataReceived -= p_ErrorDataReceived;
                        p.Kill();
                        p.Close();
                        app_reader.CancelAsync();
                        process_created = false;
                        MessageBox.Show("Something went worng. Please check");
                        break;
                        default:
                        System.Diagnostics.Debug.WriteLine("unknown characters recieved");
                        break;
                }
            }));
        }

        void p_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
        }

        private void window_main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                p.CancelOutputRead();
                p.CancelErrorRead();
                p.OutputDataReceived -= p_OutputDataReceived;
                p.ErrorDataReceived -= p_ErrorDataReceived;
                p.Kill();
                p.Close();
            }
            catch { }
            System.Diagnostics.Debug.WriteLine("closing all the process");

        }
    }
}
