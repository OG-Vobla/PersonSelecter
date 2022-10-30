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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonSelecter
{
    /// <summary>
    /// Логика взаимодействия для EditPersonPage.xaml
    /// </summary>
    public partial class EditPersonPage : Page
    {
        Person person;
        Person dataPerson;
        string defaultDescription;
        Item wornItem;
        public EditPersonPage()
        {
            InitializeComponent();
            foreach (var i in MongoDb.FindAllPersons())
            {
                PersonList.Items.Add(i.Name);
            }
            defaultDescription = $"Чтобы узнать описание скилла \nнажмите на него один раз.";
            TextBlockDescription.Text = defaultDescription;
            ItemStore.Items.Clear();
            foreach (var item in MongoDb.FindAllItems())
            {
                ItemStore.Items.Add(item);
            }

            defaultDescription = $"Чтобы добавить или убрать предмет \nв инвентаре, нажмите на него два раза. \nЧтобы узнать его описание \nнажмите на него один раз.";
            TextBlockDescriptionItem.Text = defaultDescription;
        }

        private void ItemStore_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            person.AddItem((sender as ListView).SelectedItem as Item);
            InventoryUpdate();


        }
        private void Inventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            person.DropItem((((sender as ListView).SelectedItem as ItemCount)).Item.ItemName);
            InventoryUpdate();
        }
        private void ItemStore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item item = MongoDb.FindItem(((sender as ListView).SelectedItem as Item).ItemName);
            TextBlockDescriptionItem.Text = item.GetDescription();
            
        }

        private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            wornItem = MongoDb.FindItem(((sender as ListView).SelectedItem as ItemCount)?.Item.ItemName);
            TextBlockDescriptionItem.Text = wornItem?.GetDescription();
        }
        private void InventoryUpdate()
        {
            Inventory.Items.Clear();
            if (person.Inventory != null)
            {
                foreach (var i in person.Inventory)
                {
                    Inventory.Items.Add(i);
                }
            }
        }
        private void PersonList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            person = MongoDb.FindPerson(PersonList.SelectedItem.ToString());
            CreateDataPerson();
            Skills.Items.Clear();
            foreach (var skill in person.Skills)
            {
                Skills.Items.Add(skill);
            }
            
            personName.Text = person.Name;
            TextBoxExp.Text = person.Expirience.ToString();
            TextBoxLvl.Text = person.LVL.ToString();
            ExpToUpRef();
            LvlBar.Value = person.Expirience;
            WornSwordName.Text = person.WornSword?.ItemName;
            WornHelmetName.Text = person.WornHelmet?.ItemName;
            WornBreastplateName.Text = person.WornBreastplate?.ItemName;
            SetDefaultPoints();
            InventoryUpdate();
            TextBlockPersonInfo.Text = person.PrintInfo();
            Points.Text = person.CharPoints.ToString();
        }
        public void ClickUp(object sender, EventArgs e)
        {
            ChangeCharacteristic(sender, +1);
        }

        public void ClickDown(object sender, EventArgs e)
        {
            ChangeCharacteristic(sender, -1);
        }
        public void ChangeCharacteristic(object sender, int e)
        {
            int a = 0;
            switch ((sender as Button).Name.Split('_')[0])
            {
                case "Strenght":
                    a = int.Parse(StrenghtPoints.Text) + e;
                    if (a >= dataPerson.minStrenght && a <= dataPerson.maxStrenght && int.Parse(Points.Text) - e >= 0)
                    {
                        StrenghtPoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                        //person.Strenght = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
                case "Intelligence":
                    a = int.Parse(IntelligencePoints.Text) + e;
                    if (a >= dataPerson.minIntelligence && a <= dataPerson.maxIntelligence && int.Parse(Points.Text) - e >= 0)
                    {
                        IntelligencePoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                       // person.Intelligence = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
                case "Constitution":
                    a = int.Parse(ConstitutionPoints.Text) + e;
                    if (a >= dataPerson.minConstitution && a <= dataPerson.maxConstitution && int.Parse(Points.Text) - e >= 0)
                    {
                        ConstitutionPoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                        //person.Constitution = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
                case "Dexterity":
                    a = int.Parse(DexterityPoints.Text) + e;
                    if (a >= dataPerson.minDexterity && a <= dataPerson.maxDexterity && int.Parse(Points.Text) - e >= 0)
                    {
                        DexterityPoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                        //person.Dexterity = a;
                        
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
            }

        }
        public void PointsChanged(object sender, EventArgs e)
        {
            if(Points.Text != "0")
            {
                person.Strenght = int.Parse(StrenghtPoints.Text);
                person.Dexterity = int.Parse(DexterityPoints.Text);
                person.Constitution = int.Parse(ConstitutionPoints.Text);
                person.Intelligence = int.Parse(IntelligencePoints.Text);
                person.FullCalc();
                TextBlockPersonInfo.Text = person.PrintInfo();
            }
            
        }
        private void Skills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Skill skill = MongoDb.FindSkill(((sender as ListView).SelectedItem as Skill).SkillName);
            TextBlockDescription.Text = skill.GetDescription();
        }
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dataPerson != null)
            {
                TextBlockCharInfo.Text = $"Сила необходима для нанесения \nфизического урона по врагам. \nТак же по показателю силы \nможно понять на сколько высокая \nгрузоподъёмность у персонажа.\n\n С каждым очком силы вы получаете:";
                foreach (var i in dataPerson.StrenghtCalcValue)
                {
                    TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
                }
            }

        }

        private void DexterityTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if(dataPerson != null)
            {
                TextBlockCharInfo.Text = $"Ловкость необходима для совершения манёвров.\nТак же по показателю ловкости \nможно понять на сколько высокая \nвероятность улонения у персонажа.\n\n С каждым очком ловкости \nвы получаете:";
                foreach (var i in dataPerson.DexterityCalcValue)
                {
                    TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
                }
            }

        }

        private void ConstitutionTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dataPerson != null)
            {
                TextBlockCharInfo.Text = $"Конституция тела определяет вес.\nТак же по показателю конституции \nможно понять на сколько высокая \nстойкость у персонажа.\n\n С каждым очком конституции \nвы получаете:";
                foreach (var i in dataPerson.ConstitutionCalcValue)
                {
                    TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
                }
            }

        }

        private void IntelligenceTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dataPerson != null)
            {
                TextBlockCharInfo.Text = $"Интелект необходим для нанесения \nмагического урона по врагам. \nТак же по показателю интелекта \nможно понять на сколько много \nманы у персонажа.\n\n С каждым очком силы вы получаете:";
                foreach (var i in dataPerson.IntelligenceCalcValue)
                {
                    TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
                }
            }

        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlockCharInfo.Text = "Для того чтобы узнать всю \nнеобходимую информацию о\nхарактеристики наведитесь на неё.";
        }
        private void CreateDataPerson()
        {
            string nameClass = person.GetType().Name;
            switch (nameClass)
            {
                case "Rogue":
                    dataPerson = new Rogue("1");
                    break;
                case "Wizard":
                    dataPerson = new Wizard("1");
                    break;
                case "Warrior":
                    dataPerson = new Warrior("1");
                    break;
            }
        }
        public void SetDefaultPoints()
        {
            StrenghtPoints.Text = $"{person.Strenght}";
            IntelligencePoints.Text = $"{person.Intelligence}";
            ConstitutionPoints.Text = $"{person.Constitution}";
            DexterityPoints.Text = $"{person.Dexterity}";
        }

        private void Exp_1000_Click(object sender, RoutedEventArgs e)
        {
            ExpUp(1000);
        }
        private void ExpToUpRef()
        {
            int ExpToUp = 0;
            for(int i = 0; i <= Int32.Parse(TextBoxLvl.Text); i++)
            {
                ExpToUp += i * 1000;
            }
            LvlBar.Maximum = ExpToUp;
            TextBoxExpToUp.Text = ExpToUp.ToString();
        }
        private void ExpUp(int value)
        {
            //TextBoxExp.Text = (Int32.Parse(TextBoxExp.Text) + value).ToString();
            if(Int32.Parse(TextBoxExp.Text)+ value >= Int32.Parse(TextBoxExpToUp.Text))
            {
                TextBoxExp.Text = (Int32.Parse(TextBoxExp.Text) + value - Int32.Parse(TextBoxExpToUp.Text)).ToString();
                TextBoxLvl.Text = (Int32.Parse(TextBoxLvl.Text) + 1).ToString();

                ExpToUpRef();
            }
            else
            {
                TextBoxExp.Text = (Int32.Parse(TextBoxExp.Text)+ value).ToString();
            }
            LvlBar.Value = Int32.Parse(TextBoxExp.Text);
            person.LVL = Int32.Parse(TextBoxLvl.Text);
            person.Expirience = Int32.Parse(TextBoxExp.Text);
        }

        private void Exp_100_Click(object sender, RoutedEventArgs e)
        {
            ExpUp(100);
        }

        private void TextBoxLvl_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if(TextBoxLvl.Text != "0" && Int32.Parse(TextBoxLvl.Text) > person.LVL)
            {
                Points.Text = (Int32.Parse(Points.Text)+5).ToString();
                if (Int32.Parse(TextBoxLvl.Text) % 3 == 0 && Int32.Parse(TextBoxLvl.Text) > person.LVL)
                {
                    SkillChoice skillChoice = new SkillChoice(person);

                    if (skillChoice.ShowDialog() == true)
                    {
                        person.AddSkill(skillChoice.GetSkill());
                        Skills.Items.Clear();
                        foreach(var ski in person.Skills)
                        {
                            Skills.Items.Add(ski);
                        }
                        
                    }
                }
            }

        }

        private void ItemStore_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void WornItem_Click(object sender, RoutedEventArgs e)
        {
            Item item =wornItem;
            if (item == null)
            {
                MessageBox.Show("Чтобы надеть предмет необходимо сначала выбрать этот предмет в инвентарею");
            }
            else if(!person.WornItem(item))
            {
                MessageBox.Show("Вы не можете надеть этот предмет из-за нехватки характеристик");
            }
            else
            {
                person.WornItem(item);
                switch (item.Type)
                {
                    case "Helmet":
                        WornHelmetName.Text = item.ItemName;
                        break;
                    case "Sword":
                        WornSwordName.Text = item.ItemName;
                        break;
                    case "Breastplate":
                        WornBreastplateName.Text = item.ItemName;
                        break;
                }
            }

        }

        private void TakeOfItemItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = wornItem;
            if (item == null)
            {
                MessageBox.Show("Чтобы снять предмет необходимо сначала выбрать этот предмет в инвентарею");
            }
            else
            {
                
                switch (item.Type)
                {
                    case "Helmet":
                        person.TakeOffItem("Helmet");
                        WornHelmetName.Text = "";
                        break;
                    case "Sword":
                        person.TakeOffItem("Sword");
                        WornSwordName.Text ="";
                        break;
                    case "Breastplate":
                        person.TakeOffItem("Breastplate");
                        WornBreastplateName.Text ="";
                        break;
                }
            }
        }

        private void TakeOfSword_Click(object sender, RoutedEventArgs e)
        {
            if (WornSwordName.Name == "")
            {
                MessageBox.Show("Нет надетого оружия");
            }
            else
            {
                person.TakeOffItem("Sword");
                WornSwordName.Text = "";

                
            }
        }

        private void TakeOfBreastplate_Click(object sender, RoutedEventArgs e)
        {
            if (WornSwordName.Name == "")
            {
                MessageBox.Show("Нет надетого нагрудника");
            }
            else
            {
                person.TakeOffItem("Breastplate");
                WornBreastplateName.Text = "";


            }
        }

        private void TakeOfHelmet_Click(object sender, RoutedEventArgs e)
        {
            if (WornSwordName.Name == "")
            {
                MessageBox.Show("Нет надетого шлема");
            }
            else
            {
                person.TakeOffItem("Helmet");
                WornHelmetName.Text = "";


            }
        }

        private void SavePerson_Click(object sender, RoutedEventArgs e)
        {
            string lastName = person.Name;
            person.CharPoints = Int32.Parse(Points.Text);
            person.Name = personName.Text;
            MongoDb.ReplacePerson(lastName, person);
            
            PersonList.Items.Clear();
            foreach (var i in MongoDb.FindAllPersons())
            {
                PersonList.Items.Add(i.Name);
            }
            this.NavigationService.GoBack();
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
