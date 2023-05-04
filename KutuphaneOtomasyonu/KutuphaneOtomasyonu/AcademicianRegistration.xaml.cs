using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    public partial class AcademicianRegistration : UserControl
    {
        public AcademicianRegistration()
        {
            InitializeComponent();
        }

        MySQLAcademicianDAL academician = new MySQLAcademicianDAL();

        private void AkademisyenKayit_Loaded(object sender, RoutedEventArgs e)
        {
            academician.PersonSelect(Academician_dgv);
        }

        private void AkademisyenKayit_AkademisyenEkle_Click(object sender, RoutedEventArgs e)
        {
            if (AcademicianName_txt.Text != "" && AcademicianSurname_txt.Text != "" && AcademicianPersonalNumber_txt.Text != "" && AcademicianPhoneNumber_txt.Text != "" && Role_cmb.SelectedIndex == 2)
            {
                academician.PersonName = AcademicianName_txt.Text.Trim().ToUpper();
                academician.PersonSurname = AcademicianSurname_txt.Text.Trim().ToUpper();
                academician.PersonalNumber = AcademicianPersonalNumber_txt.Text.Trim();
                academician.PhoneNumber = AcademicianPhoneNumber_txt.Text.Trim();
                academician.RoleId = Convert.ToInt32(Role_cmb.SelectedIndex);

                academician.PersonInsert();
                academician.PersonSelect(Academician_dgv);
            }
            else
            {
                MessageBox.Show("Akademisyen Kaydı İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void AkademisyenKayit_KayitGunscelle_Click(object sender, RoutedEventArgs e)
        {
            if (AcademicianName_txt.Text == "" && AcademicianSurname_txt.Text == "" && AcademicianPersonalNumber_txt.Text == "" && AcademicianPhoneNumber_txt.Text == "" && Role_cmb.SelectedIndex != 2)
            {
                academician.PersonId = Convert.ToInt32(AcademicianId_txt.Text.Trim());
                academician.PersonName = AcademicianName_txt.Text.Trim().ToUpper();
                academician.PersonSurname = AcademicianSurname_txt.Text.Trim().ToUpper();
                academician.PersonalNumber = AcademicianPersonalNumber_txt.Text.Trim();
                academician.PhoneNumber = AcademicianPhoneNumber_txt.Text.Trim();
                academician.RoleId = Convert.ToInt32(Role_cmb.SelectedIndex);
                academician.PersonUpdate();
                academician.PersonSelect(Academician_dgv);
            }
            else
            {
                MessageBox.Show("Akademisyenin Bilgilerini Güncellemek İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void AkademiseyenKayit_AkademisyenSil_Click(object sender, RoutedEventArgs e)
        {
            if (AcademicianId_txt.Text == "")
            {
                MessageBox.Show("Akademisyenin Bilgilerini Silmek için Gerekli Id Bilgisi Alanı Boş. Lütfen Silmek İstediğiniz Kayıt Satırını Tekrar Seçiniz.");
            }
            else
            {
                academician.PersonId = Convert.ToInt32(AcademicianId_txt.Text.Trim());
                academician.PersonDelete();
                academician.PersonSelect(Academician_dgv);
            }
        }

        private void AkademiseyenKayit_Ara_Click(object sender, RoutedEventArgs e)
        {
            academician.FindId = Convert.ToInt32(Find_cmb.SelectedIndex);
            academician.LookUpValue = Find_txt.Text.Trim();
            academician.PersonFindAndFill(Academician_dgv);
        }
    }
}
