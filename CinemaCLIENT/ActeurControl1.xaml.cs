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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaCLIENT
{
    /// <summary>
    /// Logique d'interaction pour ActeurControl1.xaml
    /// </summary>
    public partial class ActeurControl1 : UserControl
    {
        ActorServiceClient actorClient = new ActorServiceClient();
        public ActeurControl1()
        {
            InitializeComponent();
            tableau.ItemsSource = actorClient.FindAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addActor addActor = new addActor();
            addActor.Show();
        }
    }
}
