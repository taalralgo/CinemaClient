using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CinemaCLIENT.StaffServiceReference;
using Microsoft.Win32;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour updateUser.xaml
    /// </summary>
    public partial class updateUser : Window
    {
        private string imgpicture;

        public updateUser()
        {
            InitializeComponent();
        }

        public updateUser(Staff staff)
        {
            
            InitializeComponent();
            PrenomTxt.Text = staff.Prenom;
            NomTxt.Text = staff.Nom;
            EmailTxt.Text = staff.Email;
            NomUtlilisateurTxt.Text = staff.UserName;
            MdpTxt.Text = staff.Passwd;
            if (staff.Active)
                ActiveTxt.Text = "Activer";
            else
                ActiveTxt.Text = "Desactiver";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Parcourir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files(*.jpg|*.jpg|all files(*.*)|*.*";
            fd.ShowDialog();
            imgpicture = fd.FileName.ToString();
            ImgPath.Text = imgpicture;
            imgPhoto.Source = new BitmapImage(new Uri(imgpicture, UriKind.Absolute));
        }
    }
}
