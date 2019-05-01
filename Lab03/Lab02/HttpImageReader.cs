using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lab03
{
    public static class HttpImageReader
    {
        public async static Task<BitmapImage> GetImage(string imageUrl)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(imageUrl);
                MemoryStream memory = await response.Content.ReadAsStreamAsync() as MemoryStream;
                bitmap.StreamSource = memory;
            }
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
    }
}
