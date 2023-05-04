using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    public partial class DueDateDeposit : UserControl
    {
        public DueDateDeposit()
        {
            InitializeComponent();
        }

        MySQLDepositDAL deposit = new MySQLDepositDAL();

        private void ZamaniDolanlar_Loaded(object sender, RoutedEventArgs e)
        {
            deposit.DueSelect(OutOfTime_dgv);
        }

        private void ZamaniDolanlar_GeriAl_Click(object sender, RoutedEventArgs e)
        {
            if (DepositId_txt.Text == "" && BookId_txt.Text == "")
            {
                MessageBox.Show("Kitabı Geri Almak İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
            else
            {
                deposit.DepositId = Convert.ToInt32(DepositId_txt.Text.Trim());
                deposit.BookId = Convert.ToInt32(BookId_txt.Text.Trim());
                deposit.DueAcceptReturn();
                deposit.DueSelect(OutOfTime_dgv);
            }
        }

        private void ZamaniDolanlar_Ara_Click(object sender, RoutedEventArgs e)
        {
            deposit.FindId = Ara_cmb.SelectedIndex;
            deposit.LookUpValue = Ara_txt.Text.Trim();
            deposit.DueFindAndFill(OutOfTime_dgv);
        }
    }
}
