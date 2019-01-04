using CinemaCLIENT.CategorieServiceReference;
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
    /// Logique d'interaction pour addCategorie.xaml
    /// </summary>
    public partial class addCategorie : Window
    {
        bool verif = false;
        CategorieServiceReference.CategoryServiceClient categoClient = new CategorieServiceReference.CategoryServiceClient();
        public addCategorie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            

            if(CategorieTxt.Text.Length > 0)
            {
                string name = CategorieTxt.Text;
                Category c = new Category();
                c = categoClient.FindByName(name);
                if(c == null)
                {
                    Category cat = new Category();
                    cat.Nom = name.ToUpper();
                    categoClient.Add(cat);
                    MessageBox.Show("Ajouter avec succes");
                }
                else
                {
                    MessageBox.Show("La categorie existe deja");
                }
            }
            else
            {
                MessageBox.Show("Le champ est vide");
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (verif)
            {
                string anciennom = CategorieTxt.Text;
                string nouveaunom = CategorieNouTxt.Text;
                if (CheickInf(anciennom,3) && CheickInf(nouveaunom, 3))
                {
                    if (!anciennom.Equals(nouveaunom))
                    {
                        Category c = new Category();
                        c = categoClient.FindByName(anciennom);
                        if (c != null)
                        {
                            int n = categoClient.Modifier(anciennom, nouveaunom);
                            if (n > 0)
                                MessageBox.Show("Categorie Modifier");
                            else
                                MessageBox.Show("Categorie nom modifier");
                        }
                        else
                        {
                            MessageBox.Show("La categorie n'existe pas");
                        }
                    }
                    else
                        MessageBox.Show("Les deux noms sont pareils");

                }
                else
                {
                    MessageBox.Show("Un champ est vide");
                }
            }

            if (!verif)
            {
                lbl.Visibility = Visibility.Visible;
                CategorieNouTxt.Visibility = Visibility.Visible;
                verif = true;
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
    }
}
