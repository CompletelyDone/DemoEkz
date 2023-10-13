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
        private int debugCounter = 15;
        DBManager manager;
        private string strCaptcha;
        public Authorization()
        {
            InitializeComponent();
            DataContext = this;
            manager = new DBManager();
            GenerateNewCaptcha();

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

        private void OnImageClicked(object sender, RoutedEventArgs e) => GenerateNewCaptcha();
        private async void Auth(object sender, RoutedEventArgs e)
        {
            if (CaptchaBox.Text == strCaptcha)
            {
                if (await manager.Auth(TextBoxLogin.Text, PasswordBox.Password))
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    TextBoxLogin.Text = String.Empty;
                    PasswordBox.Password = String.Empty;
                    CaptchaBox.Text = String.Empty;
                    GenerateNewCaptcha();
                    MessageBox.Show("Неверный логин/пароль");
                }
            }
            else
            {
                GenerateNewCaptcha();
                MessageBox.Show("Неверная капча");
            }
        }
        private void AuthAsGuest(object sender, RoutedEventArgs e)
        {
            manager.role = 4;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
