using System;

namespace Dalssoft.DiagramNet.EventsArgs
{
    public class ElementConnectEventArgs : EventArgs
    {
        public ElementConnectEventArgs(NodeElement node1, NodeElement node2, BaseLinkElement link)
        {
            Node1 = node1;
            Node2 = node2;
            Link = link;
        }

        public NodeElement Node1 { get; }
        public NodeElement Node2 { get; }
        public BaseLinkElement Link { get; }

        public override string ToString()
        {
            string toString = "";

            if (Node1 != null)
                toString += "Node1:" + Node1.ToString();

            if (Node2 != null)
                toString += "Node2:" + Node2.ToString();

            if (Link != null)
                toString += "Link:" + Link.ToString();

            return toString;
        }

    }
}