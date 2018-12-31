using CinemaCLIENT.StaffServiceReference;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StaffServceClient StaffClient = new StaffServceClient();
                //StaffServceClient d = new StaffServceClient();
                string username = TxtUsername.Text;
                string password = TxtPassword.Password;
                if (SuppEspace_SupperieurTrois(username) && password.Length > 4)
                {
                    Staff staff = new Staff();
                    staff = StaffClient.FindUser(username);
                    if (staff != null)
                    {
                        if (staff.Passwd.Equals(password))
                        {
                            if (staff.Active)
                            {
                                Menu t = new Menu();
                                t.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(staff.UserName + " n'est pas actif, veuillez contacter l'administarteur");
                                TxtUsername.Text = "";
                                TxtPassword.Password = "";
                            }

                        }
                        else
                        {
                            MessageBox.Show("Le mot de passe est incorrect");
                            TxtPassword.Password = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Le nom d'utilisateur est incorrect");
                        TxtUsername.Text = "";
                        TxtPassword.Password = "";
                    }
                }
                else
                {
                    MessageBox.Show("Le nom d'utilisateur et le mot de passe doit etre supperieur à 4");
                    TxtUsername.Text = "";
                    TxtPassword.Password = "";
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Verifiez si le service est demarré");
            }
            
            
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Supprimer les espaces dans une chaine et check si le champ > à 3
        private bool SuppEspace_SupperieurTrois(string champ)
        {
            string valeur = champ;
            //Pour tous les caractères de la valeur:
            for (int i = 0; i < valeur.Length; i++)
            {
                //Si c'est un espace:
                if (valeur[i].Equals(" "))
                    valeur.Replace(" ", ""); //On l'efface.
            }
            if (valeur.Length < 3)
                return false;
            return true;
        }
    }
}
