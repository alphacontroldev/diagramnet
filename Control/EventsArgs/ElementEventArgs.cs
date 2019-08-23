using System;

namespace Dalssoft.DiagramNet.EventsArgs
{
    public class ElementEventArgs : EventArgs
    {
        public ElementEventArgs(BaseElement el) => Element = el;

        public BaseElement Element { get; }

        public override string ToString() => $"el: {Element.GetHashCode()}";
    }
}