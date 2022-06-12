using Lab4.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab4.Render
{
    class MainRender
    {
        public event Action RenderEvent;

        Image _image;
        RenderService _renderService;
        bool _renderStarted;

        public static double PixelsPerDip => 96;
        public static int FramesPerSecond => 60;

        public MainRender(Image image, RenderService renderService)
        {
            _image = image;
            _renderService = renderService;
        }

        public async void Start()
        {
            _renderStarted = true;
            while (_renderStarted)
            {
                await Task.Delay(1000 / FramesPerSecond);
                RenderFrame();
                RenderEvent?.Invoke();
            }
        }

        public void Stop()
        {
            _renderStarted = false;
        }

        public void RenderFrame()
        {
            _image.Source = GetRender();
        }

        private BitmapSource GetRender()
        {
            var _renderSize = _renderService.GetRenderSize();
            var bitmap = new RenderTargetBitmap(_renderSize.Item1, _renderSize.Item2, PixelsPerDip, PixelsPerDip, PixelFormats.Pbgra32);

            var drawingvisual = new DrawingVisual();

            using (DrawingContext dc = drawingvisual.RenderOpen())
            {
                Render(dc);
            }

            bitmap.Render(drawingvisual);
            return bitmap;
        }
        private void Render(DrawingContext dc)
        {
            int camera_position = _renderService.GetCameraPosition();
            foreach (var item in _renderService.Backgrounds)
            {
                item.Render(dc, camera_position);
            }
            foreach (var horse in _renderService.HorsesService.Horses)
            {
                horse.Render(dc, camera_position);
            }
        }
    }
}