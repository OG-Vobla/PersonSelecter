using PersonSelecter.DB;
using PersonSelecter.Other;
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
    /// Логика взаимодействия для ItemSkillCreate.xaml
    /// </summary>
    public partial class ItemSkillCreate : Page
    {

        public ItemSkillCreate()
        {
            InitializeComponent();
            foreach (var i in MongoDb.FindAllBuffs())
            {
                Buffs.Items.Add(i);
            }
        }

        private void FurtherBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Buff> buffs = new List<Buff>();    
            foreach(var i in ChoiceBuff.Items)
            {
                buffs.Add(MongoDb.FindBuff(i.ToString()));
            }
            if (MongoDb.FindSkill(Name.Text) != null)
            {
                Skill skill = MongoDb.FindSkill(Name.Text);
                skill.Buffs = buffs;
                MongoDb.ReplaceSkill(Name.Text, skill);
            }
            MongoDb.AddSkillToDB(new Skill(Name.Text, buffs, Description.Text));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Buff> buffs = new List<Buff>();
            foreach (var i in ChoiceBuff.Items)
            {
                buffs.Add(MongoDb.FindBuff(i.ToString()));
            }
            if (MongoDb.FindItem(Name.Text) != null)
            {
                Item item = MongoDb.FindItem(Name.Text);
                item.Buffs = buffs;
                MongoDb.ReplaceItem(Name.Text, item);
            }
            else
            {
                MongoDb.AddItemToDB(new Item(Name.Text, buffs, Type.Text, Description.Text));
            }
            
        }

        private void Buff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChoiceBuff.Items.Add(((sender as ListView).SelectedItem as Buff));
        }

        private void ChoiceBuff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChoiceBuff.Items.Remove(((sender as ListView).SelectedItem as Buff));
        }
        private void BackBtn1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
