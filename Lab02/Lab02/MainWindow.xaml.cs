using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string randomImgUrl = "https://source.unsplash.com/random/250x250";
        private static readonly HttpClient client = new HttpClient();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            AddPersonLoop();
        }

        async void AddPersonLoop()
        {
            int i = 1;

            await Task.Run(async () =>
            {
                while (true)
                {
                    var response = await client.GetAsync(randomImgUrl);
                    MemoryStream memory = await response.Content.ReadAsStreamAsync() as MemoryStream;
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = memory;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    Dispatcher.Invoke(() =>
                    {
                        Items.Add(new Person { Name = $"Person{i}", Age = i, Image = bitmap });
                    });

                    i++;
                    await Task.Delay(3000);
                }
            });
        }

        public ObservableCollection<Person> Items { get; } = new ObservableCollection<Person>
        {
            new Person { Name = "P1", Age = 1 },
            new Person { Name = "P2", Age = 2 }
        };

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            Items.Add(new Person { Age = int.Parse(ageTextBox.Text), Name = nameTextBox.Text, Image = pictureBox.Source as BitmapImage});
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };  

            if (dlg.ShowDialog() == true)
            {
                pictureBox.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }
    }
}