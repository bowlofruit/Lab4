using Lab_2_3.Logic.Models;
using Lab_2_3.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_2_3.Logic.Services
{
    class HorsesService
    {
        public ObservableCollection<Horse> Horses { get; private set; } = new ObservableCollection<Horse>();
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
            Console.WriteLine("Horses Thread: " + Thread.CurrentThread.ManagedThreadId);
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