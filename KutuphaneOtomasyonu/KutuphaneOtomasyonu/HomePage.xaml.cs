using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Anasayfa.xaml etkileşim mantığı
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }

        bool flag = true;
        public void SayfaEkle(System.Windows.Controls.Grid grd, UserControl usrcntrl)
        {
            if (grd.Children.Count > 0)
            {
                grd.Children.Clear();
                grd.Children.Add(usrcntrl);
            }
            else
            {
                grd.Children.Add(usrcntrl);
            }
        }

        private void Anasayfa_Loaded(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new DocumentInquiry());
        }

        private void Kapat_btn_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen girisEkrani = new LoginScreen();
            girisEkrani.Show();
            this.Close();
        }

        private void Kucult_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MenuAcKapa_btn(object sender, RoutedEventArgs e)
        {
            double Genislik;
            Genislik = 273.6;

            GridLength grd;

            if (flag == true)
            {
                grd = new GridLength(4, GridUnitType.Star);
                MenuBoyut.Width = grd;
                Anasayfa_Viewbox.Width = Genislik;
                KitapEkle_lbl.Visibility = Visibility.Hidden;
                KitapListesi_lbl.Visibility = Visibility.Hidden;
                AkademisyenKayit_lbl.Visibility = Visibility.Hidden;
                OgrenciKayit_lbl.Visibility = Visibility.Hidden;
                AnlikOkuyucular_lbl.Visibility = Visibility.Hidden;
                ZamaniDolanlar_lbl.Visibility = Visibility.Hidden;
                Cikis_lbl.Visibility = Visibility.Hidden;
                VersiyonNumarasi_lbl.Visibility = Visibility.Hidden;
                Baslik_lbl.Visibility = Visibility.Hidden;
                flag = false;
            }
            else
            {
                grd = new GridLength(20, GridUnitType.Star);
                MenuBoyut.Width = grd;
                Anasayfa_Viewbox.Width = Genislik;
                KitapEkle_lbl.Visibility = Visibility.Visible;
                KitapListesi_lbl.Visibility = Visibility.Visible;
                AkademisyenKayit_lbl.Visibility = Visibility.Visible;
                OgrenciKayit_lbl.Visibility = Visibility.Visible;
                AnlikOkuyucular_lbl.Visibility = Visibility.Visible;
                ZamaniDolanlar_lbl.Visibility = Visibility.Visible;
                Cikis_lbl.Visibility = Visibility.Visible;
                VersiyonNumarasi_lbl.Visibility = Visibility.Visible;
                Baslik_lbl.Visibility = Visibility.Visible;
                flag = true;
            }
        }

        private void Anasayfa_IlkSayfa_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new DocumentInquiry());
        }

        private void MenuButon_AkademisyenKayıt_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new AcademicianRegistration());
        }

        private void MenuButon_KitapListesi_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new Deposit());
        }

        private void MenuButon_KitapEkle_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new BookAdd());
        }

        private void MenuButon_OgrenciKayit_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new StudentRegistretion());
        }

        private void MenuButon_AnlikOkuyucular_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new InstantReader());
        }

        private void MenuButon_ZamaniDolanlar_Click(object sender, RoutedEventArgs e)
        {
            SayfaEkle(MasterPage, new DueDateDeposit());
        }

        private void MenuButon_OturumKapat_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen girisEkrani = new LoginScreen();
            girisEkrani.Show();
            this.Close();
        }
    }
}
