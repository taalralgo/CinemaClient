using CinemaCLIENT.ActorServiceReference;
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
    /// Logique d'interaction pour addActor.xaml
    /// </summary>
    public partial class addActor : Window
    {
        ActorServiceClient actorClient = new ActorServiceClient();
        bool verif = false;
        public addActor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            string prenom = PrenomActeurTxt.Text;
            string nom = NomActeurTxt.Text;
            if(CheickInf(prenom,4) && CheickInf(nom,2))
            {
                Actor a = new Actor();
                a = actorClient.FindByName(nom,prenom);
                if(a != null)
                {
                    MessageBox.Show("L'acteur existe deja");
                }
                else
                {
                    Actor act = new Actor();
                    act.Nom = nom.ToUpper();
                    act.Prenom = prenom;
                    actorClient.Add(act);
                    MessageBox.Show("Acteur ajouter");
                }
            }
            else
            {
                MessageBox.Show("Le nom ou le prenom n'est pas bien renseigné");
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

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (verif)
            {
                string ancienprenom = PrenomActeurTxt.Text;
                string anciennom = NomActeurTxt.Text;
                string nouveauprenom = NouveauPrenomTxt.Text;
                string nouveaunom = NouveauNomTxt.Text;
                if (CheickInf(anciennom, 2) && CheickInf(ancienprenom, 4) && CheickInf(nouveaunom, 2) && CheickInf(nouveauprenom, 4))
                {
                    Actor a = new Actor();
                    a = actorClient.FindByName(anciennom, ancienprenom);
                    if (a != null)
                    {
                        int n = actorClient.Modifier(anciennom,ancienprenom,nouveaunom,nouveauprenom);
                        if (n > 0)
                            MessageBox.Show("Acteur modifier");
                        else
                            MessageBox.Show("Erreur de modification");
                    }
                    else
                    {
                        MessageBox.Show("L'acteur n'existe pas");
                    }
                }
                else
                    MessageBox.Show("Un champ n'est pas bien renseigné");
            }

            if (!verif)
            {
                lbl1.Visibility = Visibility.Visible;
                lbl2.Visibility = Visibility.Visible;
                NouveauPrenomTxt.Visibility = Visibility.Visible;
                NouveauNomTxt.Visibility = Visibility.Visible;
                verif = true;
            }
        }
    }
}
