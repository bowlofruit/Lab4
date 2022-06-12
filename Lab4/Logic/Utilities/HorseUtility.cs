using Lab_2_3.Logic.Models;
using System;
using System.Reflection;
using System.Windows.Media;

namespace Lab_2_3.Logic.Utilities
{
    class HorseUtility
    {
        Random rnd = new Random();
        ImageUtility imageUtility = new ImageUtility();

        public Horse GenerateRandomHorse(int index)
        {            
            var name = Names[rnd.Next(Names.Length)];

            PropertyInfo[] properties = typeof(Colors).GetProperties();
            var color = (Color)properties[rnd.Next(properties.Length)].GetValue(null, null);
            
            var anim = imageUtility.GetHorseAnimation(color);

            return new Horse($"{index + 1}.{name}", color, 1.25, index % 8, anim);
        }
        private static readonly string[] Names =
        {
            "Bella","Alex","Lilly","Alexia","Fancy","Sugar","Lady","Tucker","Dakota","Cash",
            "Daisy","Spirit","Cisco","Annie","Buddy","Whiskey","Chance","Blue","Molly","Ginger",
            "Gypsy","Charlie","Ranger","Star","Willow","Lacey","Scout","Lucky","Ladybug","Ellie",
            "Belle","Lucy","Rosie","Rebel","Cody","Jasper","Magic","Cricket","Cheyenne","Dash",
            "Red","Bruno","Chief","Sunshine","Cloud","Dusty","Rose","Sunrise","Kid","Angel","Teddy"
        };
    }
}