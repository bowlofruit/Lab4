using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab_2_3.Logic.Utilities
{
    class ImageUtility
    {
        public List<ImageSource> GetHorseAnimation(Color color)
        {
            const int count = 12;
            var bitmap_image_list = ReadImageList("Images/Horses", "WithOutBorder_", ".png", count);
            var mask_image_list = ReadImageList("Images/HorsesMask", "mask_", ".png", count);
            return bitmap_image_list.Select(x => x as ImageSource).ToList(); // <-- program will be fast as fuck
            return bitmap_image_list.Select((item, index) => GetImageWithColor(item, mask_image_list[index], color)).ToList();
        }
        private List<BitmapImage> ReadImageList(string path, string name, string format, int count)
        {
            // pack://application:,,,/Images/folder.png
            path = $"pack://application:,,,/{path}/{name}";
            List<BitmapImage> list = new List<BitmapImage>();
            for (int i = 0; i < count; i++)
            {
                var uri = path + string.Format("{0:0000}", i) + format;
                var img = new BitmapImage(new Uri(uri));
                list.Add(img);
            }
            return list;
        }

        // https://github.com/reneschulte/WriteableBitmapEx <-- used nuget extension for WriteableBitmap
        private ImageSource GetImageWithColor(BitmapImage image, BitmapImage mask, Color color)
        {
            WriteableBitmap image_bmp = new WriteableBitmap(image);
            WriteableBitmap mask_bmp = new WriteableBitmap(mask);
            WriteableBitmap output_bmp = BitmapFactory.New(image.PixelWidth, image.PixelHeight);
            output_bmp.ForEach((x, y, c) =>
            {
                return MultiplyColors(image_bmp.GetPixel(x, y), color, mask_bmp.GetPixel(x, y).A);
            });
            return output_bmp;
        }

        private Color MultiplyColors(Color color_1, Color color_2, byte alpha)
        {
            var amount = alpha / 255.0;
            byte r = (byte)(color_2.R * amount + color_1.R * (1 - amount));
            byte g = (byte)(color_2.G * amount + color_1.G * (1 - amount));
            byte b = (byte)(color_2.B * amount + color_1.B * (1 - amount));
            return Color.FromArgb(color_1.A, r, g, b);
        }
    }
}