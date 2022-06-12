using Lab_2_3.Logic.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Lab_2_3.Logic.Render
{
    class MainRender
    {
        public event Action RenderEvent;

        Image image;
        RenderService renderService;
        public static double PixelsPerDip => 96;
        public static int FramesPerSecond => 50;
        bool render_started;

        public MainRender(Image image, RenderService renderService)
        {
            this.image = image;
            this.renderService = renderService;
        }

        public async void Start()
        {
            render_started = true;
            Console.WriteLine("Render Thread: " + Thread.CurrentThread.ManagedThreadId);
            while (render_started)
            {
                await Task.Delay(1000 / FramesPerSecond);
                RenderFrame();
                RenderEvent?.Invoke();
            }
        }
        public void Stop() => render_started = false;
        public void RenderFrame() => image.Source = GetRender();
        private BitmapSource GetRender()
        {
            var render_size = renderService.GetRenderSize();
            var bitmap = new RenderTargetBitmap(
                render_size.Item1, render_size.Item2,
                PixelsPerDip, PixelsPerDip, PixelFormats.Pbgra32);

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
            int camera_position = renderService.GetCameraPosition();
            foreach (var item in renderService.backgrounds)
            {
                item.Render(dc, camera_position);
            }
            foreach (var horse in renderService.horsesService.Horses)
            {
                horse.Render(dc, camera_position);
            }
        }
    }
}