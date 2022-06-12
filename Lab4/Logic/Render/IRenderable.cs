using System.Windows.Media;

namespace Lab_2_3.Logic.Render
{
    interface IRenderable
    {
        void Render(DrawingContext dc, int cameraPos);
    }
}