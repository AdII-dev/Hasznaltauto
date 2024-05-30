using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hasznaltauto
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckAndDisplayImage(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Marka.Text) &&
                !string.IsNullOrWhiteSpace(Tipus.Text) &&
                !string.IsNullOrWhiteSpace(Uzemanyag.Text) &&
                !string.IsNullOrWhiteSpace(Kivitel.Text) &&
                !string.IsNullOrWhiteSpace(Evjarat.Text) &&
                !string.IsNullOrWhiteSpace(Ar.Text) &&
                Hajtas.SelectedItem != null)
            {
                CarImage.Source = new BitmapImage(new Uri("path/to/your/image.jpg", UriKind.Relative));
                CarImage.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                CarImage.Visibility = Visibility.Collapsed;
            }
        }
    }
}
