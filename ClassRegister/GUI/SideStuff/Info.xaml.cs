using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassRegister
{
    /// <summary>
    /// Logika interakcji dla klasy Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        //To mój taki własny, ładniejszy MessageBox
        public Info()
        {
            InitializeComponent();   
        }
        public void Assign(string text)
        {
            //Przypisanie tekstu podanego w parametrze do tekst bloku
            this.TBlock.Text = text; 
        }

        private void OBtn_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

    }
}
