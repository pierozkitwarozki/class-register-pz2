using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassRegister.Handling
{
    public class DashboardHandle
    {
        public Model.NUserM User { get; private set; } //zalogowany użytkownik
        public bool Student { get; private set; } = false; //uprawnienia
        public bool Teacher { get; private set; } = false; //uprawnienia
        public int SelectedSubId { get; set; } //Id wybranego przedmiotu
        public int SelectedGrId { get; set;} //Id wybranej grupy
        public int SelectedStudId { get; set; } //Id wybranego studenta
        public IList<Model.NSubject> SubjectList { get; private set; } //lista przedmiotów do wyboru 
        public List<Model.NSubject> SubjectList2 { get; private set; } = new List<Model.NSubject>();//lista przedmiotów do wyboru 
        public IList<Model.NStudent> StudentList { get; private set; } //lista studentów do wyboru
        public List<Model.NGroup> GroupList { get; private set; } //lista grup do wyboru    
        public List<float> GradePick { get; private set; }  //lista ocen do wyboru    
        public string StudentGroup { get; private set; } //ewentualna grupa studenta;
        public int StudentGroupId { get; private set; } //id tego wyżej
        public string UserFunction { get; private set; } //czy jest pracownikiem czy uczniem, do napisu
        private IList<Model.NGroup> tempList;

        public DashboardHandle()
        {
            User = LoginWindow.LUser; //przekazanie zalogowanego użytkownika
            if (SignUp.Student == true)
            {
                this.Student = true;
                this.grAttach();
                this.UserFunction = "Student";

            }else if (SignUp.Teacher == true)
            {
                this.Teacher = true;
                this.UserFunction = "Pracownik";
            }
            GradePick = new List<float>();
            GradePick.Add(2);
            GradePick.Add(3);
            GradePick.Add((float)3.5);
            GradePick.Add(4);
            GradePick.Add((float)4.5);
            GradePick.Add(5);
            SessionFactory.Configuration();
            using (SessionFactory.session)
            {
                tempList = SessionFactory.session
                        .QueryOver<Model.NGroup>().List();               
            }           
        }

        public List<string> t1DataLoad()
        {
            //Metoda wczytująca przedmioty, których uczy zalogowany pracownik
            //Zwracająca listę z nazwami tych przedmiotów, 
            //aby mogły być potem wyświetlane w listBoxie
                SessionFactory.Configuration();
                using (SessionFactory.session)
                {
                List<string> subNamesList = new List<string>();
                SubjectList = SessionFactory
                        .session
                        .QueryOver<Model.NSubject>()
                        .Where(x => x.TeacherId == this.User.Id)
                        .List();
                    foreach (var x in SubjectList)
                    {
                        subNamesList.Add(x.SubName);
                    }
                return subNamesList;
                }

        }
        public List<string> t2DataLoad()
        {
            //Metoda wczytująca grupy w których wykładany jest wybrany wcześniej przedmiot
            //i zwracająca listę z nazwami tych grup, analogicznie jak w t1DataLoad
            SessionFactory.Configuration();
            using (SessionFactory.session)
            {
                    IList<Model.NSubjectGroupAttach> grList;
                    List<string> grNamesList = new List<string>();
                    grList = SessionFactory
                    .session
                    .QueryOver<Model.NSubjectGroupAttach>()
                    .Where(x => x.SubId == SelectedSubId)
                    .List();
                GroupList = new List<Model.NGroup>();
                for (int i = 0; i < grList.Count; i++)
                {
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        if (grList.ElementAt(i).GroupId == tempList.ElementAt(j).GroupId)
                        {
                            GroupList.Add(tempList.ElementAt(j));
                            grNamesList.Add(tempList.ElementAt(j).GroupName);
                        }
                    }
                }
                return grNamesList;
            }

        }


        public List<string> t3DataLoad()
        {
            //Metoda wczytująca studentów z wybranej grupy i analogicznie
            //zwracająca listę z ich imionami + nazwiskami 
                List<string> stNamesList = new List<string>();
                string FLName;
                SessionFactory.Configuration();
                using (SessionFactory.session)
                {
                    StudentList = SessionFactory.
                        session
                        .QueryOver<Model.NStudent>()
                        .Where(x => x.GroupId == SelectedGrId)
                        .List();
                    foreach (var x in StudentList)
                    {
                        FLName = (x.FirstName + " " + x.LastName);
                        stNamesList.Add(FLName);
                    }
                return stNamesList;
                }
        }

        public void dataLoad(DataGrid grid)
        {
            //Metoda wczytująca oceny zalogowanego studenta
            //I przypisująca je wraz z nazwą przedmiotu do 
            //podanego jako parametr DataGrida
                int subId;
                SessionFactory.Configuration();
                using (SessionFactory.session)
                {
                    IList<Model.NGrades> query1 = SessionFactory.session
                        .QueryOver<Model.NGrades>()
                        .Where(x => x.StudentId == this.User.Id)
                        .List();
                    IList<Model.NSubject> query2 = SessionFactory.session
                        .QueryOver<Model.NSubject>().List();
                    List<Model.NStudentGrades> gradeList = new List<Model.NStudentGrades>();
                    for (int i = 0; i < query1.Count; i++)
                    {
                        subId = query1.ElementAt(i).SubId;
                        for (int j = 0; j < query2.Count; j++)
                        {
                            if (query2.ElementAt(j).SubId == subId)
                            {
                                gradeList.Add(
                                    new Model.NStudentGrades
                                    (query2.ElementAt(j).SubName, query1.ElementAt(i).Grade));
                            }
                        }
                    }
                    grid.ItemsSource = gradeList;
                }           
        }

        public void addGrade(int studentId, int subjectId, float grade) //Metoda dodawania ocen
        {
            SessionFactory.Configuration();
            using (SessionFactory.session)
            {
                //Lista jednoelementowa która (nie) zawiera ocenę danego
                //ucznia, z danego przedmiotu
                IList<Model.NGrades> takenGrade =
                    SessionFactory.session
                    .QueryOver<Model.NGrades>()
                    .Where(x => x.StudentId == studentId && x.SubId == subjectId)
                    .List();
                SessionFactory.Configuration();
                SessionFactory.session.BeginTransaction();
                if (takenGrade.Count != 0)
                {
                    //Poprawa oceny danego ucznia z danego przedmiotu na inną
                    Model.NGrades Grade = new Model.NGrades();
                    Grade.Id = takenGrade.ElementAt(0).Id;
                    Grade.StudentId = takenGrade.ElementAt(0).StudentId;
                    Grade.SubId = takenGrade.ElementAt(0).SubId;
                    Grade.Grade = grade;
                    SessionFactory.session.SaveOrUpdate(Grade);
                }
                else
                {
                    //Zapisywanie nowej oceny jeśli takiej nie było wgl
                    Model.NGrades Grade = new Model.NGrades();
                    Grade.Grade = grade;
                    Grade.StudentId = studentId;
                    Grade.SubId = subjectId;
                    SessionFactory.session.Save(Grade);

                }
                SessionFactory.session.Transaction.Commit();
            }
        }

        public List<string> getSubjectInfo()
        {
            //Metoda zwraca litę z nazwami przedmiotów na które uczęszcza student,
            //lub które prowadzi pracownik, w zależności od tego kto jest zalogowany
            SessionFactory.Configuration();
            using (SessionFactory.session)
            {
                List<string> subjectNames = new List<string>();
                if (this.Student == true)
                {
                    IList<Model.NSubjectGroupAttach> query1 =
                        SessionFactory.session
                        .QueryOver<Model.NSubjectGroupAttach>()
                        .Where(x => x.GroupId == this.StudentGroupId)
                        .List();                   
                    IList<Model.NSubject> query2 =
                        SessionFactory.session
                        .QueryOver<Model.NSubject>()
                        .List();
                    for(int i=0; i<query1.Count(); i++)
                    {
                        for(int j=0; j < query2.Count(); j++)
                        {
                            if (query1.ElementAt(i).SubId == query2.ElementAt(j).SubId)
                            {
                                SubjectList2.Add(query2.ElementAt(j));
                                subjectNames.Add(query2.ElementAt(j).SubName);
                            }
                        }
                    }
                    
                }
                else if(this.Teacher == true)
                {
                    IList<Model.NSubject> query1 =
                        SessionFactory.session
                        .QueryOver<Model.NSubject>()
                        .Where(x => x.TeacherId == User.Id)
                        .List();
                    foreach(var x in query1)
                    {
                        SubjectList.Add(x); 
                        subjectNames.Add(x.SubName);
                    }
                }
                return subjectNames;               
            }

        }

        public Model.NTeacher getTeacherInfo()
        {
            //Metoda zwraca nauczyciela który prowadzi wybrany przedmiot
            SessionFactory.Configuration();
            using (SessionFactory.session)
            {
                    IList<Model.NSubject> query1 =
                    SessionFactory.session
                    .QueryOver<Model.NSubject>()
                    .Where(x => x.SubId == SelectedSubId)
                    .List();
                
                    IList<Model.NTeacher> query2 =
                        SessionFactory.session
                        .QueryOver<Model.NTeacher>()
                        .Where(x => x.Id == query1.ElementAt(0).TeacherId)
                        .List();

                    return query2.ElementAt(0);             
            }
        }

        private void grAttach()
        {
            //Jeśli użytkownik jest studentem, to znajduję grupę w której się znajduje 
            //i przypisuje ją do właściwości StudentGroup, w celu jej późniejszego wyświetlenia
            //w Dashboardzie, same with Id
            SessionFactory.Configuration();
            using (SessionFactory.session)
            {
                IList<Model.NStudent> list =
                    SessionFactory.session
                    .QueryOver<Model.NStudent>()
                    .Where(x => x.Id == this.User.Id)
                    .List();
                int grId = list.ElementAt(0).GroupId;
                IList<Model.NGroup> list2 =
                    SessionFactory.session
                    .QueryOver<Model.NGroup>()
                    .Where(x => x.GroupId == grId)
                    .List();
                this.StudentGroup = list2.ElementAt(0).GroupName;
                this.StudentGroupId = list2.ElementAt(0).GroupId;
            }
        }
        
    }
}
