using Database.MySQLDatabase;
using Modal;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Controller.MySQLController
{
    public class MySQLDepositDAL : IDepositDAL
    {
        public int DepositId { get; set; }
        public int PersonId { get; set; }
        public int BookId { get; set; }
        public string DeliveryDate { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public string LookUpValue { get; set; } = string.Empty;
        public int FindId { get; set; }

        MySQLDatabaseCommand MyDatabaseCommand = new MySQLDatabaseCommand();
        MySQLDatabaseConnection MyConnection = new MySQLDatabaseConnection();

        public void DepositInsert()
        {
            try
            {
                string query = "INSERT INTO kutuphane.emanet (İlkTeslimTarihi, SonTeslimTarihi, KisiId, KitapId, IadeEdildi) VALUES (@DeliveryDate, @DueDate, @PersonId, @BookId, @Refunded)";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@DeliveryDate", MySqlDbType.String).Value = DeliveryDate;
                    cmd.Parameters.Add("@DueDate", MySqlDbType.String).Value = DueDate;
                    cmd.Parameters.Add("@PersonId", MySqlDbType.Int32).Value = PersonId;
                    cmd.Parameters.Add("@BookId", MySqlDbType.Int32).Value = BookId;
                    cmd.Parameters.Add("@Refunded", MySqlDbType.Int32).Value = 0;
                    cmd.ExecuteNonQuery();
                }
                string query1 = "UPDATE kutuphane.kitap SET DurumId = @StateId WHERE Id = @BookId";
                using (MySqlCommand cmd1 = MyDatabaseCommand.MyCommand(query1))
                {
                    cmd1.Parameters.Add("@BookId", MySqlDbType.Int32).Value = BookId;
                    cmd1.Parameters.Add("@StateId", MySqlDbType.Int32).Value = 2;
                    cmd1.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
                MessageBox.Show("Kitap Kişiye Emanet Edildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Emanet Verme İşlemi Sırasında Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PersonSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler ORDER BY Id DESC;";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kişi Bilgileri Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void BookSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.StokAdedi, durum.Adi AS Durum " +
                    "FROM kutuphane.kitap " +
                    "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id WHERE durum.Id = 1 ORDER BY Id DESC;";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitap Bilgileri Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void InstantReaderSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT emanet.Id, emanet.SonTeslimTarihi AS IadeTarihi, emanet.IadeEdildi, kitap.Adi AS KitapAdi, kitap.ISBN, kisiler.KisiselNo AS KurumNo, kisiler.Adi, kisiler.Soyadi " +
                    "FROM kutuphane.emanet " +
                    "INNER JOIN kutuphane.kisiler ON emanet.KisiId = kisiler.Id " +
                    "INNER JOIN kutuphane.kitap ON emanet.KitapId = kitap.Id ORDER BY emanet.Id DESC;";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilgiler Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void AcceptReturn()
        {
            try
            {
                string query = "SELECT KitapId FROM kutuphane.emanet WHERE Id = @DepositId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@DepositId", MySqlDbType.Int32).Value = DepositId;
                    BookId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                string query1 = "UPDATE kutuphane.kitap SET DurumId = @StateId WHERE Id = @BookId";
                using (MySqlCommand cmd1 = MyDatabaseCommand.MyCommand(query1))
                {
                    cmd1.Parameters.Add("@BookId", MySqlDbType.Int32).Value = BookId;
                    cmd1.Parameters.Add("@StateId", MySqlDbType.Int32).Value = 1;
                    cmd1.ExecuteNonQuery();
                }
                string query2 = "UPDATE kutuphane.emanet SET IadeEdildi = @Refunded WHERE Id = @DepositId";
                using (MySqlCommand cmd2 = MyDatabaseCommand.MyCommand(query2))
                {
                    cmd2.Parameters.Add("@DepositId", MySqlDbType.Int32).Value = DepositId;
                    cmd2.Parameters.Add("@Refunded", MySqlDbType.Int32).Value = 1;
                    cmd2.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Geri Alma İşlemi Sırasında Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void DueAcceptReturn()
        {
            try
            {
                string query = "UPDATE kutuphane.kitap SET DurumId = @StateId WHERE Id = @BookId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@BookId", MySqlDbType.Int32).Value = BookId;
                    cmd.Parameters.Add("@StateId", MySqlDbType.Int32).Value = 1;
                    cmd.ExecuteNonQuery();
                }
                string query1 = "UPDATE kutuphane.emanet SET IadeEdildi = @Refunded WHERE Id = @DepositId";
                using (MySqlCommand cmd1 = MyDatabaseCommand.MyCommand(query1))
                {
                    cmd1.Parameters.Add("@DepositId", MySqlDbType.Int32).Value = DepositId;
                    cmd1.Parameters.Add("@Refunded", MySqlDbType.Int32).Value = 1;
                    cmd1.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
                MessageBox.Show("Geri Alma İşlemi Başarılı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Geri Alma İşlemi Sırasında Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void DueSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT emanet.SonTeslimTarihi AS IadeTarihi, emanet.Id, kisiler.Adi, kisiler.Soyadi, Kisiler.KisiselNo AS KurumNo, kisiler.TelNo AS Telefon_No, kitap.Id AS KitapId, kitap.Adi AS KitapAdi, kitap.ISBN " +
                    "FROM kutuphane.emanet " +
                    "INNER JOIN kutuphane.kisiler ON emanet.kisiId = kisiler.Id " +
                    "INNER JOIN kutuphane.kitap ON emanet.kitapId = kitap.Id WHERE SonTeslimTarihi < '" + DateTime.Now.ToShortDateString() + "' AND IadeEdildi = 0";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilgiler Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public DateTime CountDays(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            if (dt.DayOfWeek == DayOfWeek.Saturday)
                dt = dt.AddDays(2);
            else if (dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);
            return dt.AddDays(weeks * 7);
        }

        public void DepositFind(DataGrid dataGrid)
        {
            try
            {
                string query = "";
                switch (FindId)
                {
                    case 1:
                        query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler " +
                            "WHERE kisiler.KisiselNo LIKE '" + LookUpValue + "%' ORDER BY Id DESC;";
                        break;
                    case 2:
                        query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler " +
                            "WHERE kisiler.Adi LIKE '" + LookUpValue + "%' ORDER BY Id DESC;";
                        break;
                    case 3:
                        query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.StokAdedi, durum.Adi AS Durum " +
                    "FROM kutuphane.kitap " +
                    "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id WHERE kitap.Adi LIKE '" + LookUpValue + "%' AND durum.Id = 1 ORDER BY Id DESC;";
                        break;
                    case 4:
                        query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.StokAdedi, durum.Adi AS Durum " +
                    "FROM kutuphane.kitap " +
                    "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id WHERE kitap.ISBN LIKE '" + LookUpValue + "%' AND durum.Id = 1 ORDER BY Id DESC;";
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

        public void DueFindAndFill(DataGrid dataGrid)
        {
            try
            {
                string query = "";
                switch (FindId)
                {
                    case 1:
                        query = "SELECT emanet.SonTeslimTarihi AS IadeTarihi, emanet.Id, kisiler.Adi, kisiler.Soyadi, Kisiler.KisiselNo AS KurumNo, kisiler.TelNo AS Telefon_No, kitap.Id AS KitapId, kitap.Adi AS KitapAdi, kitap.ISBN " +
                    "FROM kutuphane.emanet " +
                    "INNER JOIN kutuphane.kisiler ON emanet.kisiId = kisiler.Id " +
                    "INNER JOIN kutuphane.kitap ON emanet.kitapId = kitap.Id WHERE kisiler.KisiselNo LIKE '" + LookUpValue + "%' AND SonTeslimTarihi < '" + DateTime.Now.ToShortDateString() + "' AND IadeEdildi = 0";
                        break;
                    case 2:
                        query = "SELECT emanet.SonTeslimTarihi AS IadeTarihi, emanet.Id, kisiler.Adi, kisiler.Soyadi, Kisiler.KisiselNo AS KurumNo, kisiler.TelNo AS Telefon_No, kitap.Id AS KitapId, kitap.Adi AS KitapAdi, kitap.ISBN " +
                    "FROM kutuphane.emanet " +
                    "INNER JOIN kutuphane.kisiler ON emanet.kisiId = kisiler.Id " +
                    "INNER JOIN kutuphane.kitap ON emanet.kitapId = kitap.Id WHERE kisiler.Adi LIKE '" + LookUpValue + "%' AND SonTeslimTarihi < '" + DateTime.Now.ToShortDateString() + "' AND IadeEdildi = 0";
                        break;
                    case 3:
                        query = "SELECT emanet.SonTeslimTarihi AS IadeTarihi, emanet.Id, kisiler.Adi, kisiler.Soyadi, Kisiler.KisiselNo AS KurumNo, kisiler.TelNo AS Telefon_No, kitap.Id AS KitapId, kitap.Adi AS KitapAdi, kitap.ISBN " +
                    "FROM kutuphane.emanet " +
                    "INNER JOIN kutuphane.kisiler ON emanet.kisiId = kisiler.Id " +
                    "INNER JOIN kutuphane.kitap ON emanet.kitapId = kitap.Id WHERE kitap.Adi LIKE '" + LookUpValue + "%' AND SonTeslimTarihi < '" + DateTime.Now.ToShortDateString() + "' AND IadeEdildi = 0";
                        break;
                    default:
                        query = "SELECT emanet.SonTeslimTarihi AS IadeTarihi, emanet.Id, kisiler.Adi, kisiler.Soyadi, Kisiler.KisiselNo AS KurumNo, kisiler.TelNo AS Telefon_No, kitap.Id AS KitapId, kitap.Adi AS KitapAdi, kitap.ISBN " +
                    "FROM kutuphane.emanet " +
                    "INNER JOIN kutuphane.kisiler ON emanet.kisiId = kisiler.Id " +
                    "INNER JOIN kutuphane.kitap ON emanet.kitapId = kitap.Id WHERE SonTeslimTarihi <  '" + DateTime.Now.ToShortDateString() + "' AND IadeEdildi = 0";
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

        public void InstantFindAndFill(DataGrid dataGrid)
        {
            try
            {
                string query = "";
                switch (FindId)
                {
                    case 1:
                        query = "SELECT emanet.Id, emanet.SonTeslimTarihi AS IadeTarihi, emanet.IadeEdildi, kitap.Adi AS KitapAdi, kitap.ISBN, kisiler.KisiselNo AS KurumNo, kisiler.Adi, kisiler.Soyadi " +
                        "FROM kutuphane.emanet " +
                        "INNER JOIN kutuphane.kisiler ON emanet.KisiId = kisiler.Id " +
                        "INNER JOIN kutuphane.kitap ON emanet.KitapId = kitap.Id WHERE IadeEdildi = 0 AND kisiler.KisiselNo LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    case 2:
                        query = "SELECT emanet.Id, emanet.SonTeslimTarihi AS IadeTarihi, emanet.IadeEdildi, kitap.Adi AS KitapAdi, kitap.ISBN, kisiler.KisiselNo AS KurumNo, kisiler.Adi, kisiler.Soyadi " +
                        "FROM kutuphane.emanet " +
                        "INNER JOIN kutuphane.kisiler ON emanet.KisiId = kisiler.Id " +
                        "INNER JOIN kutuphane.kitap ON emanet.KitapId = kitap.Id WHERE IadeEdildi = 0 AND kisiler.Adi LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    case 3:
                        query = "SELECT emanet.Id, emanet.SonTeslimTarihi AS IadeTarihi, emanet.IadeEdildi, kitap.Adi AS KitapAdi, kitap.ISBN, kisiler.KisiselNo AS KurumNo, kisiler.Adi, kisiler.Soyadi " +
                        "FROM kutuphane.emanet " +
                        "INNER JOIN kutuphane.kisiler ON emanet.KisiId = kisiler.Id " +
                        "INNER JOIN kutuphane.kitap ON emanet.KitapId = kitap.Id WHERE IadeEdildi = 0 AND ISBN LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    case 4:
                        query = "SELECT emanet.Id, emanet.SonTeslimTarihi AS IadeTarihi, emanet.IadeEdildi, kitap.Adi AS KitapAdi, kitap.ISBN, kisiler.KisiselNo AS KurumNo, kisiler.Adi, kisiler.Soyadi " +
                        "FROM kutuphane.emanet " +
                        "INNER JOIN kutuphane.kisiler ON emanet.KisiId = kisiler.Id " +
                        "INNER JOIN kutuphane.kitap ON emanet.KitapId = kitap.Id WHERE IadeEdildi = 0 AND kitap.Adi LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    default:
                        InstantReaderSelect(dataGrid);
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
    }
}
