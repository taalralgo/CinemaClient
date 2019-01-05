using CinemaCLIENT.AdresseServiceReference;
using CinemaCLIENT.StaffServiceReference;
using CinemaCLIENT.StoreServiceReference;
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

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour gestionStore.xaml
    /// </summary>
    public partial class gestionStore : Window
    {
        bool verif = false;
        StoreServiceClient storeClient = new StoreServiceClient();
        AdresseServiceClient adresseClient = new AdresseServiceClient();
        public gestionStore()
        {
            InitializeComponent();
            Adressecbx.ItemsSource = adresseClient.FindAll();
            Adressecbx.SelectedIndex = 1;
            Adressecbx.DisplayMemberPath = "Nom";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            StoreServiceReference.Store s = new StoreServiceReference.Store();
            int n = (int)((AdresseServiceReference.Adresse)Adressecbx.SelectedItem).ID;
            s.AdresseID = n;
            StoreServiceReference.Store sto = new StoreServiceReference.Store();
            sto = storeClient.FindByAdresse(s.AdresseID);
            if(sto == null)
            {
                storeClient.Add(s);
                MessageBox.Show("Store ajouté");
            }
            else
            {
                MessageBox.Show("Il y'a un store à cette adresse ou le manager est deja defini");
            }
        }
        /*
         private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }*/

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (verif)
            {
                int ancienAdresse = ((AdresseServiceReference.Adresse)Adressecbx.SelectedItem).ID;
                int nouveauAdresse = ((AdresseServiceReference.Adresse)NouveauAdressecbx.SelectedItem).ID;

                StoreServiceReference.Store s = new StoreServiceReference.Store();
                int n = storeClient.Modifier(ancienAdresse, nouveauAdresse);
                if (n > 0)
                    MessageBox.Show("Store modifier");
                else
                    MessageBox.Show("Store n'est pas modifier");
            }

            if (!verif)
            {
                lbl2.Visibility = Visibility.Visible;
                NouveauAdressecbx.Visibility = Visibility.Visible;
                verif = true;
                NouveauAdressecbx.Items.Add(adresseClient.FindAll());
                NouveauAdressecbx.DisplayMemberPath = "Nom";
                NouveauAdressecbx.SelectedIndex = 1;
            }
        }
    }
}
