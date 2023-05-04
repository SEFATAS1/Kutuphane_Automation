using Database.MySQLDatabase;
using Modal;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Controller.MySQLController
{
    public class MySQLUserDAL : IUserDAL
    {
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserMail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        MySQLDatabaseCommand MyDatabaseCommand = new MySQLDatabaseCommand();
        MySQLDatabaseConnection MyConnection = new MySQLDatabaseConnection();

        public void Insert()
        {
            try
            {
                if (IsThere())
                {
                    MessageBox.Show("Bilgilerdeki Mail Ve Telefon Numarası İle Kayıt Bulunduğundan Kayıt Yapılamamaktadır.");
                }
                else
                {
                    string query = "INSERT INTO kutuphane.kullanicilar (Adi,Soyadi,Mail,Sifre,TelNo) VALUES (@UserName,@UserSurname,@UserMail,@Password,@PhoneNumber)";
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@UserName", MySqlDbType.String).Value = UserName;
                        cmd.Parameters.Add("@UserSurname", MySqlDbType.String).Value = UserSurname;
                        cmd.Parameters.Add("@UserMail", MySqlDbType.String).Value = UserMail;
                        cmd.Parameters.Add("@Password", MySqlDbType.String).Value = Password;
                        cmd.Parameters.Add("@PhoneNumber", MySqlDbType.String).Value = PhoneNumber;
                        cmd.ExecuteNonQuery();
                    }
                    MyConnection.ConnectionClose();
                    MessageBox.Show("Kayıt Başarılı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı Kayıt Edilirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public bool Login()
        {
            string query = "SELECT * FROM kutuphane.kullanicilar Where Mail = @UserMail AND Sifre = @Password";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@UserMail", MySqlDbType.String).Value = UserMail;
                cmd.Parameters.Add("@Password", MySqlDbType.String).Value = Password;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }

        public bool IsThere()
        {
            string query = "SELECT * FROM kutuphane.kullanicilar Where Mail = @UserMail OR TelNo = @PhoneNumber";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@UserMail", MySqlDbType.String).Value = UserMail;
                cmd.Parameters.Add("@PhoneNumber", MySqlDbType.String).Value = PhoneNumber;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }
    }
}
