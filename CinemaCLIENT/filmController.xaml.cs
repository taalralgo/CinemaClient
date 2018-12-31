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
    /// Logique d'interaction pour filmController.xaml
    /// </summary>
    public partial class filmController : UserControl
    {
        public filmController()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AjoutFilm add = new AjoutFilm();
            add.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateFilm updateFilm = new UpdateFilm();
            updateFilm.Show();
        }
    }
}
