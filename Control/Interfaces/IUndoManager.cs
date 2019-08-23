namespace Dalssoft.DiagramNet.Interfaces
{
    internal interface IUndoManager<T>
    {
        bool CanRedo { get; }
        bool CanUndo { get; }
        bool Enabled { get; set; }

        void AddUndo(T obj);
        T Redo();
        T Undo();
    }
}