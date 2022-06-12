using Lab4.Models;
using System.Collections.Generic;

namespace Lab4.Services
{    
    class RenderService
    {
        public HorsesService HorsesService { get; }
        public List<BackgroundObject> Backgrounds { get; }
        public GetSize GetRenderSize { get; private set; }
        public GetPosition GetCameraPosition { get; private set; }
        public Horse CurrentFocusHorse { get; private set; }

        public delegate int GetPosition();
        public delegate (int, int) GetSize();

        public RenderService(HorsesService horsesService, List<BackgroundObject> backgrounds, GetSize renderSizeDelegate)
        {
            HorsesService = horsesService;
            Backgrounds = backgrounds;
            GetRenderSize = renderSizeDelegate;
        }

        public void FocusChange(Horse horse)
        {
            if (horse is null) return;
            CurrentFocusHorse = horse;
            GetCameraPosition = () => horse.Position + 100 - GetRenderSize().Item1 / 2;
        }
    }
}