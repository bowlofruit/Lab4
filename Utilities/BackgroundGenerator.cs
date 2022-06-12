using Lab4.Models;
using System.Collections.Generic;
using System.Windows;

namespace Lab4.Utilities
{
    class BackgroundGenerator
    {
        public List<BackgroundObject> Generate(int traceLength)
        {
            var backgrounds = new List<BackgroundObject>();

            var img_size = new Size(960, 990);
            for (int i = 0; i < (traceLength / img_size.Width) + 1; i++)
            {
                backgrounds.Add(new BackgroundObject(new Point(img_size.Width * i, -30), "Images/Background/Track.png", img_size));
            }

            return backgrounds;
        }
    }
}