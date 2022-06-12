using Lab4.Render;
using Lab4.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab4.Models
{
    public class Horse : PropertyChangedNotifier, IRenderable
    {
        Stopwatch _timer;
        private int _track;
        private List<ImageSource> _animations;
        private bool _isStarted;
        private int _traceEnds;
        private const double _averageSpeed = 60;
        private double _speed;
        private double _speedDifference;
        private bool _speedDifferenceChanging;
        private double _frame;

        public string Name { get; private set; }
        public Color Color { get; private set; }
        public int Position { get; private set; }
        public TimeSpan Time { get; private set; }
        public string FormattedTime => string.Format(@"{0:mm\:ss\:ff}", Time);

        public Horse(string name, Color color, int track, List<ImageSource> animations)
        {
            Name = name;
            Color = color;
            _track = track;
            _animations = animations;
        }

        public async Task StartRace(int traceEnds, Stopwatch timer)
        {
            Random rnd = new Random();
            Position = 0;
            _isStarted = true;
            _speed = _averageSpeed;
            _speedDifference = 0;
            _timer = timer;
            _traceEnds = traceEnds;

            while (_isStarted)
            {
                await Task.Delay(300);
                _speedDifferenceChanging = true;
                double min = _averageSpeed / 26 * -1;
                double max = _averageSpeed / 34;
                double acceleration = rnd.NextDouble() * (max - min) + min;
                if (_speed < _averageSpeed * 4/2) 
                    acceleration += _averageSpeed / 20;
                if (_speed > _averageSpeed * 4/2) 
                    acceleration -= _averageSpeed / 20;
                _speedDifference = acceleration;
                _speedDifferenceChanging = false;
            }
        }
        public void StopRace()
        {
            _isStarted = false;
            _speed = 0;
            _speedDifference = 0;
        }

        public void Render(DrawingContext dc, int cameraPos)
        {
            dc.DrawImage(_animations[(int)Math.Floor(_frame)], new Rect(new Point(Position - cameraPos, 160 + 50 * _track), new Size(120 + _track * 8, 120 + _track * 12)));

            if (_isStarted)
            {
                _frame = (_frame + 0.7 * (_speed / _averageSpeed)) % _animations.Count;
                Position += (int)Math.Round(_speed);
                Time = new TimeSpan(_timer.ElapsedTicks);

                if (!_speedDifferenceChanging)
                {
                    if (_speedDifference != 0) _speed += _speedDifference / 5;
                }
                
                if (Position >= _traceEnds)
                {
                    Position = _traceEnds;
                    _frame = 0;
                    StopRace();
                }

                OnPropertyChanged(nameof(Position));
            }
        }
    }
}