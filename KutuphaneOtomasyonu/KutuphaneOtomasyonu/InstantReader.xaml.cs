using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    public partial class InstantReader : UserControl
    {
        public InstantReader()
        {
            InitializeComponent();
        }

        MySQLDepositDAL deposit = new MySQLDepositDAL();

        private void AnlikOkuyucular_Loaded(object sender, RoutedEventArgs e)
        {
            deposit.InstantReaderSelect(InstantReader_dgv);
        }

        private void AnlikOkuyucular_GeriAl_Click(object sender, RoutedEventArgs e)
        {
            if (DepositId_txt.Text != "" && PersonName_txt.Text != "" && PersonalNumber_txt.Text != "" && PersonSurname_txt.Text != "" && BookName_txt.Text != "" && ISBN_txt.Text != "" && DueDate_txt.Text != "")
            {
                deposit.DepositId = Convert.ToInt32(DepositId_txt.Text.Trim());
                deposit.AcceptReturn();
                MessageBox.Show("Kitap Geri Alındı.");
                deposit.InstantReaderSelect(InstantReader_dgv);
            }
            else
            {
                MessageBox.Show("Kitabı Geri Almak İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void AnlikOkuyucular_Ara_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Find_txt.Text == "")
                {
                    MessageBox.Show("Arama Yapılacak Değeri Giriniz.");
                }
                else
                {
                    deposit.LookUpValue = Find_txt.Text.Trim();
                    deposit.FindId = Find_cmb.SelectedIndex;
                    deposit.InstantFindAndFill(InstantReader_dgv);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama Yapılırken Hata İle Karşılaşıldı. \n" + ex);
            }
        }
    }
}
