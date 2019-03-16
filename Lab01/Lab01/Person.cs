using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab01
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Bitmap Image { get; set; }
        public ImageSource ImagePath { get; set; }
    }
}
