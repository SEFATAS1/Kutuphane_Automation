﻿using Database.MySQLDatabase;
using Modal;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Controller.MySQLController
{
    public class MySQLAcademicianDAL : IPeopleDAL
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public string PersonSurname { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int FindId { get; set; }
        public string LookUpValue { get; set; } = string.Empty;

        MySQLDatabaseCommand MyDatabaseCommand = new MySQLDatabaseCommand();
        MySQLDatabaseConnection MyConnection = new MySQLDatabaseConnection();

        public void PersonInsert()
        {
            try
            {
                string query = "INSERT INTO kutuphane.kisiler (Adi,Soyadi,KisiselNo,RolId,TelNo) VALUES (@PersonName,@PersonSurname,@PersonalNumber,@RoleId,@PhoneNumber)";
                if (PersonIsThere())
                {
                    MessageBox.Show("Bilgilerdeki Kurum Numarası Ve/Veya Telefon Numarası İle Kayıt Bulunduğundan Kayıt Yapılamamaktadır.");
                }
                else
                {
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@PersonName", MySqlDbType.String).Value = PersonName;
                        cmd.Parameters.Add("@PersonSurname", MySqlDbType.String).Value = PersonSurname;
                        cmd.Parameters.Add("@PersonalNumber", MySqlDbType.String).Value = PersonalNumber;
                        cmd.Parameters.Add("@RoleId", MySqlDbType.Int32).Value = RoleId;
                        cmd.Parameters.Add("@PhoneNumber", MySqlDbType.String).Value = PhoneNumber;
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Kayıtlı Başarılı.");
                }
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Akademisyen Kayıt Edilirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PersonUpdate()
        {
            try
            {
                string query = "UPDATE kutuphane.kisiler SET Adi = @PersonName, Soyadi = @PersonSurname, KisiselNo = @PersonalNumber, RolId = @RoleId, TelNo = @PhoneNumber WHERE Id = @PersonId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@PersonId", MySqlDbType.Int32).Value = PersonId;
                    cmd.Parameters.Add("@PersonName", MySqlDbType.String).Value = PersonName;
                    cmd.Parameters.Add("@PersonSurname", MySqlDbType.String).Value = PersonSurname;
                    cmd.Parameters.Add("@PersonalNumber", MySqlDbType.String).Value = PersonalNumber;
                    cmd.Parameters.Add("@RoleId", MySqlDbType.Int32).Value = RoleId;
                    cmd.Parameters.Add("@PhoneNumber", MySqlDbType.String).Value = PhoneNumber;
                    cmd.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
                MessageBox.Show("Akademisyenin Bilgileri Güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Akademisyenin Bilgileri Güncellenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PersonSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler where kisiler.RolId = 2 ORDER BY Id DESC";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıtlı Akademisyenler Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PersonDelete()
        {
            try
            {
                string query = "DELETE FROM kutuphane.kisiler WHERE Id = @PersonId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@PersonId", MySqlDbType.Int32).Value = PersonId;
                    cmd.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
                MessageBox.Show("Akademisyen Kayıt Silme İşlemi Başarılı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıtlı Akademisyen Bilgileri Silinirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PersonFindAndFill(DataGrid dataGrid)
        {
            try
            {
                string query = "";
                switch (FindId)
                {
                    case 1:
                        query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler where KisiselNo LIKE '" + LookUpValue + "%' AND kisiler.RolId = 2 ORDER BY Id DESC";
                        break;
                    case 2:
                        query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler where Adi LIKE '" + LookUpValue + "%' AND kisiler.RolId = 2 ORDER BY Id DESC";
                        break;
                    case 3:
                        query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler where Soyadi LIKE '%" + LookUpValue + "%' AND kisiler.RolId = 2 ORDER BY Id DESC";
                        break;
                    default:
                        query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler where kisiler.RolId = 2 ORDER BY Id DESC";
                        break;
                }

                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama Sonuçları Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public bool PersonIsThere()
        {
            string query = "SELECT * FROM kutuphane.kisiler Where KisiselNo = @PersonalNumber OR TelNo = @PhoneNumber AND RolId = @RoleId";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@PersonalNumber", MySqlDbType.String).Value = PersonalNumber;
                cmd.Parameters.Add("@PhoneNumber", MySqlDbType.String).Value = PhoneNumber;
                cmd.Parameters.Add("@RoleId", MySqlDbType.Int32).Value = RoleId;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }
    }
}
