using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hasznaltauto
{
    public partial class MainWindow : Window
    {
        private Car[] cars;

        public MainWindow()
        {
            InitializeComponent();
            LoadCarData();
            PopulateMarkaList();
            PopulateKivitelList();
            PopulateEvjaratList();
        }

        private void LoadCarData()
        {
            var lines = File.ReadAllLines(@"..\..\..\Adatok.txt");
            cars = lines.Skip(1).Select(line => new Car(line)).ToArray();
        }

        private void PopulateMarkaList()
        {
            var markak = cars.Select(car => car.Marka).Distinct().ToList();
            foreach (var marka in markak)
            {
                Marka.Items.Add(new ListBoxItem { Content = marka });
            }
        }

        private void PopulateKivitelList()
        {
            var kivitelek = cars.Select(car => car.Kivitel).Distinct().ToList();
            foreach (var kivitel in kivitelek)
            {
                Kivitel.Items.Add(new ListBoxItem { Content = kivitel });
            }
        }

        private void PopulateEvjaratList()
        {
            for (int ev = 2000; ev <= 2024; ev++)
            {
                Evjarat.Items.Add(new ListBoxItem { Content = ev.ToString() });
            }
        }

        private void Marka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Marka.SelectedItem is ListBoxItem selectedItem)
            {
                var selectedMarka = selectedItem.Content.ToString();
                var tipusok = cars.Where(car => car.Marka == selectedMarka)
                                 .Select(car => car.Tipus)
                                 .Distinct()
                                 .ToList();

                Tipus.Items.Clear();
                foreach (var tipus in tipusok)
                {
                    Tipus.Items.Add(new ListBoxItem { Content = tipus });
                }
            }
        }

        private void Uzemanyag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Uzemanyag.SelectedItem is ListBoxItem selectedItem && selectedItem.Content.ToString() == "Elektromos")
            {
                MessageBox.Show("Azt felejtsd is el.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CheckAndDisplayImage(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs(out string uzemanyag))
            {
                var selectedCar = cars.FirstOrDefault(car =>
                    car.Marka == ((ListBoxItem)Marka.SelectedItem).Content.ToString() &&
                    car.Tipus == ((ListBoxItem)Tipus.SelectedItem).Content.ToString() &&
                    car.Uzemanyag == uzemanyag &&
                    car.Kivitel == ((ListBoxItem)Kivitel.SelectedItem).Content.ToString() &&
                    car.Hajtas == ((ListBoxItem)Hajtas.SelectedItem).Content.ToString() &&
                    car.Evjarat == int.Parse(((ListBoxItem)Evjarat.SelectedItem).Content.ToString()) &&
                    car.Ar == int.Parse(Ar.Text));

                if (selectedCar != null)
                {
                    CarImage.Source = new BitmapImage(new Uri(selectedCar.ImgPath, UriKind.Relative));
                    CarImage.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Nem található megfelelő autó.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CarImage.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                CarImage.Visibility = Visibility.Collapsed;
            }
        }

        private bool ValidateInputs(out string uzemanyag)
        {
            uzemanyag = null;
            if (Marka.SelectedItem == null ||
                Tipus.SelectedItem == null ||
                Uzemanyag.SelectedItem == null ||
                Kivitel.SelectedItem == null ||
                Evjarat.SelectedItem == null ||
                string.IsNullOrWhiteSpace(Ar.Text) ||
                Hajtas.SelectedItem == null)
            {
                return false;
            }

            uzemanyag = (Uzemanyag.SelectedItem as ListBoxItem)?.Content.ToString();
            return true;
        }
    }
}