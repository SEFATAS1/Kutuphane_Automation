using Database.MySQLDatabase;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Modal;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Controller.MySQLController
{
    public class MySQLGraduationInquiryDAL : IProofingDocumentDAL
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public string PersonSurname { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public string LookUpValue { get; set; } = string.Empty;

        MySQLDatabaseCommand MyDatabaseCommand = new MySQLDatabaseCommand();
        MySQLDatabaseConnection MyConnection = new MySQLDatabaseConnection();

        public void Find(DataGrid dataGrid)
        {
            try
            {
                if (PersonIsThere())
                {
                    string query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler WHERE KisiselNo = '" + PersonalNumber + "' AND RolId = 1 ORDER BY Id DESC";
                    MyDatabaseCommand.MySelectCommand(query, dataGrid);
                    MyConnection.ConnectionClose();
                }
                else
                {
                    MessageBox.Show("Sorgulamak İstediğiniz Öğrenci Numarası İle Kayıtlı Bir Öğrenci Bulunmamaktadır.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama Sonuçları Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }

        public bool Inquiry()
        {
            string query = "SELECT * FROM kutuphane.emanet Where KisiId = @PersonId AND IadeEdildi = 0";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@PersonId", MySqlDbType.String).Value = PersonId;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }

        public void PDFCreate()
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter.GetInstance(document, new FileStream(@"C:\Users\Bilg\Desktop\Mezuniyet Belgesi\deneme.pdf", FileMode.Create));
            BaseFont arial = BaseFont.CreateFont("@\"C:\\Users\\Bilg\\Desktop\\Mezuniyet Belgesi\\ArialNova.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(arial, 15, Font.NORMAL);

            document.Open();

            PdfPTable table = new PdfPTable(3);
            PdfPCell cell = new PdfPCell(new Phrase("İlişik Kesme Formu", font));
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            table.AddCell(new Phrase("Öğrenci Adı: ", font));
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            table.AddCell(cell);

            table.AddCell(new Phrase("Öğrenci Soyadı: ", font));
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            table.AddCell(cell);

            table.AddCell(new Phrase("Öğrenci Numarası: ", font));
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            table.AddCell(cell);

            table.AddCell(new Phrase("Fakülte Adı: ", font));
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Üzerine kayıtlı herhangi bir kitap vb. doküman bulunmadığından ilişik kesme işleminin onayına"));
            cell.Colspan = 3;
            cell.Rowspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Onaylayan Adı: ", font));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Onaylayan Soyadı: ", font));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Onaylayan İmza: "));
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 2;
            cell.Rowspan = 2;
            table.AddCell(cell);

            document.Add(table);
            document.Close();
        }

        public bool PersonIsThere()
        {
            string query = "SELECT * FROM kutuphane.kisiler Where KisiselNo = @PersonalNumber AND RolId = 1";
            using (MySqlCommand cmd = MyDatabaseCommand.MyCommand(query))
            {
                cmd.Parameters.Add("@PersonalNumber", MySqlDbType.String).Value = PersonalNumber;
                return MyDatabaseCommand.MyReader(cmd);
            }
        }

        public void PersonSelect(DataGrid dataGrid)
        {
            try
            {
                string query = "SELECT kisiler.Id AS Id, kisiler.KisiselNo AS KurumNo, kisiler.Adi AS Adi, kisiler.Soyadi AS Soyadi, kisiler.TelNo AS Telefon_No FROM kutuphane.kisiler WHERE RolId = 1 ORDER BY Id DESC";
                MyDatabaseCommand.MySelectCommand(query, dataGrid);
                MyConnection.ConnectionClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıtlı Kişiler Listelenirken Hata İle Karşılaşıldı. \n" + ex);
            }
        }
    }
}
