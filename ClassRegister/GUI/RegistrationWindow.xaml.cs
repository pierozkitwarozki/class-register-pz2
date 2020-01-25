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
using System.Threading;

namespace ClassRegister
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            HelpTip.Text= ("System oceniania studentów WAT" +
                "\n\nAby rejestracja użytkownika w systemie przebiegła " +
                "\npomyślnie musisz wprowadzić imię, nazwisko, " +
                "\nprawidłowy adres e-mail, nr indeksu oraz unikatową" +
                "\nnazwę użytkownika i hasło. W przeciwnym wypadku " +
                "\nrejestracja zakończy się niepowodzeniem.");
            //Przypisanie tekstu do toolTipa, ze wskazówką jak 
            //się rejestrować

        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
                this.Close(); 
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            //Wywołanie metody sprawdzającej czy dane są w porządku
            //Czy rejestracja pobiegła pomyślnie, jeśli tak
            //to dodanie do bazy danych wprowadzonych danych
            SignUp.JoinUs(FNBox.Text.ToString(),
                LNBox.Text.ToString(),
                UBox.Text.ToString(),
                EBox.Text.ToString(),
                CBox.Text.ToString(),
                PBox.Password.ToString(),
                RPBox.Password.ToString());          
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //Przesuwanie okna lewym przyciskiem myszy
            this.MouseLeftButtonDown += delegate { DragMove(); }; 
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            //Same, Enter to to samo co Apply, a Escape to samo co Close
            if (this.IsEnabled)
            {
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    this.ApplyBtn_Click(sender, e);
                }
                else if (Keyboard.IsKeyDown(Key.Escape))
                {
                    this.CloseBtn_Click(sender, e);
                }
            }
        }

        private void clearBox(object sender, MouseButtonEventArgs e)
        {
            //Czysczenie textBoxa/passwordBoxa, który jest obecnie 
            //kliknięty
            if (UBox.IsFocused)
            {
                UBox.Clear();
            }
            else if (FNBox.IsFocused)
            {
                FNBox.Clear();
            }
            else if (LNBox.IsFocused)
            {
                LNBox.Clear();
            }
            else if (EBox.IsFocused)
            {
                EBox.Clear();
            }
            else if (PBox.IsFocused)
            {
                PBox.Clear();
            }
            else if (RPBox.IsFocused)
            {
                RPBox.Clear();
            }
            else if (CBox.IsFocused)
            {
                CBox.Clear();
            }
        }
    }
        
}
