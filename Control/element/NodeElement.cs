using System;
using System.Drawing;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    /// <summary>
    /// This is the base for all node element.
    /// </summary>
    [Serializable]
    public abstract class NodeElement : BaseElement
    {
        protected const int connectSize = 3;

        //		public NodeElement(): base() 
        //		{
        //			InitConnectors();
        //		}

        protected NodeElement(int top, int left, int width, int height) : base(top, left, width, height)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            InitConnectors();
        }

        [Browsable(false)]
        public ConnectorElement[] Connectors { get; protected set; }

        public override Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                UpdateConnectorsPosition();
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override Size Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                UpdateConnectorsPosition();
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                foreach (ConnectorElement c in Connectors)
                {
                    c.Visible = value;
                }
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual bool IsConnected
        {
            get
            {
                foreach (ConnectorElement c in Connectors)
                {
                    if (c.Links.Count > 0)
                        return true;
                }
                return false;
            }
        }

        protected virtual void InitConnectors()
        {
            Connectors[0] = new ConnectorElement(this);
            Connectors[1] = new ConnectorElement(this);
            Connectors[2] = new ConnectorElement(this);
            Connectors[3] = new ConnectorElement(this);
            UpdateConnectorsPosition();
        }

        protected virtual void UpdateConnectorsPosition()
        {
            Point loc;
            ConnectorElement connect;

            //Top
            loc = new Point(this.location.X + this.size.Width / 2,
                this.location.Y);
            connect = (ConnectorElement)Connectors[0];
            connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
            connect.Size = new Size(connectSize * 2, connectSize * 2);

            //Botton
            loc = new Point(this.location.X + this.size.Width / 2,
                this.location.Y + this.size.Height);
            connect = (ConnectorElement)Connectors[1];
            connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
            connect.Size = new Size(connectSize * 2, connectSize * 2);

            //Left
            loc = new Point(this.location.X,
                this.location.Y + this.size.Height / 2);
            connect = (ConnectorElement)Connectors[2];
            connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
            connect.Size = new Size(connectSize * 2, connectSize * 2);

            //Right
            loc = new Point(this.location.X + this.size.Width,
                this.location.Y + this.size.Height / 2);
            connect = (ConnectorElement)Connectors[3];
            connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
            connect.Size = new Size(connectSize * 2, connectSize * 2);
        }

        public override void Invalidate()
        {
            base.Invalidate();

            for (int i = Connectors.Length - 1; i >= 0; i--)
            {
                //connects[i].Invalidate();

                for (int ii = Connectors[i].Links.Count - 1; ii >= 0; ii--)
                {
                    Connectors[i].Links[ii].Invalidate();
                }
            }
        }


        internal virtual void Draw(Graphics g, bool drawConnector)
        {
            this.Draw(g);
            if (drawConnector)
                DrawConnectors(g);
        }

        protected void DrawConnectors(Graphics g)
        {
            foreach (ConnectorElement ce in Connectors)
            {
                ce.Draw(g);
            }
        }

        public virtual ElementCollection GetLinkedNodes()
        {
            ElementCollection ec = new ElementCollection();

            foreach (ConnectorElement ce in Connectors)
            {
                foreach (BaseLinkElement le in ce.Links)
                {
                    if (le.Connector1 == ce)
                    {
                        ec.Add(le.Connector2.ParentElement);
                    }
                    else
                    {
                        ec.Add(le.Connector1.ParentElement);
                    }
                }
            }

            return ec;
        }
    }
}
