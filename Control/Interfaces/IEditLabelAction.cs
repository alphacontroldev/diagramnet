using System.Windows.Forms;

namespace Dalssoft.DiagramNet.Interfaces
{
    internal interface IEditLabelAction
    {
        void StartEdit(BaseElement el, TextBox textBox);
        void EndEdit();
    }
}