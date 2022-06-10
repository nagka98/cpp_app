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
        bool calib_timer = false;
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
                calibration_label.Content = "Not Available";
                dynamic_label.Content = "No Data";
                imagebox.Source = null;
                imagebox_dynamic.Source = null;
                timer.Content = "Not Available";
                prediction_label.Content = "N Data";
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
                //startInfo.FileName = "C:/Users/Mani/Desktop/thesis/cc++/build/app_console.exe";
                //startInfo.FileName = "C:/Users/Mani/Desktop/thesis/CLapp_SF/main.exe";
                startInfo.FileName = "./main.exe";
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
                        case "startup":
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-flex.jpg"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-flex.jpg"));
                        calibration_label.Content = "please Flex your Fingers";
                        calib_timer = true;
                        break;
                        case "calib_min":
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-palm.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-palm.png"));
                        calibration_label.Content = "Please keep your hand in above position";
                        break;
                        case "calib_max":
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-fist.jpg"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-fist.jpg"));
                        calibration_label.Content = "Please keep your hand in above position";
                        break;
                        case "1000":
                        prediction_label.Content = "1";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-1.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-1.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "1100":
                        prediction_label.Content = "2";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-2.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-2.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "1110":
                        prediction_label.Content = "3";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-3.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-3.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "1111":
                        prediction_label.Content = "4";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-3.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-4.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "11111":
                        prediction_label.Content = "5";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-5.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-5.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "1101":
                        prediction_label.Content = "7";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-5.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-7.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "1011":
                        prediction_label.Content = "8";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-8.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-8.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "0111":
                        prediction_label.Content = "9";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-9.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-9.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "0000":
                        prediction_label.Content = "10";
                        //imagebox.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/ASL-10.png"));
                        imagebox.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/ASL-10.png"));
                        calibration_label.Content = "Completed";
                        break;
                        case "updown":
                        dynamic_label.Content = "updown";
                        //imagebox_dynamic.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/updown.png"));
                        imagebox_dynamic.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/updown.png"));
                        break;
                        case "wave":
                        dynamic_label.Content = "wave";
                        //imagebox_dynamic.Source = new BitmapImage(new Uri("C:/Users/Mani/Desktop/thesis/ASL-Numbers/wave.jpg"));
                        imagebox_dynamic.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/images/wave.jpg"));
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
                        case "calib_done":
                        calibration_label.Content = "completed";
                        timer.Content = "COMPLETED";
                        imagebox.Source = null;
                        calib_timer = false;
                        break;
                        default:
                        if(calib_timer)
                        {
                            timer.Content = e.Data.ToString();
                        }
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
