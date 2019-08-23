namespace Dalssoft.DiagramNet.Interfaces
{
    /// <summary>
    /// When a class implements this interface, then it can be controlled.
    /// </summary>
    internal interface IControllable
    {
        IController GetController();
    }
}
