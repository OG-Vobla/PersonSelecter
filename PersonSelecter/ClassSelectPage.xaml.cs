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
    /// Логика взаимодействия для ClassSelectPage.xaml
    /// </summary>
    public partial class ClassSelectPage : Page
    {
        public ClassSelectPage()
        {
            InitializeComponent();
        }
        private void RogueCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            Canvas.SetZIndex(RoguePng, 1);
            Canvas.SetZIndex(RogueRectangle, 0);
            Canvas.SetZIndex(RogueTextBlock, 0);
        }

        private void WizardCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            Canvas.SetZIndex(WizardPng, 1);
            Canvas.SetZIndex(WizardRectangle, 0);
            Canvas.SetZIndex(WizardTextBlock, 0);
        }

        private void WizardCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas.SetZIndex(WizardPng, 0);
            Canvas.SetZIndex(WizardRectangle, 1);
            Canvas.SetZIndex(WizardTextBlock, 1);
        }

        private void WarriorCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas.SetZIndex(WarriorPng, 0);
            Canvas.SetZIndex(WarriorRectangle, 1);
            Canvas.SetZIndex(WarriorTextBlock, 1);
        }

        private void WarriorCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            Canvas.SetZIndex(WarriorPng, 1);
            Canvas.SetZIndex(WarriorRectangle, 0);
            Canvas.SetZIndex(WarriorTextBlock, 0);
        }

        private void RogueCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas.SetZIndex(RoguePng, 0);
            Canvas.SetZIndex(RogueRectangle, 1);
            Canvas.SetZIndex(RogueTextBlock, 1);
        }

        private void WizardCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new SpecSelectPage('Z'));
        }
        
        private void RogueCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new SpecSelectPage('R'));
        }

        private void WarriorCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new SpecSelectPage('W'));
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
