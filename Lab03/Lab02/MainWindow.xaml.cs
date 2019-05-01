using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Net.NetworkInformation;
using LiveCharts;
using LiveCharts.Wpf;

namespace Lab03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string randomPersonUrl = "https://randomuser.me/api/?format=json";
        private PersonContext context = new PersonContext();

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
            DateTime birthday = DateTime.Parse("2008-11-01T19:35:00.0000000Z");
            for (int i = 0; i < countJob; i++)
            {
                int age = 1;
                string person = null;
                string city = null;
                string email = null;
                BitmapImage bitmap = null;
                var responseJson = await client.GetStringAsync(randomPersonUrl);
                JToken personJson = JObject.Parse(responseJson)["results"][0];

                if (worker.CancellationPending)
                {
                    //worker.ReportProgress(0, "Cancelled");
                    e.Cancel = true;
                    return;
                }

                if (nameCheckBox.Dispatcher.Invoke(() => nameCheckBox.IsChecked) == true)
                    person = (string)personJson["login"]["username"];

                if (ageCheckBox.Dispatcher.Invoke(() => ageCheckBox.IsChecked) == true)
                    age = (int)personJson["dob"]["age"];

                if (cityCheckBox.Dispatcher.Invoke(() => cityCheckBox.IsChecked) == true)
                    city = (string)personJson["location"]["city"];

                if (emailCheckBox.Dispatcher.Invoke(() => emailCheckBox.IsChecked) == true)
                    email = (string)personJson["email"];

                if (birthdayCheckBox.Dispatcher.Invoke(() => birthdayCheckBox.IsChecked) == true)
                    birthday = (DateTime)personJson["dob"]["date"];

                if (imageCheckBox.Dispatcher.Invoke(() => imageCheckBox.IsChecked) == true)
                    await Dispatcher.Invoke(async () =>
                     {
                         bitmap = await HttpImageReader.GetImage((string)personJson["picture"]["medium"]);
                     });

                Dispatcher.Invoke(() =>
                {
                    context.People.Add(new Person
                    {
                        Name = person,
                        Age = age,
                        Image = ImageConverter.ToByteArray(bitmap),
                        City = city,
                        Email = email,
                        Birthday = birthday
                    });
                    context.SaveChanges();
                });

            }
            //  worker.ReportProgress(100, "Done");
        }


        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dataProgressBar.Value = e.ProgressPercentage;
            dataTextBlock.Text = e.UserState as string;
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            var person = new Person
            {
                Age = int.Parse(ageTextBox.Text),
                Name = nameTextBox.Text,
                Image = ImageConverter.ToByteArray(pictureBox.Source as BitmapImage),
                City = cityTextBox.Text,
                Email = emailTextBox.Text,
                Birthday = datePicker.SelectedDate
            };
            context.People.Add(person);
            context.SaveChanges();
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
            bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (!isAvailable)
            {
                MessageBox.Show("Sorry, ERR_INTERNET_DISCONNECTED");
                return;
            }
            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource personViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("personViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // personViewSource.Source = [generic data source]
            context.People.Load();
            personViewSource.Source = context.People.Local;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            context.Dispose();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Person)personListView.SelectedItem;
            context.People.Remove(person);
            context.SaveChanges();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void ButtonChartPie_Click(object sender, RoutedEventArgs e)
        {
            Window1 form = new Window1(context);
            form.Show();
        }
    }
}