using Lab4.Render;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab4.Models
{
    class BackgroundObject
    {
        Size _size;
        Point _location;
        ImageSource _image;

        public BackgroundObject(Point location, ImageSource image, Size size)
        {
            _location = location;
            _image = image;
            _size = size;
        }
        public BackgroundObject(Point location, string relative_path, Size size)
            : this(location, new BitmapImage(new Uri($"pack://application:,,,/{relative_path}")), size)
        { }

        public void Render(DrawingContext dc, int cameraPos)
        {
            dc.DrawImage(_image, new Rect(_location.X - cameraPos, _location.Y, _size.Width, _size.Height));
        }
    }
}