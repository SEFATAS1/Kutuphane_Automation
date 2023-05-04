using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// KitapEkle.xaml etkileşim mantığı
    /// </summary>
    public partial class BookAdd : UserControl
    {
        public BookAdd()
        {
            InitializeComponent();
        }

        MySQLBookDAL book = new MySQLBookDAL();

        private void KitapListesi_Load(object sender, RoutedEventArgs e)
        {
            RecordDate_txt.Text = DateTime.Now.ToString("dd MMMM yyyy");
            book.BookSelect(BookAdd_dgv);
        }

        private void KitapEkle_Ekle_Click(object sender, RoutedEventArgs e)
        {
            if (BookName_txt.Text != "" && ISBN_txt.Text != "" && RecordDate_txt.Text != "" && Count_txt.Text != ""
                && WriterName_txt.Text != "" && WriterSurname_txt.Text != "" && Publisher_txt.Text != ""
                && BookState_cmb.SelectedIndex != 0 && CategoryName_cmb.SelectedIndex != 0)
            {
                book.BookName = BookName_txt.Text.Trim();
                book.ISBN = ISBN_txt.Text.Trim();
                book.RecordDate = RecordDate_txt.Text.Trim();
                book.Count = Convert.ToInt32(Count_txt.Text.Trim());
                book.WriterName = WriterName_txt.Text.Trim();
                book.WriterSurname = WriterSurname_txt.Text.Trim();
                book.PublisherName = Publisher_txt.Text.Trim();
                book.BookStateId = BookState_cmb.SelectedIndex;
                book.CategoryId = CategoryName_cmb.SelectedIndex;

                book.BookInsert();
                book.BookSelect(BookAdd_dgv);
            }
            else
            {
                MessageBox.Show("Kitap Kaydı İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void KitapEkle_Güncelle_Click(object sender, RoutedEventArgs e)
        {
            if (BookName_txt.Text != "" && ISBN_txt.Text != "" && RecordDate_txt.Text != "" && Count_txt.Text != ""
                && WriterName_txt.Text != "" && WriterSurname_txt.Text != "" && Publisher_txt.Text != ""
                && BookState_cmb.SelectedIndex != 0 && CategoryName_cmb.SelectedIndex != 0)
            {
                book.BookId = Convert.ToInt32(BookId_txt.Text.Trim());
                book.BookName = BookName_txt.Text.Trim();
                book.ISBN = ISBN_txt.Text.Trim();
                book.RecordDate = RecordDate_txt.Text.Trim();
                book.Count = Convert.ToInt32(Count_txt.Text.Trim());
                book.WriterName = WriterName_txt.Text.Trim();
                book.WriterSurname = WriterSurname_txt.Text.Trim();
                book.PublisherName = Publisher_txt.Text.Trim();
                book.BookStateId = BookState_cmb.SelectedIndex;
                book.CategoryId = CategoryName_cmb.SelectedIndex;

                book.BookUpdate();
                book.BookSelect(BookAdd_dgv);
            }
            else
            {
                MessageBox.Show("Kitap Bilgilerini Güncellemek İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void KitapEkle_Ara_Click(object sender, RoutedEventArgs e)
        {
            book.FindId = Ara_cmb.SelectedIndex;
            book.LookUpValue = Ara_txt.Text.Trim();
            book.BookFindAndFill(BookAdd_dgv);
        }
    }
}
