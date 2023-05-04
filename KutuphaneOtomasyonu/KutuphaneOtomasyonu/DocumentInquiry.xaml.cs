using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    public partial class DocumentInquiry : UserControl
    {
        public DocumentInquiry()
        {
            InitializeComponent();
        }

        MySQLGraduationInquiryDAL graduation = new MySQLGraduationInquiryDAL();

        private void BelgeSorgulama_Loaded(object sender, RoutedEventArgs e)
        {
            graduation.PersonSelect(GraduationInquiry_dgv);
        }

        private void BelgeSorgulama_Ara_Click(object sender, RoutedEventArgs e)
        {
            if (PersonalNumber_txt.Text == "")
            {
                MessageBox.Show("Aramak İstediğiniz Kişinin Kurum Numarasını Lütfen Giriniz.");
            }
            else
            {
                graduation.PersonalNumber = PersonalNumber_txt.Text.Trim();
                graduation.Find(GraduationInquiry_dgv);
            }
        }

        private void BelgeSorgulama_Sorgula_Click(object sender, RoutedEventArgs e)
        {
            if (PersonId_txt.Text == "" && PersonName_txt.Text == "" && PersonSurname_txt.Text == "" && PersonalNumber_txt.Text == "")
            {
                MessageBox.Show("Sorgulamak İstediğiniz Kişinin Eksik Bilgileri Bulunmaktadır. Lütfen Eksik Bilgi Girişi Yapmayınız.");
            }
            else
            {
                graduation.PersonId = Convert.ToInt32(PersonId_txt.Text.Trim());
                if (graduation.Inquiry())
                {
                    MessageBox.Show("Sorguladığınız Kişinin İade Etmediği Kitap/Kitaplar Bulunmaktadır.");
                }
                else
                {
                    MessageBox.Show("Sorgulamak İstediğiniz Kişinin Üzerine Emanette Kitap Bulunmamaktadır.");
                    graduation.PersonalNumber = PersonalNumber_txt.Text.Trim();
                    graduation.PersonName = PersonName_txt.Text.Trim();
                    graduation.PersonSurname = PersonSurname_txt.Text.Trim();
                    graduation.PDFCreate();
                    MessageBox.Show("Belge Oluşturuldu ve Kayıt Edildi.");
                }
            }
        }

        private void BelgeSorgulama_Pdf_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
