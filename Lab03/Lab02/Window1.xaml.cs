using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Shapes;

namespace Lab03
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Func<ChartPoint, string> labelPoint = chartpoint => string.Format("{0} ({1:P})", chartpoint.Y, chartpoint.Participation);

        public Window1(PersonContext context)
        {
            InitializeComponent();
                Create_data_age create = new Create_data_age(context);
                SeriesCollection seriesViews = new SeriesCollection();

            foreach (var obj in Create_data_age.ageData)
            {
                seriesViews.Add(new PieSeries()
                {
                    Title = obj.Age.ToString(),
                    Values = new ChartValues<int>{ obj.Count },
                    DataLabels = true,
                    LabelPoint = labelPoint
                });     
            }
            piechart.Series = seriesViews;
            //InitializeComponent();
        }
    }
}
