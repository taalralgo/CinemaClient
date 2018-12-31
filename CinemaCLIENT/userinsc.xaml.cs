using CinemaCLIENT.AdresseServiceReference;
using CinemaCLIENT.StaffServiceReference;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour userinsc.xaml
    /// </summary>
    public partial class userinsc : Window
    {
        string imgpicture;
        AdresseServiceClient AdresseClient = new AdresseServiceClient();
        public userinsc()
        {
            InitializeComponent();
            try
            {
                StoreServiceReference.StoreServiceClient StoreClient = new StoreServiceReference.StoreServiceClient();
                
                StoreCbx.ItemsSource = StoreClient.FindAll();
                StoreCbx.DisplayMemberPath = "ID";
                AdressCbx.ItemsSource = AdresseClient.FindAll();
                AdressCbx.DisplayMemberPath = "Nom";
                List<string> ListActive = new List<string>();
                ListActive.Add("Activer");
                ListActive.Add("Desactiver");
                ActiveCbx.ItemsSource = ListActive;

                AdressCbx.SelectedIndex = 1;
                StoreCbx.SelectedIndex = 1;
                ActiveCbx.SelectedIndex = 1;
            }
            catch(Exception)
            {
                MessageBox.Show("Verifiez si le service est demarré");
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InitAndTraitement()
        {
            StaffServiceReference.Staff staff = new StaffServiceReference.Staff();
            StoreServiceReference.Store store = new StoreServiceReference.Store();
            AdresseServiceReference.Adresse adresse = new AdresseServiceReference.Adresse();
            string prenom, nom, email, role, username, password, activestr;
            bool active;
            prenom = PrenomTxt.Text;
            nom = NomTxt.Text;
            email = EmailTxt.Text;
            role = RoleTxt.Text;
            username = NomUtlilisateurTxt.Text;
            password = MdpTxt.Text;
            activestr = (string)ActiveCbx.SelectedItem;
            if(SuppEspace_SupperieurQuatre(username) && SuppEspace_SupperieurQuatre(password))
            {
                if (CheickInf(prenom,3) && CheickInf(nom,2) && CheickInf(email,10) )
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
                        staff.Store.ID = staff.StoreID;
                        staff.Adresse.ID = staff.AdresseID;

                        byte[] imge;
                        FileStream fs = new FileStream(imgpicture, FileMode.Open, FileAccess.Read);
                        BinaryReader rd = new BinaryReader(fs);
                        imge = rd.ReadBytes((int)fs.Length);
                        staff.Picture = imge;
                        if (activestr.Equals("Activer"))
                        {
                            active = true;
                            staff.Active = active;
                        }
                        else
                        {
                            active = false;
                            staff.Active = active;
                        }
                        try
                        {
                            StaffServceClient StaffClient = new StaffServceClient();
                            StaffClient.Add(staff);
                        }
                        catch(Exception ex)
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

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            InitAndTraitement();
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
