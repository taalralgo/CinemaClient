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
using System.Runtime.Serialization;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour GestionUsers.xaml
    /// </summary>
    public partial class GestionUsers : UserControl
    {
        static StaffServceClient StaffClient = new StaffServceClient();
        public GestionUsers()
        {
            InitializeComponent();
            try
            {
                StaffServceClient StaffClient = new StaffServceClient();
                ClearAndFillUpDataGrid();
            }
            catch(Exception)
            {
                MessageBox.Show("Verifiez si le service est demarré");
            }
        }

        private void ClearAndFillUpDataGrid()
        {
            tableau.ItemsSource = StaffClient.FindAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userinsc ins = new userinsc();
            ins.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Staff s = (Staff)tableau.SelectedItem;
            updateUser up = new updateUser(s);
            up.Show();
        }

        private void Bloquer_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = (Staff)tableau.SelectedItem;
            if(staff != null)
            {
                int n = StaffClient.Bloquer(staff);
                if (n > 0)
                    MessageBox.Show("User bloqué");
                else
                    MessageBox.Show("User est deja bloqué");
            }
        }

        private void Debloquer_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = (Staff)tableau.SelectedItem;
            if (staff != null)
            {
                int n = StaffClient.Debloquer(staff);
                if (n > 0)
                    MessageBox.Show("User debloqué");
                else
                    MessageBox.Show("User est deja actif");
            }
        }
    }
}
