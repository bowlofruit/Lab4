using Lab4.Models;
using Lab4.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4.Services
{
    class HorsesService
    {
        public Collection<Horse> Horses { get; private set; } = new Collection<Horse>();

        public void GenerateList(int count)
        {
            if (Horses.Count < count)
            {
                HorseUtility horseUtility = new HorseUtility();
                for (int i = Horses.Count; i < count; i++)
                {
                    var horse = horseUtility.GenerateRandomHorse(i);
                    Horses.Add(horse);
                }
            }
            else if (Horses.Count > count)
            {
                var subtract = Horses.Count - count;
                for (int i = 0; i < subtract; i++) Horses.Remove(Horses.Last());
            }
        }
        public async Task StartRaceAsync(int traceEnds)
        {
            Stopwatch timer = new Stopwatch();
            List<Task> tasks = new List<Task>();
            timer.Start();
            foreach (var horse in Horses)
            {
                tasks.Add(horse.StartRace(traceEnds, timer));
            }
            await Task.WhenAll(tasks);
            timer.Stop();
        }
        public void StopRace()
        {
            foreach (var horse in Horses)
            {
                horse.StopRace();
            }
        }
    }
}