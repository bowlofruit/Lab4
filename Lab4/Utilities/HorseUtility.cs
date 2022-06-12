using Lab4.Models;
using System;
using System.Reflection;
using System.Windows.Media;

namespace Lab4.Utilities
{
    class HorseUtility
    {
        Random _rnd = new Random();
        ImageUtility _imageUtility = new ImageUtility();

        public Horse GenerateRandomHorse(int index)
        {
            var name = Names[_rnd.Next(Names.Length)];

            PropertyInfo[] properties = typeof(Colors).GetProperties();
            var color = (Color)properties[_rnd.Next(properties.Length)].GetValue(null, null);
            
            var anim = _imageUtility.GetHorseAnimation(color);

            return new Horse($"{index + 1}.{name}", color, index % 8, anim);
        }
        private static readonly string[] Names =
        {
            "Liam","Oliver","Elijah","James","William","Benjamin","Lucas","Henry","Jackson","Daniel",
            "Michael","Mason","Cisco","Saul","Buddy","Whiskey","Chance","Molly","Yusuf"
        };
    }
}