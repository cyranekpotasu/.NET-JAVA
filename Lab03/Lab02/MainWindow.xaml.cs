using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string randomImgUrl = "https://randomuser.me/api/?format=xml";

        private static readonly HttpClient client = new HttpClient();
        BackgroundWorker worker = new BackgroundWorker();
        int countJob;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += AddPersonLoop;
            worker.ProgressChanged += Worker_ProgressChanged;
        }


        async void AddPersonLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int age = 1;
            string person = null;
            string city = null;
            string email = null;
            DateTime birthday = DateTime.Parse("2008-11-01T19:35:00.0000000Z");
            int i = 0;
            while (i != countJob)
            {
                var bitmap2 = new BitmapImage();
                var responseXml = await client.GetAsync(randomImgUrl);
                MemoryStream memoryXml = await responseXml.Content.ReadAsStreamAsync() as MemoryStream;
                if (worker.CancellationPending == true)
                {
                    worker.ReportProgress(0, "Cancelled");
                    e.Cancel = true;
                    return;
                }
                else
                    using (XmlReader reader = XmlReader.Create(memoryXml, new XmlReaderSettings()))
                    {

                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    switch (reader.Name)
                                    {
                                        case "username":
                                            if (!(bool)nameCheckBox.Dispatcher.Invoke(() => nameCheckBox.IsChecked))
                                            {
                                                break;
                                            }

                                            while (reader.Read())
                                            {
                                                switch (reader.NodeType)
                                                {
                                                    case XmlNodeType.Text:
                                                        person = reader.Value;
                                                        /*worker.ReportProgress(
                                                        (int)Math.Round((float)i * 100.0 / (float)countJob),
                                                        "Loading " + person + "...");*/
                                                        break;
                                                }
                                                break;
                                            }
                                            break;
                                        case "age":
                                            if (!(bool)ageCheckBox.Dispatcher.Invoke(() => ageCheckBox.IsChecked))
                                            {
                                                break;
                                            }
                                            while (reader.Read())
                                            {
                                                switch (reader.NodeType)
                                                {
                                                    case XmlNodeType.Text:
                                                        Int32.TryParse(reader.Value, out age);
                                                        break;
                                                }
                                                break;
                                            }
                                            break;
                                        case "city":
                                            if (!(bool)cityCheckBox.Dispatcher.Invoke(() => cityCheckBox.IsChecked))
                                            {
                                                break;
                                            }
                                            while (reader.Read())
                                            {
                                                switch (reader.NodeType)
                                                {
                                                    case XmlNodeType.Text:
                                                        city = reader.Value;
                                                        break;
                                                }
                                                break;
                                            }
                                            break;
                                        case "email":
                                            if (!(bool)emailCheckBox.Dispatcher.Invoke(() => emailCheckBox.IsChecked))
                                            {
                                                break;
                                            }
                                            while (reader.Read())
                                            {
                                                switch (reader.NodeType)
                                                {
                                                    case XmlNodeType.Text:
                                                        email = reader.Value;
                                                        break;
                                                }
                                                break;
                                            }
                                            break;
                                        case "date":
                                            if (!(bool)birthdayCheckBox.Dispatcher.Invoke(() => birthdayCheckBox.IsChecked))
                                            {
                                                break;
                                            }
                                            while (reader.Read())
                                            {
                                                switch (reader.NodeType)
                                                {
                                                    case XmlNodeType.Text:
                                                        birthday = DateTime.Parse(reader.Value);
                                                        break;
                                                }
                                                break;
                                            }
                                            break;
                                        case "medium":
                                            if (!(bool)imageCheckBox.Dispatcher.Invoke(() => imageCheckBox.IsChecked))
                                            {
                                                break;
                                            }
                                            while (reader.Read())
                                            {
                                                switch (reader.NodeType)
                                                {
                                                    case XmlNodeType.Text:

                                                        var response = await client.GetAsync(reader.Value);
                                                        MemoryStream memory = await response.Content.ReadAsStreamAsync() as MemoryStream;
                                                        var bitmap = new BitmapImage();
                                                        bitmap.BeginInit();
                                                        bitmap.StreamSource = memory;
                                                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                                        bitmap.EndInit();
                                                        bitmap.Freeze();
                                                        bitmap2 = bitmap;
                                                        break;
                                                }
                                                break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                        }
                        i++;
                        await Task.Delay(3000);
                    }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (!(bool)imageCheckBox.Dispatcher.Invoke(() => imageCheckBox.IsChecked))
                    {
                        if (!(bool)ageCheckBox.Dispatcher.Invoke(() => ageCheckBox.IsChecked))
                            if (!(bool)birthdayCheckBox.Dispatcher.Invoke(() => birthdayCheckBox.IsChecked))
                                Items.Add(new Person { Name = person, City = city, Email = email });
                            else
                                Items.Add(new Person { Name = person, City = city, Email = email, Birthday = birthday });
                        else
                           if (!(bool)birthdayCheckBox.Dispatcher.Invoke(() => birthdayCheckBox.IsChecked))
                                Items.Add(new Person { Name = person, Age = age, City = city, Email = email });
                           else
                                Items.Add(new Person { Name = person, Age = age, City = city, Email = email, Birthday = birthday });
                    }
                    else
                        Items.Add(new Person { Name = person, Age = age, Image = bitmap2, City = city, Email = email, Birthday = birthday });
                });
            }
            //  worker.ReportProgress(100, "Done");
        }

        public ObservableCollection<Person> Items { get; } = new ObservableCollection<Person>
        {
            new Person { Name = "P1", Age = 1 },
            new Person { Name = "P2", Age = 2 }
        };

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dataProgressBar.Value = e.ProgressPercentage;
            dataTextBlock.Text = e.UserState as string;
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            Items.Add(new Person
            {
                Age = int.Parse(ageTextBox.Text),
                Name = nameTextBox.Text,
                Image = pictureBox.Source as BitmapImage,
                City = cityTextBox.Text,
                Email = emailTextBox.Text
            });
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

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
            {
                dataTextBlock.Text = "Cancelling...";
                worker.CancelAsync();
            }
        }

        private void RunDataDownload(object sender, RoutedEventArgs e)
        {
            countJob = int.Parse(countTextBox.Text);
            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
        }

    }
}