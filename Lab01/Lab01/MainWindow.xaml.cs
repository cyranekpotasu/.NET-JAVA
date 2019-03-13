using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Interop;

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap bitmap;
        private string imageUrl;
        int i = 0;
        string[] picture_urls = {
            "https://www.hdwallpapers.in/thumbs/2019/abstract_neon_eye-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/abstract_leaves-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/samsung_galaxy_s10_stock-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/aston_martin_vanquish_vision_concept_2019_4k_2-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/lamborghini_aventador_svj_roadster_2019_4k_8k-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/bugatti_la_voiture_noire_2019_geneva_motor_show_5k-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/snow_mountains_landscape_2-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/ferrari_f8_tributo_2019_4k_5k_2-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/spider_man_into_the_spider_verse_4k_14-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/nokia_8_sirocco_two_worlds_collide-t1.jpg",
            "https://www.hdwallpapers.in/thumbs/2019/captain_marvel_3-t1.jpg",
            "https://images.unsplash.com/photo-1444703686981-a3abbc4d4fe3?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80"
        };


        public ObservableCollection<Person> Items { get; } = new ObservableCollection<Person> { };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (i == picture_urls.Count<string>())
                i = 0;
            DownloadImage(picture_urls[i++]);
            Items.Add(new Person { Age = int.Parse(ageTextBox.Text), Name = nameTextBox.Text, Image = bitmap });
        }

        private void AddNewImage(object sender, RoutedEventArgs e)
        {
            DownloadImage(picture_urls[i++]);
            imageBox.Source = GetImage();
        }

        public void DownloadImage(string imageUrl)
        {
            this.imageUrl = imageUrl;
            Download();
        }

        public void Download()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(imageUrl);
                bitmap = new Bitmap(stream);
                stream.Flush();
                //stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public ImageSource GetImage()
        {
            var handle = bitmap.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public void SaveImage(string filename, ImageFormat format)
        {
            if (bitmap != null)
            {
                bitmap.Save(filename, format);
            }
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index == -1)
                MessageBox.Show("Item is not available in ListBox");
            else
            {
                bitmap = Items[index].Image;
                imageBox.Source = GetImage();
            }
        }
    }
}