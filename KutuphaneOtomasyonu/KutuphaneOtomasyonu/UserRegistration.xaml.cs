using Controller;
using Controller.MySQLController;
using System.Windows;

namespace UI
{
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void Kapat_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Kaydol_Cilick(object sender, RoutedEventArgs e)
        {
            MySQLUserDAL user = new MySQLUserDAL();
            Encryption sifreleme = new Encryption();
            if (UserName_txt.Text != "" && UserSurname_txt.Text != "" && UserMail_txt.Text != "" && PhoneNumber_txt.Text != "" && Password_txt.Text != "" && Password2_txt.Text != "")
            {
                user.UserName = UserName_txt.Text.Trim();
                user.UserSurname = UserSurname_txt.Text.Trim();
                user.UserMail = UserMail_txt.Text.Trim();
                user.PhoneNumber = PhoneNumber_txt.Text.Trim();
                if (Password_txt.Text == Password2_txt.Text)
                {
                    user.Password = Encryption.ComputeSha256Hash(Password_txt.Text.Trim());
                    user.Insert();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("2 Alanada Girilen Şifre Bilgisinin Aynı Olduğundan Emin Olunuz.");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Kaydı İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }
    }
}
