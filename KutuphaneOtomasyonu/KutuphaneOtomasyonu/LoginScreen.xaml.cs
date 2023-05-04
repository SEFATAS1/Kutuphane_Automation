using Controller;
using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Input;

namespace UI
{
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void KapatButonu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SimgeDurumu_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void GirisYap_btn_Click(object sender, RoutedEventArgs e)
        {
            MySQLUserDAL user = new MySQLUserDAL();
            Encryption sifreleme = new Encryption();
            try
            {
                user.UserMail = UserMail_txt.Text.Trim();
                user.Password = Encryption.ComputeSha256Hash(Password_txt.Text.Trim());
                if (UserMail_txt.Text != "" && Password_txt.Text != "")
                {
                    if (user.Login())
                    {
                        HomePage anasayfa = new HomePage();
                        anasayfa.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Mail Adresi Veya Şifresi Yanlış.");
                    }
                }
                else
                {
                    MessageBox.Show("Giriş İşlemi Sırasında Kullanıcı Mail ve Kullanıcı Şifre Alanları Boş Bırakılamaz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş Yapılırken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        private void GirisYap_Kaydol_Click(object sender, RoutedEventArgs e)
        {
            UserRegistration kullanıcıKaydol = new UserRegistration();
            kullanıcıKaydol.Show();
        }
    }
}
