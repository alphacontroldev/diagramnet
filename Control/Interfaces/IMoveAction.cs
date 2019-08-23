using System.Drawing;
using Dalssoft.DiagramNet.EventsArgs;

namespace Dalssoft.DiagramNet.Interfaces
{
    public delegate void OnElementMovingDelegate(ElementEventArgs args);

    internal interface IMoveAction
    {
        bool IsMoving { get; }

        void Start(Point mousePoint, Document document, OnElementMovingDelegate callback);
        void Move(Point dragPoint);
        void End();
    }
}