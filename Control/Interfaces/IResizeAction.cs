using System.Drawing;
using System.Windows.Forms;
using Dalssoft.DiagramNet.EventsArgs;

namespace Dalssoft.DiagramNet.Interfaces
{
    public delegate void OnElementResizingDelegate(ElementEventArgs args);

    internal interface IResizeAction
    {
        bool IsResizing { get; }
        bool IsResizingLink { get; }

        void Select(Document document);
        void Start(Point mousePoint, OnElementResizingDelegate callback);
        void Resize(Point dragPoint);
        void End(Point posEnd);
        void DrawResizeCorner(Graphics g);
        void UpdateResizeCorner();
        Cursor UpdateResizeCornerCursor(Point mousePoint);
        void ShowResizeCorner(bool show);
    }
}