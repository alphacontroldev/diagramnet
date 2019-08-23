using System;

namespace Dalssoft.DiagramNet.EventsArgs
{
    public class ElementSelectionEventArgs : EventArgs
    {
        public ElementSelectionEventArgs(ElementCollection elements) => Elements = elements;

        public ElementCollection Elements { get; }

        public override string ToString() => $"ElementCollection: {Elements.Count}";
    }
}