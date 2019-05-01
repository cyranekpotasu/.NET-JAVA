using System.IO;
using System.Windows.Media.Imaging;

namespace Lab03
{
    public static class ImageConverter
    {
        public static byte[] ToByteArray(BitmapImage image)
        {
            byte[] data;
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static BitmapImage ToBitmapImage(byte[] bytearray)
        {
            using (var ms = new MemoryStream(bytearray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
    }
}
