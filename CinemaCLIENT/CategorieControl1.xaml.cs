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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour CategorieControl1.xaml
    /// </summary>
    public partial class CategorieControl1 : UserControl
    {
        public CategorieControl1()
        {
            InitializeComponent();
            try
            {
                CategoryServiceClient CategoClient = new CategoryServiceClient();
                tableau.ItemsSource = CategoClient.FindAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur n'est pas demarré");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addCategorie addCategorie = new addCategorie();
            addCategorie.Show();
        }
    }
}
