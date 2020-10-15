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

namespace Prb.LussenTop10.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        const int MaxItems = 10;
        string[] catNames;
        List<string>[] catContent;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;
            catNames = new string[6];
            catContent = new List<string>[6];
            CreateCategories();
            InitiateLists();
            populateCombobox();
        }
        private void CreateCategories()
        {
            catNames[0] = "Auteurs";
            catNames[1] = "Films";
            catNames[2] = "Restaurants";
            catNames[3] = "Games";
            catNames[4] = "TV-series";
            catNames[5] = "Steden";
        }
        private void InitiateLists()
        {
            for(int r = 0; r < 6; r++)
            {
                catContent[r] = new List<string>();
            }
        }
        private void populateCombobox()
        {
            cmbCategory.ItemsSource = catNames;
            cmbCategory.SelectedIndex = 0;
        }
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;

            int position = cmbCategory.SelectedIndex;
            lstTop10s.ItemsSource = null;
            lstTop10s.ItemsSource = catContent[position];

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;

            int position = cmbCategory.SelectedIndex;
            if(catContent[position].Count >= MaxItems)
            {
                lblError.Content = $"Er zijn reeds {MaxItems} items in categorie {cmbCategory.SelectedItem.ToString()} aanwezig!";
                lblError.Visibility = Visibility.Visible;
            }
            else
            {
                string newItem = txtNewItem.Text.Trim();
                if(newItem.Length == 0)
                {
                    lblError.Content = $"Je dient een waarde in te voeren !";
                    lblError.Visibility = Visibility.Visible;
                }
                else
                {

                    catContent[position].Add(newItem);
                    txtNewItem.Text = "";
                    txtNewItem.Focus();
                    lstTop10s.ItemsSource = null;
                    lstTop10s.ItemsSource = catContent[position];
                }
            }

        }



        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if(lstTop10s.SelectedIndex <= 0) // niets of eerste geselecteerd
            {
                return;
            }
            int catPosition = cmbCategory.SelectedIndex;
            int itemPosition = lstTop10s.SelectedIndex;
            string tempItem = catContent[catPosition][itemPosition - 1];
            catContent[catPosition][itemPosition - 1] = catContent[catPosition][itemPosition];
            catContent[catPosition][itemPosition] = tempItem;

            lstTop10s.ItemsSource = null;
            lstTop10s.ItemsSource = catContent[catPosition];

            lstTop10s.SelectedIndex = itemPosition - 1;

        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (lstTop10s.SelectedIndex == -1) // niets geselecteerd
            {
                return;
            }
            if(lstTop10s.SelectedIndex == lstTop10s.Items.Count -1) // laatste geselecteerd
            {
                return;
            }
            int catPosition = cmbCategory.SelectedIndex;
            int itemPosition = lstTop10s.SelectedIndex;
            string tempItem = catContent[catPosition][itemPosition + 1];
            catContent[catPosition][itemPosition + 1] = catContent[catPosition][itemPosition];
            catContent[catPosition][itemPosition] = tempItem;

            lstTop10s.ItemsSource = null;
            lstTop10s.ItemsSource = catContent[catPosition];

            lstTop10s.SelectedIndex = itemPosition + 1;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstTop10s.SelectedIndex == -1) // niets geselecteerd
            {
                return;
            }
            int catPosition = cmbCategory.SelectedIndex;
            int itemPosition = lstTop10s.SelectedIndex;
            catContent[catPosition].RemoveAt(itemPosition);
            lstTop10s.ItemsSource = null;
            lstTop10s.ItemsSource = catContent[catPosition];
        }
    }
}
