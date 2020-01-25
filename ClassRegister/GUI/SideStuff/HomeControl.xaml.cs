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

namespace ClassRegister.GUI
{
    /// <summary>
    /// Logika interakcji dla klasy HomeWindow.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            Intro.Text = ("Witaj " + Dashboard.DashH.User.FirstName + "!" +
            "\nZalogowałeś(aś) się do systemu " +
            " oceniania studentów Wojskowej Akademii Technicznej" +
            " im. Jarosława Dąbrowskiego w Warszawie." +
            "\nZ systemu logowania mogą korzystać studenci oraz pracownicy Akademii."+
            "\nJesteś zalogowany jako: " + Dashboard.DashH.UserFunction);
        }

    }
}
