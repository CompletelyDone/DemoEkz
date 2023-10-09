using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        DispatcherTimer timer;
        private int debugCounter = 15;
        DBManager manager;
        private string strCaptcha;
        public Authorization()
        {
            InitializeComponent();
            DataContext = this;
            manager = new DBManager();
            GenerateNewCaptcha();
            timer = new DispatcherTimer();

        }

        private void GenerateNewCaptcha()
        {
            strCaptcha = Captcha.GenerateCaptchaText(6);
            CaptchaImage.Source = Captcha.DrawCaptchaImage(strCaptcha);
            debugCounter++;
            if (debugCounter > 20)
            {
                var newWindowAdmin = new MainWindow();
                newWindowAdmin.Show();
                this.Close();
            }
        }

        private async void OnImageClicked(object sender, RoutedEventArgs e) => GenerateNewCaptcha();
        private async void Auth(object sender, RoutedEventArgs e)
        {

        }
    }
}
