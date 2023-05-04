using Controller.MySQLController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    public partial class StudentRegistretion : UserControl
    {
        MySQLStudentDAL student = new MySQLStudentDAL();

        public StudentRegistretion()
        {
            InitializeComponent();
        }

        private void OgrenciKayit_Loaded(object sender, RoutedEventArgs e)
        {
            student.PersonSelect(StudentList_dgv);
        }

        private void OgrenciKayit_UyeEkle(object sender, RoutedEventArgs e)
        {
            if (StudentName_txt.Text != "" && StudentSurname_txt.Text != "" && PhoneNumber_txt.Text != "" && Role_cmb.SelectedIndex == 1 && PersonalNumber_txt.Text != "")
            {
                student.PersonName = StudentName_txt.Text.Trim().ToUpper();
                student.PersonSurname = StudentSurname_txt.Text.Trim().ToUpper();
                student.PersonalNumber = PersonalNumber_txt.Text.Trim();
                student.PhoneNumber = PhoneNumber_txt.Text.Trim();
                student.RoleId = Role_cmb.SelectedIndex;
                student.PersonInsert();
                student.PersonSelect(StudentList_dgv);
            }
            else
            {
                MessageBox.Show("Öğrenci Kaydı İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void OgrenciKayit_Güncelle(object sender, RoutedEventArgs e)
        {
            if (StudentId_txt.Text == "" && PersonalNumber_txt.Text == "" && StudentName_txt.Text == "" && StudentSurname_txt.Text == "" && PhoneNumber_txt.Text == "" && Role_cmb.SelectedIndex != 1)
            {
                student.PersonId = Convert.ToInt32(StudentId_txt.Text.Trim());
                student.PersonName = StudentName_txt.Text.Trim().ToUpper();
                student.PersonSurname = StudentSurname_txt.Text.Trim().ToUpper();
                student.PersonalNumber = PersonalNumber_txt.Text.Trim();
                student.PhoneNumber = PhoneNumber_txt.Text.Trim();
                student.RoleId = Convert.ToInt32(Role_cmb.SelectedIndex);
                student.PersonUpdate();

                student.PersonSelect(StudentList_dgv);
            }
            else
            {
                MessageBox.Show("Öğrenci Kaydının Güncellenmesi İçin Gerekli Bilgiler Eksik. Lütfen Eksik Bilgi Girmeyiniz.");
            }
        }

        private void OgrenciKayit_Sil(object sender, RoutedEventArgs e)
        {
            if (StudentId_txt.Text == "")
            {
                MessageBox.Show("Öğrencinin Bilgilerini Silmek için Gerekli Id Bilgis Alanı Boş. Lütfen Doldurunuz.");
            }
            else
            {
                student.PersonId = Convert.ToInt32(StudentId_txt.Text.Trim());
                student.PersonDelete();
                student.PersonSelect(StudentList_dgv);
            }
        }

        private void OgrenciKayit_Ara_Click(object sender, RoutedEventArgs e)
        {
            student.FindId = Convert.ToInt32(Find_cmb.SelectedIndex);
            student.LookUpValue = Find_txt.Text.Trim();
            student.PersonFindAndFill(StudentList_dgv);
        }
    }
}
