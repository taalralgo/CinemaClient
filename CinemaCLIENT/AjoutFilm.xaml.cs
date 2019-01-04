using CinemaCLIENT.FilmServiceReference1;
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
    /// Logique d'interaction pour AjoutFilm.xaml
    /// </summary>
    public partial class AjoutFilm : Window
    {
        public AjoutFilm()
        {
            InitializeComponent();
            LangueServiceReference1.LanguageServiceClient langueClient = new LangueServiceReference1.LanguageServiceClient();
            OLangueCbx.ItemsSource = langueClient.FindAll();
            OLangueCbx.DisplayMemberPath = "Name";
            LangueCbx.ItemsSource = langueClient.FindAll();
            LangueCbx.DisplayMemberPath = "Name";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            FilmServiceClient FilmClient = new FilmServiceClient();
            Film film = new Film();
            string title, description, eva, caracteristik;
            int coutR, langueO, lang, dureeF, dureeL, prixL;
            DateTime anneeS;
            title = TitreTxt.Text;
            description = DescriptionTxt.Text;
            eva = EvaluatoinTxt.Text;
            caracteristik = CaracteristqueSpTxt.Text;
            try
            {
                coutR = int.Parse(CoutRTxt.Text);
                langueO = ((LangueServiceReference1.Language)LangueCbx.SelectedItem).ID;
                lang = ((LangueServiceReference1.Language)LangueCbx.SelectedItem).ID;
                dureeL = int.Parse(DureeLTxt.Text);
                dureeF = int.Parse(LongueurTxt.Text);
                prixL = int.Parse(TarifLTxt.Text);
                anneeS = DateTime.Parse(AnneeSortie.Text);
                if (CheickInf(title, 2) && CheickInf(description, 5) && CheickInf(eva, 5) && CheickInf(caracteristik, 5)
                    && anneeS.Year > DateTime.Today.Year && coutR > 5 && dureeL > 12 && dureeF > 45 && prixL > 5)
                {
                    MessageBox.Show("Un des champs n'est pas bien renseigné, Referez vous à la doc pour plus de detail");
                }
                else
                {
                    Film f = new Film();
                    f = FilmClient.FindByTitle(title);
                    if (f == null)
                    {
                        film.Title = title;
                        film.Description = description;
                        film.Release_year = anneeS;
                        film.Rental_duration = dureeL;
                        film.Rental_rate = prixL;
                        film.Lenght = dureeF;
                        film.LanguageID = lang;
                        film.Languag_origineID = langueO;
                        film.Rating = eva;
                        film.Special_features = caracteristik;
                        film.Remplacement_cost = coutR;
                        try
                        {
                            FilmClient.Add(film);
                            MessageBox.Show("Film ajouter avec succes");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Verifier si le service est demarré");
                        }
                    }
                    else
                        MessageBox.Show("Film existe");
                    
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Verifiez si les valeurs numeriques sont bien renseignées et qu'elles sont valides");
            }
        }
    }
}
