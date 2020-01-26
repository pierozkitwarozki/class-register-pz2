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
    /// Logika interakcji dla klasy Register.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public static Handling.DashboardHandle DashH { get; private set; }
        private int clickCounter = -2;
        private bool infoClik = false;
        
        public Dashboard()
        {
            DashH = new Handling.DashboardHandle();
            InitializeComponent();
            this.LoadDashboardAsync();
            
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //Przesuwanie okna
            this.MouseLeftButtonDown += delegate { DragMove(); };
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            //Zamykanie okna 
            GUI.Exit exit = new GUI.Exit();
            exit.ShowDialog();
            if (exit.Exit_state == true)
            {
                this.Close();
            }
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            //wychodzenie escapem
            if (this.IsEnabled)
            {
                if (Keyboard.IsKeyDown(Key.Escape))
                {
                    this.CloseBtn_Click(sender, e);
                }
            }
        }

        private void GBtn_Click(object sender, RoutedEventArgs e)
        {
            //Pokazywanie ocen
            this.ShowGradesAsync();
        }

        private void HBtn_Click(object sender, RoutedEventArgs e)
        {
            //Przejście na stronę główną
            this.EnterHome();
            TitleL.Content = HBtn.Content;
        }

        private void AGBtn_Click(object sender, RoutedEventArgs e)
        {
            //Rozpoczęcie dodawania oceny
            this.AddGradeProcessStart();
        }

        private void LOBtn_Click(object sender, RoutedEventArgs e)
        {
            //Wylogowanie
            this.LogOut();

        }
        private void MABtn_Click(object sender, RoutedEventArgs e)
        {
            //Pokazywanie danych zalogowanego użytkownika
            this.MyPersonalData();
            TitleL.Content = MABtn.Content;
        }
                  
        private void BBtn_Click(object sender, RoutedEventArgs e)
        {
            //Przechodzenie przy listBoxach wstecz
            //przy dodawaniu oceny (wybór grupy, przedmiotu, studenta etc.)
            this.BackButtonClicker();
        }

        private void NBtn_Click(object sender, RoutedEventArgs e)
        {
            //Przechodzenie przy listBoxach naprzód
            //przy dodawaniu oceny, analogicznie
            this.NextButtonClickerAsync(); 
        }
        
        private void IBtn_Click(object sender, RoutedEventArgs e)
        {
            //Pokazanie fajnego tekściku o projekcie
            this.ShowAppInfo();
            TitleL.Content = IBtn.Content;
        }

        private void MSBtn_Click(object sender, RoutedEventArgs e)
        {
            //Pokazuje listę przedmiotów danej grupy/prowadzonych przez danego 
            //pracownika
            this.ShowMySubjectsAsync();
            
        }

        private void SBtn_Click(object sender, RoutedEventArgs e)
        {
            //Pokazywanie informacji o nauczycielu
            if (infoClik == false)
            {
                this.ShowTeacherInfoAsync();
            }
            else this.ShowMySubjectsAsync();            
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            //Minimalizowanie okna
            this.WindowState = WindowState.Minimized;
        }


        private async void ShowGradesAsync()
        {
                if (DashH.Student == true)
                {
                //Wczytywanie danych do kontrolki DataGrid
                //Pobieranie danych i zapisywanie ich do Gridu 
                //w klasie z obsługą Handling.DashboardHandle
                    List<Task<List<Model.NStudentGrades>>> tasks = new List<Task<List<Model.NStudentGrades>>>();
                    tasks.Add(Task.Run(() => DashH.GradeDataLoadAsync()));
                    var data2 = await Task.WhenAll(tasks);
                    Grades.ItemsSource = data2.ElementAt(0);               
                    MyPersonalDataHide();
                    HomeCtrl.Visibility = Visibility.Hidden;
                    SBLBox.Visibility = Visibility.Hidden;
                    SBtn.Visibility = Visibility.Hidden;
                    Grades.Visibility = Visibility.Visible;
                    TBlock2.Visibility = Visibility.Hidden;
                    WATImg.Visibility = Visibility.Visible;
                    TitleL.Content = GBtn.Content;
                }
                else SignUp.ShowMessage("Tylko dla studentów.");       
        }

        private void EnterHome()
        {
            //Ukrycie kontrolek i pokzanie ekranu głównego
            MyPersonalDataHide();           
            HomeCtrl.Visibility = Visibility.Visible;
            Grades.Visibility = Visibility.Hidden;
            TBlock2.Visibility = Visibility.Hidden;
            WATImg.Visibility = Visibility.Hidden;
            SBLBox.Visibility = Visibility.Hidden;
            SBtn.Visibility = Visibility.Hidden;
            clickCounter = -2;
            MakeGradeAdderVisible();

        }

        private void AddGradeProcessStart()
        {
            //Poukrywanie innych kontrolek, sprawdzenie czy można wejść
            //i odpalenie metody z ListBoxami, która jest niżej
            if (DashH.Teacher == true)
            {
                MyPersonalDataHide();
                HomeCtrl.Visibility = Visibility.Hidden;
                Grades.Visibility = Visibility.Hidden;
                SBLBox.Visibility = Visibility.Hidden;
                SBtn.Visibility = Visibility.Hidden;
                TBlock2.Visibility = Visibility.Hidden;
                WATImg.Visibility = Visibility.Visible;
                clickCounter = 0;
                DashH.SelectedGrId = 0;
                DashH.SelectedStudId = 0;
                DashH.SelectedSubId = 0;
                MakeGradeAdderVisible();
            }
            else SignUp.ShowMessage("Tylko dla pracowników.");
        }

        private void LogOut()
        {
            //Ukrywanie kontrolek i powrót do ekranu logowania
            MyPersonalDataHide();
            HomeCtrl.Visibility = Visibility.Hidden;
            Grades.Visibility = Visibility.Hidden;
            TBlock2.Visibility = Visibility.Hidden;
            SBLBox.Visibility = Visibility.Hidden;
            SBtn.Visibility = Visibility.Hidden;
            WATImg.Visibility = Visibility.Visible;
            clickCounter = -2;
            MakeGradeAdderVisible();
            GUI.Exit exit = new GUI.Exit();
            exit.ShowDialog();
            if (exit.Exit_state == true)
            {
                LoginWindow mn = new LoginWindow();
                this.Close();
                mn.ShowDialog();
            }
        }

        private void MyPersonalData()
        {
            //Pokazanie labeli które pokazują dane użytkownika
            //Chowanie wszystkich innych kontrolek jak wszędzie
            //no i jeśli to student to jeszcze wyświetla grupe ofc
            HomeCtrl.Visibility = Visibility.Hidden;
            Grades.Visibility = Visibility.Hidden;
            TBlock2.Visibility = Visibility.Hidden;
            SBLBox.Visibility = Visibility.Hidden;
            SBtn.Visibility = Visibility.Hidden;
            WATImg.Visibility = Visibility.Visible;
            clickCounter = -2;
            MakeGradeAdderVisible();
            FNLabel.Content = DashH.User.FirstName;
            LNLabel.Content = DashH.User.LastName;
            EMLabel.Content = DashH.User.Email;
            NRLabel.Content = DashH.User.Id;
            FNLabel.Visibility = Visibility.Visible;
            LNLabel.Visibility = Visibility.Visible;
            EMLabel.Visibility = Visibility.Visible;
            NRLabel.Visibility = Visibility.Visible;
            FL.Visibility = Visibility.Visible;
            LL.Visibility = Visibility.Visible;
            EL.Visibility = Visibility.Visible;
            NRL.Visibility = Visibility.Visible;
            if (DashH.Student == true)
            {
                GLabel.Content = DashH.StudentGroup;
                GLabel.Visibility = Visibility.Visible;
                GL.Visibility = Visibility.Visible;
            }
            else
            {
                NRL.Content = "Id pracownika:";
                NRL.Margin = GL.Margin;
                NRLabel.Margin = GLabel.Margin;
            }
        }

        private void BackButtonClicker()
        {
            //Przechodzenie w tył po listBoxach
            //w procesie dodawania ocenki
            if (clickCounter == 1)
            {
                SLBox.Visibility = Visibility.Visible;
                GLBox.Visibility = Visibility.Hidden;
                STLBox.Visibility = Visibility.Hidden;
                clickCounter--;

            }
            else if (clickCounter == 2)
            {
                SLBox.Visibility = Visibility.Hidden;
                GLBox.Visibility = Visibility.Visible;
                STLBox.Visibility = Visibility.Hidden;
                clickCounter--;

            }
            else if (clickCounter == 3)
            {
                SLBox.Visibility = Visibility.Hidden;
                GLBox.Visibility = Visibility.Hidden;
                GRLBox.Visibility = Visibility.Hidden;
                STLBox.Visibility = Visibility.Visible;
                NBtn.Content = "Dalej";
                clickCounter--;
            }
        }

        private void MakeGradeAdderVisible()
        {
            //Odpalenie pierwszego listboxa oraz
            //Przycisków next/back, tylko jeśli clickCounter jest równy 0
            //Albowiem KlikKałnter jest równy 0 tylko jeśli klikniemy
            //na przycisk Dodaj ocenke
            if (clickCounter == 0)
            {
                SLBox.Visibility = Visibility.Visible;
                NBtn.Content = "Dalej";
                NBtn.Visibility = Visibility.Visible;
                BBtn.Visibility = Visibility.Visible;
                GLBox.Visibility = Visibility.Hidden;
                STLBox.Visibility = Visibility.Hidden;
                GRLBox.Visibility = Visibility.Hidden;
                TitleL.Content = AGBtn.Content;
            }
            else if (clickCounter == -2)
            {
                SLBox.Visibility = Visibility.Hidden;
                GLBox.Visibility = Visibility.Hidden;
                STLBox.Visibility = Visibility.Hidden;
                GRLBox.Visibility = Visibility.Hidden;
                NBtn.Visibility = Visibility.Hidden;
                BBtn.Visibility = Visibility.Hidden;
            }
        }
        
        private async void NextButtonClickerAsync()
        {
            List<Task<List<String>>> tasks = new List<Task<List<string>>>();
            if (clickCounter == 0)
            {
                if (SLBox.SelectedItem != null)
                {
                    //Przechodzenie z listy przedmiotów, które prowadzi zalogowany nauczyciel na listę grup
                    //które mają dany przedmiot
                    DashH.SelectedSubId = DashH.SubjectList.ElementAt(SLBox.SelectedIndex).SubId;
                    tasks.Add(Task.Run(() => DashH.DataLoadSecondStepAsync()));
                    var items = await Task.WhenAll(tasks);
                    GLBox.ItemsSource = items.ElementAt(0);
                    SLBox.Visibility = Visibility.Hidden;                    
                    GLBox.Visibility = Visibility.Visible;
                    clickCounter++;
                }

            }
            else if (clickCounter == 1)
            {
                if (GLBox.SelectedItem != null)
                {
                    //Przechodzenie z listy grup, które mają dany przedmiot
                    //na listę studentów podpiętych do tych grup
                    DashH.SelectedGrId = DashH.GroupList.ElementAt(GLBox.SelectedIndex).GroupId;
                    tasks.Add(Task.Run(() => DashH.DataLoadThirdStepAsync()));
                    var items = await Task.WhenAll(tasks);
                    STLBox.ItemsSource = items.ElementAt(0);
                    GLBox.Visibility = Visibility.Hidden;                   
                    STLBox.Visibility = Visibility.Visible;
                    clickCounter++;
                }

            }
            else if (clickCounter == 2)
            {
                if (STLBox.SelectedItem != null)
                {
                    //Przechodzenie z listy studentów na listę ocen
                    //które można wstawić studentom do dziennika
                    DashH.SelectedStudId = DashH.StudentList.ElementAt(STLBox.SelectedIndex).Id;
                    STLBox.Visibility = Visibility.Hidden;
                    GRLBox.ItemsSource = DashH.GradePick;
                    GRLBox.Visibility = Visibility.Visible;
                    NBtn.Content = "Dodaj";
                    clickCounter++;
                }
            }
            else if (clickCounter == 3)
            {
                //A tutaj już to co dzieje się po kliknięciu przycisku
                //"Dodaj"
                if (GRLBox.SelectedItem != null)
                {
                    float grade = (float)GRLBox.SelectedItem;
                    await Task.Run(() => DashH.AddGradeAsync(DashH.SelectedStudId, DashH.SelectedSubId, grade));
                    GRLBox.Visibility = Visibility.Hidden;
                    MyPersonalDataHide();
                    HomeCtrl.Visibility = Visibility.Visible;
                    Grades.Visibility = Visibility.Hidden;
                    TBlock2.Visibility = Visibility.Hidden;
                    WATImg.Visibility = Visibility.Hidden;
                    clickCounter = -2;
                    MakeGradeAdderVisible();
                    Info info = new Info();
                    info.Assign("Dodano ocenę " +
                        grade +
                        " studentowi: " +
                        "\n" + STLBox.SelectedItem);
                    info.ShowDialog();

                }
            }
        }

        private void MyPersonalDataHide()
        {
            //Ukrywanie labeli które pokazują się w "Moje dane"
            FNLabel.Visibility = Visibility.Hidden;
            LNLabel.Visibility = Visibility.Hidden;
            EMLabel.Visibility = Visibility.Hidden;
            GLabel.Visibility = Visibility.Hidden;
            NRLabel.Visibility = Visibility.Hidden;
            FL.Visibility = Visibility.Hidden;
            LL.Visibility = Visibility.Hidden;
            EL.Visibility = Visibility.Hidden;
            GL.Visibility = Visibility.Hidden;
            NRL.Visibility = Visibility.Hidden;
        }

        private void ShowAppInfo()
        {
            //sprawdza typ zalogowanego użytkownika
            //i jako taki bajer wyświetla to w info o apce

            clickCounter = -2;
            MyPersonalDataHide();
            MakeGradeAdderVisible();
            Grades.Visibility = Visibility.Hidden;
            HomeCtrl.Visibility = Visibility.Hidden;
            SBLBox.Visibility = Visibility.Hidden;
            SBtn.Visibility = Visibility.Hidden;
            WATImg.Visibility = Visibility.Visible;
            TBlock2.Text = ("System oceniania studentów WAT" +
                "\n\nAplikacja pozwala na logowanie" +
                " się z poziomu pracownika uczelni" +
                " lub studenta. Pracownik" +
                " może dodawać i zmieniać" +
                " oceny studentów którzy należą do grup, w których" +
                " wykładany jest przez tę osobę przedmiot. Studenci" +
                " mają wgląd do swoich ocen końcowych. " +               
                "\n\nProjekt został w pełni przygotowany przez" +
                " studentów grupy I7B1S1: " +
                "\nBartłomiej PIERÓG" +
                "\nBartosz OCIMEK" +
                "\n\n\nApp icon made by Freepik from flaticon.com");
            TBlock2.Visibility = Visibility.Visible;
        }

        private async void ShowMySubjectsAsync()
        {
            //Ukrywanie, pokazywanie etc
            List<Task<List<String>>> tasks = new List<Task<List<string>>>();
            tasks.Add(Task.Run(() => DashH.GetSubjectsAsync()));
            var items = await Task.WhenAll(tasks);
            SBLBox.ItemsSource = items.ElementAt(0);
            TitleL.Content = MSBtn.Content;
            SBtn.Content = "Pokaż dane";
            DashH.SelectedSubId = 0;           
            MyPersonalDataHide();
            HomeCtrl.Visibility = Visibility.Hidden;
            Grades.Visibility = Visibility.Hidden;
            TBlock2.Visibility = Visibility.Hidden;
            WATImg.Visibility = Visibility.Visible;
            clickCounter = -2;
            MakeGradeAdderVisible();
            SBLBox.Visibility = Visibility.Visible;
            if (DashH.Student == true)
            {
                SBtn.Visibility = Visibility.Visible;
            }
            infoClik = false;
            
        }
       
        private async void ShowTeacherInfoAsync()
        {
            //Pokazuje lejbelki o nauczycielu wykładającym dany przedmiot
            if (DashH.Student == true)
            {
                
                clickCounter = -2;
                TitleL.Content = "Dane prowadzącego";
                DashH.SelectedSubId = DashH.SubjectList2.ElementAt(SBLBox.SelectedIndex).SubId;
                List<Task<Model.NTeacher>> tasks = new List<Task<Model.NTeacher>>();
                tasks.Add(Task.Run(() => DashH.GetTeacherInfoAsync()));
                var teach = await Task.WhenAll(tasks);
                Model.NTeacher teacher = teach.ElementAt(0);
                FNLabel.Content = teacher.FirstName;
                LNLabel.Content = teacher.LastName;
                EMLabel.Content = teacher.Email;
                MyPersonalDataHide();
                HomeCtrl.Visibility = Visibility.Hidden;
                Grades.Visibility = Visibility.Hidden;
                TBlock2.Visibility = Visibility.Hidden;
                SBLBox.Visibility = Visibility.Hidden;
                WATImg.Visibility = Visibility.Visible;
                FNLabel.Visibility = Visibility.Visible;
                LNLabel.Visibility = Visibility.Visible;
                EMLabel.Visibility = Visibility.Visible;
                FL.Visibility = Visibility.Visible;
                LL.Visibility = Visibility.Visible;
                EL.Visibility = Visibility.Visible;
                infoClik = true;
                SBtn.Content = "Wróc";
            }            
        }

        private async void LoadDashboardAsync()
        {           
            List<Task<List<String>>> tasks = new List<Task<List<string>>>();
            tasks.Add(Task.Run(() => DashH.DataLoadFirstStepAsync()));
            var items = await Task.WhenAll(tasks);
            SLBox.ItemsSource = items.ElementAt(0);
            HomeCtrl.Visibility = Visibility.Visible;
        }

        
    }
}
