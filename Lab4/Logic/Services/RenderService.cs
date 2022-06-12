using Lab_2_3.Logic.Models;
using System.Collections.Generic;

namespace Lab_2_3.Logic.Services
{    
    class RenderService
    {
        public delegate int GetPosition();
        public delegate (int, int) GetSize();
        public RenderService(HorsesService horsesService, List<BackgroundObject> backgrounds, GetSize renderSizeDelegate)
        {
            this.horsesService = horsesService;
            this.backgrounds = backgrounds;
            GetRenderSize = renderSizeDelegate;
        }
        public HorsesService horsesService { get; }
        public List<BackgroundObject> backgrounds { get; }
        public GetSize GetRenderSize { get; private set; }
        public GetPosition GetCameraPosition { get; private set; }
        public Horse ObservableHorse { get; private set; }
        public void ChangeObserver(Horse horse)
        {
            if (horse is null) return;
            ObservableHorse = horse;
            GetCameraPosition = () => horse.Position + 75 - GetRenderSize().Item1 / 2;
        }
    }
}