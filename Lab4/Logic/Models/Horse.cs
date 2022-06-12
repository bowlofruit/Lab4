using Lab_2_3.Logic.Render;
using Lab_2_3.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_2_3.Logic.Models
{
    public class Horse : PropertyChangedNotifier, IRenderable
    {
        [DisplayName("Name")]
        public string Name { get; private set; }

        [DisplayName("Color")]
        public Color Color { get; private set; }

        [DisplayName("Position")]
        public int Position { get; private set; }

        [DisplayName("Time")]
        public TimeSpan Time { get; private set; }

        [DisplayName("FormattedTime")]
        public string FormattedTime => string.Format(@"{0:mm\:ss\:ffffff}", Time);

        [DisplayName("Coefficient")]
        public double Coeff { get; private set; }
        
        [DisplayName("Money")]
        public double Money { get; private set; }
        public Horse(string name, Color color, double coeff, int track, List<ImageSource> animations)
        {
            Name = name;
            Color = color;
            Coeff = coeff;
            Track = track;
            Animations = animations;
        }

        public void SetMoney(double value)
        {
            if (value < 0) return;
            Money = value;
            OnPropertyChanged(nameof(Money));
        }
        public void AddCoeff(double value_to_add)
        {
            if (Coeff + value_to_add < 0.7) return;
            Coeff += value_to_add;
            OnPropertyChanged(nameof(Coeff));
        }

        public async Task StartRace(int traceEnds, Stopwatch timer)
        {
            Random rnd = new Random();
            Position = 0;
            isStarted = true;
            speed = average_speed;
            speed_difference = 0;
            this.timer = timer;
            this.traceEnds = traceEnds;

            while (isStarted)
            {
                await Task.Delay(200);
                speed_difference_changing = true;
                var (min, max) = (average_speed / 9 * -1, average_speed / 6.5);
                var acceleration = rnd.NextDouble() * (max - min) + min;
                if (speed < average_speed * 2 / 3) acceleration += average_speed / 17;
                if (speed > average_speed * 3 / 2) acceleration -= average_speed / 17;
                speed_difference = acceleration;
                speed_difference_changing = false;
            }
        }
        public void StopRace()
        {
            isStarted = false;
            speed = 0;
            speed_difference = 0;
        }

        Stopwatch timer;
        private int Track;
        private List<ImageSource> Animations;
        private bool isStarted;
        private int traceEnds;
        private const double average_speed = 30;
        private double speed;
        private double speed_difference;
        private bool speed_difference_changing;
        private double frame;

        public void Render(DrawingContext dc, int cameraPos)
        {
            dc.DrawImage(Animations[(int)Math.Floor(frame)], new Rect(new Point(Position - cameraPos, 160 + 50 * Track), new Size(120 + Track * 8, 120 + Track * 12)));

            if (isStarted)
            {
                frame = (frame + 0.7 * (speed / average_speed)) % Animations.Count;
                Position += (int)Math.Round(speed);
                Time = new TimeSpan(timer.ElapsedTicks);

                if (!speed_difference_changing)
                {
                    if (speed_difference != 0) speed += speed_difference / 5;
                }                
                if (Position >= traceEnds)
                {
                    Position = traceEnds;
                    frame = 0;
                    StopRace();
                }
                OnPropertyChanged(nameof(Position));
            }
        }
    }
}