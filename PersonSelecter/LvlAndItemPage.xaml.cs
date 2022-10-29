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
using PersonSelecter.PersonClass;
using PersonSelecter.DB;
using PersonSelecter.Other;

namespace PersonSelecter
{
    /// <summary>
    /// Логика взаимодействия для LvlAndItemPage.xaml
    /// </summary>
    public partial class LvlAndItemPage : Page
    {
        string defaultDescription;
        Person person;
        public LvlAndItemPage(Person person)
        {
            InitializeComponent();
            this.person = person;   
            foreach(var item in MongoDb.FindAllItems())
            {
                ItemStore.Items.Add(item);
            }
            defaultDescription = $"Чтобы добавить или убрать предмет \nв инвентаре, нажмите на него два раза. \nЧтобы узнать его описание \nнажмите на него один раз.";
            TextBlockDescription.Text = defaultDescription;
        }

        private void ItemStore_MouseDown(object sender, MouseButtonEventArgs e)
        {

            
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
            TextBlockDescription.Text = item.GetDescription();
        }

        private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item item = MongoDb.FindItem(((sender as ListView).SelectedItem as ItemCount)?.Item.ItemName);
            TextBlockDescription.Text = item?.GetDescription();
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
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void FurtherBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("После того как вы введёте имя, создание персонажа закончится.");
            NameGiveDWindow nameGiveDWindow = new NameGiveDWindow();

            if (nameGiveDWindow.ShowDialog() == true)
            {
                MessageBox.Show("Приятной игры!");
                person.Name = nameGiveDWindow.Name;
                MongoDb.AddPersonToDB(person);
                this.NavigationService.Navigate(new MenuPage());
            }
        }
    }
}
