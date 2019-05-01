using System;
using System.Data.Entity;
using System.Windows.Media.Imaging;

namespace Lab03
{
    public class Person
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? Birthday { get; set; }
        public byte[] Image { get; set; }

        public BitmapImage ImageSource
        {
            get
            {
                return Image!=null?ImageConverter.ToBitmapImage(Image):null;
            }
        }
    }

    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}
