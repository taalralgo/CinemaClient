using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using CinemaCLIENT.AdresseServiceReference;
using CinemaCLIENT.StaffServiceReference;
using CinemaCLIENT.StoreServiceReference;
using Microsoft.Win32;
using Staff = CinemaCLIENT.StaffServiceReference.Staff;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour updateUser.xaml
    /// </summary>
    public partial class updateUser : Window
    {
        private string imgpicture;
        AdresseServiceClient AdrClient = new AdresseServiceClient();
        StoreServiceClient strClient = new StoreServiceClient();
        StaffServceClient staffClient = new StaffServceClient();
        static Staff tampon = new Staff();

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
            List<string> actives = new List<string>();
            actives.Add("Activer");
            actives.Add("Bloquer");
            Activecbx.ItemsSource = actives;
            if (staff.Active)
            {
                Activecbx.SelectedItem = "Activer";
            }
            else
            {
                Activecbx.SelectedItem = "Bloquer";
            }
            AdressCbx.ItemsSource = AdrClient.FindAll();
            AdressCbx.DisplayMemberPath = "Nom";
            AdressCbx.SelectedItem = staff.AdresseID;
            StoreCbx.ItemsSource = strClient.FindAll();
            StoreCbx.DisplayMemberPath = "ID";
            StoreCbx.SelectedItem = staff.StoreID;
            tampon = staff;
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

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            InitAndTraitement();
        }

        private void InitAndTraitement()
        {
            StaffServiceReference.Staff staff = new StaffServiceReference.Staff();
            StoreServiceReference.Store store = new StoreServiceReference.Store();
            AdresseServiceReference.Adresse adresse = new AdresseServiceReference.Adresse();
            string prenom, nom, email, role, username, password;
            bool active = true;
            prenom = PrenomTxt.Text;
            nom = NomTxt.Text;
            email = EmailTxt.Text;
            role = RoleTxt.Text;
            username = NomUtlilisateurTxt.Text;
            password = MdpTxt.Text;
            if (Activecbx.SelectedItem.Equals("Activer"))
                active = true;
            else if (Activecbx.SelectedItem.Equals("Bloquer"))
                active = false;
            if (SuppEspace_SupperieurQuatre(username) && SuppEspace_SupperieurQuatre(password))
            {
                if (CheickInf(prenom, 3) && CheickInf(nom, 2) && CheickInf(email, 10))
                {
                    if (ImgPath.Text.Length > 2)
                    {
                        staff.Prenom = prenom;
                        staff.Nom = nom;
                        staff.Email = email;
                        staff.Role = role;
                        staff.UserName = username;
                        staff.Passwd = password;
                        staff.AdresseID = ((AdresseServiceReference.Adresse)AdressCbx.SelectedItem).ID;
                        staff.StoreID = ((StoreServiceReference.Store)StoreCbx.SelectedItem).ID;

                        byte[] imge;
                        FileStream fs = new FileStream(imgpicture, FileMode.Open, FileAccess.Read);
                        BinaryReader rd = new BinaryReader(fs);
                        imge = rd.ReadBytes((int)fs.Length);
                        staff.Picture = imge;
                        staff.Active = active;
                        try
                        {
                            int n = staffClient.Update(tampon, staff);
                            if (n > 0)
                                MessageBox.Show("Staff modifier");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                        MessageBox.Show("Choisissez une photo");
                }
                else
                    MessageBox.Show("Le prenom doit etre > 3 , le nom >2 ou verifiez l'adresse mail ");
            }
            else
            {
                MessageBox.Show("Le nom d'utilisateur et le mot de passe doit etre > à 4");
            }
        }

        private bool CheickInf(string mot, int nbre)
        {
            string valeur = mot;
            //Pour tous les caractères de la valeur:
            for (int i = 0; i < valeur.Length; i++)
            {
                //Si c'est un espace:
                if (valeur[i].Equals(" "))
                    valeur.Replace(" ", ""); //On l'efface.
            }
            if (valeur.Length < nbre)
                return false;
            return true;
        }

        private bool SuppEspace_SupperieurQuatre(string champ)
        {
            string valeur = champ;
            //Pour tous les caractères de la valeur:
            for (int i = 0; i < valeur.Length; i++)
            {
                //Si c'est un espace:
                if (valeur[i].Equals(" "))
                    valeur.Replace(" ", ""); //On l'efface.
            }
            if (valeur.Length < 4)
                return false;
            return true;
        }
    }
}
