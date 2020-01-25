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

namespace ClassRegister.GUI
{
    /// <summary>
    /// Logika interakcji dla klasy Exit.xaml
    /// </summary>
    public partial class Exit : Window
    {
        public bool Exit_state { get; set; }
        public Exit()
        {
            InitializeComponent();
        }

        private void YBtn_Click(object sender, RoutedEventArgs e)
        {
            Exit_state = true;
            this.Close();    
        }

        private void NBtn_Click(object sender, RoutedEventArgs e)
        {
            Exit_state = false;
            this.Close();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += delegate { DragMove(); }; //żeby okno było możliwe do przesuwania
        }

    }
}
