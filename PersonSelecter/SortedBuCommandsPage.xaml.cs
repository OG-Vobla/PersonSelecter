using PersonSelecter.DB;
using PersonSelecter.PersonClass;
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

namespace PersonSelecter
{
    /// <summary>
    /// Логика взаимодействия для SortedBuCommandsPage.xaml
    /// </summary>
    public partial class SortedBuCommandsPage : Page
    {
        private List<Person> FirstTeam;
        private List<Person> SecondTeam;
        private List<Person> Persons;
        public SortedBuCommandsPage()
        {
            InitializeComponent();

            /*            foreach (var i in MongoDb.FindAllPersons())
                        {
                            PersonListFirstTeam.Items.Add(i);
                        }*/
        }

        private void CreateCommands_Click(object sender, RoutedEventArgs e)
        {
            CreateTeams();
            foreach (var i in FirstTeam)
            {
                PersonListFirstTeam.Items.Add(i);
            }
            foreach (var i in SecondTeam)
            {
                PersonListSecondTeam.Items.Add(i);
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void CreateTeams()
        {
            PersonListFirstTeam.Items.Clear();
            PersonListSecondTeam.Items.Clear();
            FirstTeam = new List<Person>();
            SecondTeam = new List<Person>();
            Persons = new List<Person>();
            Random random = new Random();
            int LvlTeam = random.Next(1, 20);
            foreach (var i in MongoDb.FindAllPersons())
            {
                if(i.LVL * 100 >= LvlTeam * 100 - 10*(LvlTeam) && i.LVL * 100 <= LvlTeam*100 + 10 * (LvlTeam) )
                {
                    Persons.Add(i);
                }
            }
            if(Persons.Count <= 1)
            {
                CreateTeams();
                return;
            }
            if(Persons.Count % 2 != 0)
            {
                Persons.Remove(Persons[Persons.Count-1]);
            }
            int a = Persons.Count / 2;
            for (int i = 0; i < a; i++)
            {
                int index = random.Next(0, Persons.Count - 1);
                FirstTeam.Add(Persons[index]);
                Persons.Remove(Persons[index]);
            }
            for (int i = 0; i < a; i++)
            {
                int index = random.Next(0, Persons.Count - 1);
                SecondTeam.Add(Persons[index]);
                Persons.Remove(Persons[index]);
            }
        }
    }
}
