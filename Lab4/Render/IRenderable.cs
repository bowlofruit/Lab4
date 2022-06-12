using System.Windows.Media;

namespace Lab4.Render
{
    interface IRenderable
    {
        void Render(DrawingContext dc, int cameraPos);
    }
}