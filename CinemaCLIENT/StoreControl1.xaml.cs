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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour StoreControl1.xaml
    /// </summary>
    public partial class StoreControl1 : UserControl
    {
        StoreServiceClient storeClient = new StoreServiceClient();
        public StoreControl1()
        {
            InitializeComponent();
            tableau.ItemsSource = storeClient.FindAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gestionStore gestionStore = new gestionStore();
            gestionStore.Show();
        }
    }
}
