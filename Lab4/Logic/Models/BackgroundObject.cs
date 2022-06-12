using Lab_2_3.Logic.Render;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab_2_3.Logic.Models
{
    class BackgroundObject : IRenderable
    {
        Size size;
        Point location;
        ImageSource image;
        public BackgroundObject(Point location, ImageSource image, Size size)
        {
            this.location = location;
            this.size = size;
            this.image = image;
        }
        public BackgroundObject(Point location, string relative_path, Size size)
            : this(location, new BitmapImage(new Uri($"pack://application:,,,/{relative_path}")), size)
        { }

        public void Render(DrawingContext dc, int cameraPos)
        {
            dc.DrawImage(image, new Rect(location.X - cameraPos, location.Y, size.Width, size.Height));
        }
    }
}