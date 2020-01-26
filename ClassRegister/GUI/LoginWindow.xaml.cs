using NHibernate;
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

namespace ClassRegister
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    public partial class LoginWindow : Window
    {
        
        public static Model.NUserM LUser { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += delegate { DragMove(); };
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            GUI.Exit exit = new GUI.Exit();
            exit.ShowDialog();
            if (exit.Exit_state == true)
            {
                this.Close();               
            }            
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //zaloguj
            this.LogIn();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            //Wywoływanie nowego okienka z panelem rejestracji
            RegistrationWindow rw = new RegistrationWindow();
            rw.ShowDialog();
        }
              
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            //Logowanie enterem i wychodzenie escapem
            if (this.IsEnabled)
            {
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    this.LoginBtn_Click(sender, e);
                }
                else if (Keyboard.IsKeyDown(Key.Escape))
                {
                    this.CloseBtn_Click(sender, e);
                }
            }
            
        }

        private void ClearBox(object sender, MouseButtonEventArgs e)
        {
            //Czyszczenie boxów double clickiem
            if (UBox.IsFocused)
            {
                UBox.Clear();
            }
            else if (PBox.IsFocused)
            {
                PBox.Clear();
            }
        }

        private void LogIn()
        {
            //Metoda odpowiadająca za logowanie
            if (SignUp.USignUp(UBox.Text.ToString(), PBox.Password.ToString()) != null)
            {
                LUser = SignUp.USignUp(UBox.Text.ToString(), PBox.Password.ToString());
                GUI.Dashboard register = new GUI.Dashboard();
                this.Close();
                register.ShowDialog();
            }
            else
            {
                Info info = new Info();
                info.Assign("Niepoprawne hasło lub login.");
                info.ShowDialog();
            }
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    } 
}
