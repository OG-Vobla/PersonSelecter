using PersonSelecter.DB;
using PersonSelecter.Other;
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
using System.Windows.Shapes;

namespace PersonSelecter
{
    /// <summary>
    /// Логика взаимодействия для SkillChoice.xaml
    /// </summary>
    public partial class SkillChoice : Window
    {
        string defaultDescription;
        Person person;
        Skill ChoiceSkill;

        public SkillChoice(Person person)
        {
            InitializeComponent();
            defaultDescription = $"Чтобы узнать описание скилла \nнажмите на него один раз.\nЧтобы выбрать скилла\nнажмите на него 2 раза.";
            TextBlockDescription.Text = defaultDescription;
            this.person = person;
            UpdateSkills();
            
        }
        private void Skills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Skill skill = MongoDb.FindSkill(((sender as ListView).SelectedItem as Skill).SkillName);
            TextBlockDescription.Text = skill.GetDescription();
        }
        private void UpdateSkills()
        {
            int i = 0;
            List<Skill> skills = MongoDb.FindAllSkills();
            if(person.Skills.Count == 0)
            {
                foreach (var ski in skills)
                {
                    Skills.Items.Add(ski);
                }
            }
            else
            {
                foreach (var ski in skills)
                {
                    foreach(var sk in person.Skills)
                    {
                        if (sk.SkillName == ski.SkillName)
                        {
                            i = 1;
                            break;
                        }
                        i = 0;
                    }
                    if(i  == 0)
                    {
                        Skills.Items.Add(ski);
                    }

                }
                    
                
            }

        }

        private void Skills_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChoiceSkill = MongoDb.FindSkill(((sender as ListView).SelectedItem as Skill).SkillName);
            this.DialogResult = true;
            this.Close();
        }
        public Skill GetSkill()
        {
            return ChoiceSkill;
        }
    }
}
