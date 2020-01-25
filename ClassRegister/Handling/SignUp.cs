using NHibernate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassRegister
{
    public class SignUp
    {

        public static bool Student { get; private set; }

        public static bool Teacher { get; private set; }

        private static Model.NUserM UsernameContain(string username)
        {
            //Metoda sprawdzająca czy w bazie danych znajduje się użytkownik o takiej nazwie
            SessionFactory.Configuration();  
            using (SessionFactory.session)
            {
                IList<Model.NStudent> list = 
                    SessionFactory.session
                    .QueryOver<Model.NStudent>()
                    .Where(x=>x.Username==username)
                    .List();
                IList<Model.NTeacher> tlist =
                    SessionFactory.session
                    .QueryOver<Model.NTeacher>()
                    .Where(x => x.Username == username)
                    .List();

                if (list.Count > 0)
                {
                    Student = true;
                }
                else Student = false;

                if (tlist.Count > 0)
                {
                    Teacher = true;
                }
                else Teacher = false;

                if (Student == true)
                {
                    return (Model.NUserM)list.ElementAt(0);
                }
                else if (Teacher == true)
                {
                    return (Model.NUserM)tlist.ElementAt(0);
                }
                else return null;
            }
            
        }
        public static Model.NUserM USignUp(string username, string password)
        { 
            //metoda odpowiadająca za powodzenie/niepowodzenie logowania
                if (UsernameContain(username) != null)
                {
                    if (password == UsernameContain(username).Password)
                    {                    
                    return UsernameContain(username);
                    }
                    else return null;
                }
                else return null; 
        }

        public static void JoinUs(string fn, string ln, string username,  
                                    string em, string idd, string pswrd, string rpswrd)
        {
            //JoinUs to metoda która odpowiada za przebieg rejestracji,
            //W BD w tabeli Students podani są uczniowie, każdy ma imię, nazwisko, klasę, oraz unikalne Id
            //W procesie rejestracji, aby przebiegła pomyślnie uczeń musi podać swoje nazwisko i przypisane do niego Id
            //a następnie podaje swój adres e-mail, dowolną nazwę użytkownika (niewykorzystaną wcześniej) oraz hasło 2-krotnie.
            bool state_t = false;
            bool state_u = false;
            SessionFactory.Configuration();
            Regex Id = new Regex("\\d{1,}");
            using (SessionFactory.session)
            {
                if (Id.IsMatch(idd))
                {
                    int id = int.Parse(idd);
                    IList<Model.NTeacher> tlist =
                           SessionFactory
                           .session
                           .QueryOver<Model.NTeacher>()
                           .Where(c => c.Id == id)
                           .List();
                    IList<Model.NStudent> list =
                       SessionFactory
                       .session
                       .QueryOver<Model.NStudent>()
                       .Where(c => c.Id == id)
                       .List();

                    if (tlist.Count() > 0 &&
                         tlist.ElementAt(0).Email == em)
                    {
                        state_t = true;
                    }
                    else if (list.Count()>0 &&
                         list.ElementAt(0).Email == em)      
                    {
                        state_u = true;
                    }
                    
                    if (state_t == true || state_u == true)
                    {
                        if (pswrd == rpswrd)
                        {
                            if (state_u)
                            {
                                Model.NStudent user = new Model.NStudent();
                                user.FirstName = list.ElementAt(0).FirstName;
                                user.LastName = list.ElementAt(0).LastName;
                                user.Id = list.ElementAt(0).Id;
                                user.GroupId = list.ElementAt(0).GroupId;
                                user.Username = username;
                                user.Email = list.ElementAt(0).Email;
                                user.Password = pswrd;
                                if (UsernameContain(username) != null)
                                {
                                    ShowMessage("Nazwa użytkownika jest zajęta.");
                                }
                                else
                                {
                                    SessionFactory.Configuration();
                                    SessionFactory.session.BeginTransaction();
                                    SessionFactory.session.SaveOrUpdate(user);
                                    SessionFactory.session.Transaction.Commit();
                                    ShowMessage("Zarejestrowany pomyślnie.");
                                }
                            }
                            else if (state_t)
                            {
                                Model.NTeacher user = new Model.NTeacher();
                                user.FirstName = tlist.ElementAt(0).FirstName;
                                user.LastName = tlist.ElementAt(0).LastName;
                                user.Id = tlist.ElementAt(0).Id;
                                user.Username = username;
                                user.Email = tlist.ElementAt(0).Email;
                                user.Password = pswrd;
                                if (UsernameContain(username) != null)
                                {
                                    ShowMessage("Nazwa użytkownika jest zajęta.");
                                }
                                else
                                {
                                    SessionFactory.Configuration();
                                    SessionFactory.session.BeginTransaction();
                                    SessionFactory.session.SaveOrUpdate(user);
                                    SessionFactory.session.Transaction.Commit();
                                    ShowMessage("Zarejestrowany pomyślnie.");
                                }
                            }
                            else
                            {
                                ShowMessage("Niepowodzenie.");
                            }

                        }
                    }

                    else
                    {
                        ShowMessage("Niepowodzenie.");
                    }

                }
                else
                {
                    ShowMessage("Niepowodzenie.");
                }                
            }                    
        }
        public static void ShowMessage(string text)
        {
            Info info = new Info();
            info.Assign(text);
            info.ShowDialog();
        } 
    }
}
