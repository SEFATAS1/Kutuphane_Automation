using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace UI
{
    /// <summary>
    /// SplashScreen.xaml etkileşim mantığı
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(80);
            }
        }

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            if (progressBar.Value == 100)
            {
                LoginScreen login = new LoginScreen();
                Close();
                login.ShowDialog();
            }
        }

        private void Window_ContentRendered_1(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }
    }
}
