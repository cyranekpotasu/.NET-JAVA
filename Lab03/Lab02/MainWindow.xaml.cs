using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lab03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PersonContext context = new PersonContext();
        BackgroundWorker worker = new BackgroundWorker();
        private static readonly HttpClient client = new HttpClient();


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
            int countJob = int.Parse(Dispatcher.Invoke(() => countTextBox.Text));
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i < countJob; i++)
            {
                if (worker.CancellationPending)
                {
                    //worker.ReportProgress(0, "Cancelled");
                    e.Cancel = true;
                    return;
                }

                var person = await GetPersonData();

                Dispatcher.Invoke(() =>
                {
                    context.People.Add(person);
                    context.SaveChanges();
                });

            }
            //  worker.ReportProgress(100, "Done");
        }

        private async Task<Person> GetPersonData()
        {
            string person = null, city = null, email = null;
            DateTime? birthday = null;
            int age = 0;
            BitmapImage bitmap = null;

            var personJson = await PersonFetcher.FetchPerson();

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

            return new Person
            {
                Name = person,
                Age = age,
                Image = ImageConverter.ToByteArray(bitmap),
                City = city,
                Email = email,
                Birthday = birthday
            };
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
            client.Dispose();
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
    }
}