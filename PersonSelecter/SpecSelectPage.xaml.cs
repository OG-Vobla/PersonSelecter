using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using PersonSelecter.PersonClass;

namespace PersonSelecter
{
    /// <summary>
    /// Логика взаимодействия для SpecSelectPage.xaml
    /// </summary>
    public partial class SpecSelectPage : Page
    {
        Person dataPerson;
        int MaxPoints = 200;
        public SpecSelectPage(char nameClass)
        {
            InitializeComponent();
            switch(nameClass)
            {
                case 'R':
                    Canvas.SetZIndex(RoguePng, 1);
                    dataPerson = new Rogue("1");
                    break;
                case 'Z':
                    Canvas.SetZIndex(WizardPng, 1);
                    dataPerson = new Wizard("1");
                    break;
                case 'W':
                    Canvas.SetZIndex(WarriorPng, 1);
                    dataPerson = new Warrior("1");
                    break;
            }
            SetDefaultPoints();
            Points.Text = MaxPoints.ToString();
            
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
                        //dataPerson.Strenght = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
                case "Intelligence":
                    a = int.Parse(IntelligencePoints.Text) + e;
                    if (a >= dataPerson.minIntelligence && a <= dataPerson.maxIntelligence && int.Parse(Points.Text) - e >= 0)
                    {
                        IntelligencePoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                        //dataPerson.Intelligence = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
                case "Constitution":
                    a = int.Parse(ConstitutionPoints.Text) + e;
                    if (a >= dataPerson.minConstitution && a <= dataPerson.maxConstitution && int.Parse(Points.Text) - e >= 0)
                    {
                        ConstitutionPoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                        //dataPerson.Constitution = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
                case "Dexterity":
                    a = int.Parse(DexterityPoints.Text) + e;
                    if (a >= dataPerson.minDexterity && a <= dataPerson.maxDexterity && int.Parse(Points.Text) - e >= 0)
                    {
                        DexterityPoints.Text = $"{a}";
                        Points.Text = (int.Parse(Points.Text) - e).ToString();
                        //dataPerson.Dexterity = a;
                    }
                    else { MessageBox.Show("Вы перешли черту. Вы дорого заплатите за это...."); }
                    break;
            }
/*            dataPerson.CalculationDexterity();
            dataPerson.CalculationIntelligence();
            dataPerson.CalculationConstitution();
            dataPerson.CalculationDexterity();*/
            TextBlockPersonInfo.Text = dataPerson.PrintInfo();
        }
        public void SetDefaultPoints()
        {
            StrenghtPoints.Text = $"{dataPerson.minStrenght}";
            IntelligencePoints.Text = $"{dataPerson.minIntelligence}";
            ConstitutionPoints.Text = $"{dataPerson.minConstitution}";
            DexterityPoints.Text = $"{dataPerson.minDexterity}";
        }
        public void CheckPoints(int a)
        {
            
        }
        public void PointsChanged(object sender, EventArgs e)
        {
            if (Points.Text != "0")
            {
                dataPerson.Strenght = int.Parse(StrenghtPoints.Text);
                dataPerson.Dexterity = int.Parse(DexterityPoints.Text);
                dataPerson.Constitution = int.Parse(ConstitutionPoints.Text);
                dataPerson.Intelligence = int.Parse(IntelligencePoints.Text);
                dataPerson.FullCalc();
                TextBlockPersonInfo.Text = dataPerson.PrintInfo();
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlockCharInfo.Text =  $"Сила необходима для нанесения \nфизического урона по врагам. \nТак же по показателю силы \nможно понять на сколько высокая \nгрузоподъёмность у персонажа.\n\n С каждым очком силы вы получаете:";
            foreach(var i in dataPerson.StrenghtCalcValue)
            {
                TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
            }
        }

        private void DexterityTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlockCharInfo.Text = $"Ловкость необходима для совершения манёвров.\nТак же по показателю ловкости \nможно понять на сколько высокая \nвероятность улонения у персонажа.\n\n С каждым очком ловкости \nвы получаете:";
            foreach (var i in dataPerson.DexterityCalcValue)
            {
                TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
            }
        }

        private void ConstitutionTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlockCharInfo.Text = $"Конституция тела определяет вес.\nТак же по показателю конституции \nможно понять на сколько высокая \nстойкость у персонажа.\n\n С каждым очком конституции \nвы получаете:";
            foreach (var i in dataPerson.ConstitutionCalcValue)
            {
                TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
            }
        }

        private void IntelligenceTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlockCharInfo.Text = $"Интелект необходим для нанесения \nмагического урона по врагам. \nТак же по показателю интелекта \nможно понять на сколько много \nманы у персонажа.\n\n С каждым очком силы вы получаете:";
            foreach (var i in dataPerson.IntelligenceCalcValue)
            {
                TextBlockCharInfo.Text += ($"\n{i.Key} +{i.Value}");
            }
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlockCharInfo.Text = "Для того чтобы узнать всю \nнеобходимую информацию о\nхарактеристики наведитесь на неё.";
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void FurtherBtn_Click(object sender, RoutedEventArgs e)
        {
            dataPerson.Strenght = int.Parse(StrenghtPoints.Text);
            dataPerson.Dexterity = int.Parse(DexterityPoints.Text); 
            dataPerson.Constitution = int.Parse(ConstitutionPoints.Text);
            dataPerson.Intelligence = int.Parse(IntelligencePoints.Text);
            dataPerson.CharPoints = int.Parse(Points.Text);
            this.NavigationService.Navigate(new LvlAndItemPage( dataPerson));
        }


    }
}
