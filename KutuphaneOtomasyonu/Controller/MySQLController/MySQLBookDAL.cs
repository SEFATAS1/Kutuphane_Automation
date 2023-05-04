using Database.MySQLDatabase;
using Modal;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Controller.MySQLController
{
    public class MySQLBookDAL : IBookDAL
    {
        public int BookId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string RecordDate { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;
        public int Count { get; set; }
        public int WriterId { get; set; }
        public string WriterName { get; set; } = string.Empty;
        public string WriterSurname { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public int BookStateId { get; set; }
        public int CategoryId { get; set; }
        public int FindId { get; set; }
        public string LookUpValue { get; set; } = string.Empty;

        MySQLDatabaseCommand MyDatabaseCommand = new MySQLDatabaseCommand();
        MySQLDatabaseConnection MyConnection = new MySQLDatabaseConnection();

        public void BookInsert()
        {
            try
            {
                if (BookIsThere())
                {
                    MessageBox.Show("Bigilerdeki ISBN Numarası İle Kayıt Bulunduğundan Dolayı Kayıt Yapılamamaktadır.");
                }
                else
                {
                    WriterInsert();
                    PublisherInsert();
                    string query = "INSERT INTO kutuphane.kitap (Adi,ISBN,KayitTarihi,StokAdedi,YazarId,YayineviId,DurumId,KategoriId) VALUES (@BookName,@ISBN,@RecordDate,@Count,@WriterId,@PublisherId,@BookStateId,@CategoryId)";
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@BookName", MySqlDbType.String).Value = BookName;
                        cmd.Parameters.Add("@ISBN", MySqlDbType.String).Value = ISBN;
                        cmd.Parameters.Add("@RecordDate", MySqlDbType.String).Value = RecordDate;
                        cmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = Count;
                        cmd.Parameters.Add("@WriterId", MySqlDbType.Int32).Value = WriterId;
                        cmd.Parameters.Add("@PublisherId", MySqlDbType.Int32).Value = PublisherId;
                        cmd.Parameters.Add("@BookStateId", MySqlDbType.Int32).Value = BookStateId;
                        cmd.Parameters.Add("@CategoryId", MySqlDbType.Int32).Value = CategoryId;
                        cmd.ExecuteNonQuery();
                    }
                    MyConnection.ConnectionClose();
                    MessageBox.Show("Kayıt Başarılı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitap Kayıt Edilirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void WriterInsert()
        {
            try
            {
                if (WriterIsThere())
                {
                    string query = "SELECT Id FROM kutuphane.yazar WHERE Adi = @WriterName AND Soyadi = @WriterSurname";
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@WriterName", MySqlDbType.String).Value = WriterName;
                        cmd.Parameters.Add("@WriterSurname", MySqlDbType.String).Value = WriterSurname;
                        WriterId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                else
                {
                    string query = "INSERT INTO kutuphane.yazar (Adi,Soyadi) VALUES (@WriterName,@WriterSurname)";
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@WriterName", MySqlDbType.String).Value = WriterName;
                        cmd.Parameters.Add("@WriterSurname", MySqlDbType.String).Value = WriterSurname;
                        cmd.ExecuteNonQuery();
                    }
                    WriterId = LastInsertId();
                }
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yazar Kayıt Edilirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PublisherInsert()
        {
            try
            {
                if (PublisherIsThere())
                {
                    string query = "SELECT Id FROM kutuphane.yayinevi WHERE Adi = @PublisherName";
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@PublisherName", MySqlDbType.String).Value = PublisherName;
                        PublisherId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                else
                {
                    string query = "INSERT INTO kutuphane.yayinevi (Adi) VALUES (@PublisherName)";
                    using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                    {
                        cmd.Parameters.Add("@PublisherName", MySqlDbType.String).Value = PublisherName;
                        cmd.ExecuteNonQuery();
                    }
                    PublisherId = LastInsertId();
                }
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yayinevi Kayıt Edilirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void BookUpdate()
        {
            try
            {
                WriterUpdate();
                PublisherUpdate();
                string query = "UPDATE kutuphane.kitap SET Adi = @BookName, ISBN = @ISBN, StokAdedi = @Count, " +
                       " YazarId = @WriterId, YayineviId = @PublisherId, DurumId = @BookStateId, KategoriId = @CategoryId WHERE Id = @BookId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@BookId", MySqlDbType.Int32).Value = BookId;
                    cmd.Parameters.Add("@BookName", MySqlDbType.String).Value = BookName;
                    cmd.Parameters.Add("@ISBN", MySqlDbType.String).Value = ISBN;
                    cmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = Count;
                    cmd.Parameters.Add("@WriterId", MySqlDbType.Int32).Value = WriterId;
                    cmd.Parameters.Add("@PublisherId", MySqlDbType.Int32).Value = PublisherId;
                    cmd.Parameters.Add("@BookStateId", MySqlDbType.Int32).Value = BookStateId;
                    cmd.Parameters.Add("@CategoryId", MySqlDbType.Int32).Value = CategoryId;
                    cmd.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
                MessageBox.Show("Kitap Bilgileri Güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitap Bilgileri Güncellenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void WriterUpdate()
        {
            try
            {
                string query = "SELECT YazarId FROM kutuphane.kitap WHERE Id = @BookId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@BookId", MySqlDbType.Int32).Value = BookId;
                    WriterId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                string query1 = "UPDATE kutuphane.yazar SET Adi = @WriterName, Soyadi = @WriterSurname WHERE Id = @WriterId";
                using (MySqlCommand cmd1 = MyDatabaseCommand.MyCommand(query1))
                {
                    cmd1.Parameters.Add("@WriterId", MySqlDbType.Int32).Value = WriterId;
                    cmd1.Parameters.Add("@WriterName", MySqlDbType.String).Value = WriterName;
                    cmd1.Parameters.Add("@WriterSurname", MySqlDbType.String).Value = WriterSurname;
                    cmd1.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yazar Bilgileri Güncellenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void PublisherUpdate()
        {
            try
            {
                string query = "SELECT YayineviId FROM kutuphane.kitap WHERE Id = @PublisherId";
                using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
                {
                    cmd.Parameters.Add("@PublisherId", MySqlDbType.Int32).Value = BookId;
                    PublisherId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                string query1 = "UPDATE kutuphane.yayinevi SET Adi = @PublisherName WHERE Id = @PublisherId";
                using (MySqlCommand cmd1 = MyDatabaseCommand.MyCommand(query1))
                {
                    cmd1.Parameters.Add("@PublisherId", MySqlDbType.Int32).Value = PublisherId;
                    cmd1.Parameters.Add("@PublisherName", MySqlDbType.String).Value = PublisherName;
                    cmd1.ExecuteNonQuery();
                }
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yayinevi Bilgileri Güncellenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public void BookSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.KayitTarihi, kitap.StokAdedi, yazar.Adi AS YazarAdi, yazar.Soyadi AS YazarSoyadi, yayinevi.Adi AS YayineviAdi, kategori.Adi AS Kategori, durum.Adi AS Durum " +
                        "FROM kutuphane.kitap " +
                        "INNER JOIN kutuphane.yazar ON kitap.YazarId = yazar.Id " +
                        "INNER JOIN kutuphane.yayinevi ON kitap.YayineviId = yayinevi.Id " +
                        "INNER JOIN kutuphane.kategori ON kitap.KategoriId = kategori.Id " +
                        "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id ORDER BY Id DESC;";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilgiler Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public int LastInsertId()
        {
            string query = "SELECT LAST_INSERT_ID()";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                int LastId = Convert.ToInt32(cmd.ExecuteScalar());
                MyConnection.ConnectionClose();
                return LastId;
            }
        }

        public bool BookIsThere()
        {
            string query = "SELECT * FROM kutuphane.kitap Where ISBN = @ISBN";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@ISBN", MySqlDbType.String).Value = ISBN;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }

        public bool WriterIsThere()
        {
            string query = "SELECT * FROM kutuphane.Yazar Where Adi = @WriterName AND Soyadi = @WriterSurname";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@WriterName", MySqlDbType.String).Value = WriterName;
                cmd.Parameters.Add("@WriterSurname", MySqlDbType.String).Value = WriterSurname;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }

        public bool PublisherIsThere()
        {
            string query = "SELECT * FROM kutuphane.yayinevi Where Adi = @PublisherName";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@PublisherName", MySqlDbType.String).Value = PublisherName;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }

        public void BookFindAndFill(DataGrid dataGrid)
        {
            try
            {
                string query = "";
                switch (FindId)
                {
                    case 1:
                        query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.KayitTarihi, kitap.StokAdedi, yazar.Adi AS YazarAdi, yazar.Soyadi AS YazarSoyadi, yayinevi.Adi AS YayineviAdi, kategori.Adi AS Kategori, durum.Adi AS Durum " +
                        "FROM kutuphane.kitap " +
                        "INNER JOIN kutuphane.yazar ON kitap.YazarId = yazar.Id " +
                        "INNER JOIN kutuphane.yayinevi ON kitap.YayineviId = yayinevi.Id " +
                        "INNER JOIN kutuphane.kategori ON kitap.KategoriId = kategori.Id " +
                        "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id WHERE kitap.Adi LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    case 2:
                        query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.KayitTarihi, kitap.StokAdedi, yazar.Adi AS YazarAdi, yazar.Soyadi AS YazarSoyadi, yayinevi.Adi AS YayineviAdi, kategori.Adi AS Kategori, durum.Adi AS Durum " +
                        "FROM kutuphane.kitap " +
                        "INNER JOIN kutuphane.yazar ON kitap.YazarId = yazar.Id " +
                        "INNER JOIN kutuphane.yayinevi ON kitap.YayineviId = yayinevi.Id " +
                        "INNER JOIN kutuphane.kategori ON kitap.KategoriId = kategori.Id " +
                        "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id WHERE yazar.Adi LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    case 3:
                        query = "SELECT kitap.Id, kitap.Adi, kitap.ISBN, kitap.KayitTarihi, kitap.StokAdedi, yazar.Adi AS YazarAdi, yazar.Soyadi AS YazarSoyadi, yayinevi.Adi AS YayineviAdi, kategori.Adi AS Kategori, durum.Adi AS Durum " +
                        "FROM kutuphane.kitap " +
                        "INNER JOIN kutuphane.yazar ON kitap.YazarId = yazar.Id " +
                        "INNER JOIN kutuphane.yayinevi ON kitap.YayineviId = yayinevi.Id " +
                        "INNER JOIN kutuphane.kategori ON kitap.KategoriId = kategori.Id " +
                        "INNER JOIN kutuphane.durum ON kitap.DurumId = durum.Id WHERE ISBN LIKE '" + LookUpValue + "%' ORDER BY Id DESC";
                        break;
                    default:
                        BookSelect(dataGrid);
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
