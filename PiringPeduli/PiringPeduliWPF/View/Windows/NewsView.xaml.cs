using System.Windows;
using System.Windows.Controls;

namespace PiringPeduliWPF.View.Windows
{
    public partial class NewsView : Window
    {
        public NewsView()
        {
            InitializeComponent();
            LoadNewsItems();
        }

        private void LoadNewsItems()
        {
            // Contoh data
            var newsData = new[]
            {
                new { Title = "Berita 1", Description = "Deskripsi berita 1", UrlToImage = "https://via.placeholder.com/100" },
                new { Title = "Berita 2", Description = "Deskripsi berita 2", UrlToImage = "https://via.placeholder.com/100" },
                // Tambahkan lebih banyak data
            };

            foreach (var item in newsData)
            {
                // Buat Border untuk setiap berita
                var border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = System.Windows.Media.Brushes.Red,
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(5),
                };

                // Grid untuk mengatur layout gambar dan teks
                var grid = new Grid
                {
                    Background = System.Windows.Media.Brushes.White
                };

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.6, GridUnitType.Star) });

                // Gambar
                var viewbox = new Viewbox
                {
                    Margin = new Thickness(5)
                };
                var imageBorder = new Border
                {
                    CornerRadius = new CornerRadius(10),
                    ClipToBounds = true
                };
                var image = new Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(item.UrlToImage)),
                    Stretch = System.Windows.Media.Stretch.Uniform
                };
                imageBorder.Child = image;
                viewbox.Child = imageBorder;
                Grid.SetColumn(viewbox, 0);
                grid.Children.Add(viewbox);

                // StackPanel untuk teks
                var stackPanel = new StackPanel
                {
                    Margin = new Thickness(10, 0, 0, 0)
                };

                // Judul
                var title = new TextBlock
                {
                    Text = item.Title,
                    FontWeight = FontWeights.Bold,
                    FontSize = 16,
                    Foreground = System.Windows.Media.Brushes.Black,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                stackPanel.Children.Add(title);

                // Deskripsi
                var description = new TextBlock
                {
                    Text = item.Description,
                    FontSize = 14,
                    Foreground = System.Windows.Media.Brushes.Gray,
                    TextWrapping = TextWrapping.Wrap
                };
                stackPanel.Children.Add(description);

                Grid.SetColumn(stackPanel, 1);
                grid.Children.Add(stackPanel);

                border.Child = grid;

                // Tambahkan ke StackPanel induk
                ContentContainer.Children.Add(border);
            }
        }
    }
}
