using Lab_2_3.Logic.Models;
using System.Collections.Generic;
using System.Windows;

namespace Lab_2_3.Logic.Utilities
{
    class BackgroundGenerator
    {
        public List<BackgroundObject> Generate(int traceLength)
        {
            var backs = new List<BackgroundObject>();

            var img_size = new Size(960, 990);
            for (int i = -2; i < (traceLength / img_size.Width) + 1; i++)
            {
                backs.Add(new BackgroundObject(new Point(img_size.Width * i, -30), "Images/Background/Track.png", img_size));
            }

            return backs;
        }
    }
}