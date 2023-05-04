using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// KitapListesi.xaml etkileşim mantığı
    /// </summary>
    public partial class Deposit : UserControl
    {
        public Deposit()
        {
            InitializeComponent();
        }

        MySQLDepositDAL deposit = new MySQLDepositDAL();
        DateTime dt = DateTime.Now;

        private void Emanet_Loaded(object sender, RoutedEventArgs e)
        {
            DeliveryDate_txt.Text = dt.ToString("dd MMMM yyyy");
            dt = deposit.CountDays(dt, 15);
            DueDate_txt.Text = dt.ToString("dd MMMM yyyy");
            deposit.PersonSelect(PersonList_dgv);
            deposit.BookSelect(BookList_dgv);
        }

        private void btnEmanetVer(object sender, RoutedEventArgs e)
        {
            if (PersonelNumber_txt.Text == "" && PhoneNumber_txt.Text == "" && PersonName_txt.Text == "" && PersonSurName_txt.Text == "")
            {
                MessageBox.Show("Kitap Emanet İşlemi İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
            else
            {
                deposit.DeliveryDate = DeliveryDate_txt.Text.Trim();
                deposit.DueDate = DueDate_txt.Text.Trim();
                deposit.PersonId = Convert.ToInt32(PersonId_txt.Text.Trim());
                deposit.BookId = Convert.ToInt32(BookId_txt.Text.Trim());
                deposit.DepositInsert();
                deposit.BookSelect(BookList_dgv);
            }
        }

        private void Ara_Click(object sender, RoutedEventArgs e)
        {
            deposit.FindId = Find_cmb.SelectedIndex;
            deposit.LookUpValue = Find_txt.Text.Trim();
            switch (Find_cmb.SelectedIndex)
            {
                case 0:
                    deposit.PersonSelect(PersonList_dgv);
                    deposit.BookSelect(BookList_dgv);
                    break;
                case > 0 and < 3:
                    deposit.DepositFind(PersonList_dgv);
                    break;
                case > 2 and < 5:
                    deposit.DepositFind(BookList_dgv);
                    break;
            }
        }
    }
}
