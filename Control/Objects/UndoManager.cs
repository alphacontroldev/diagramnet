using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dalssoft.DiagramNet.Interfaces;

namespace Dalssoft.DiagramNet.Objects
{
    internal class UndoManager : IUndoManager<object>
    {
        protected MemoryStream[] _steps;
        protected int _index;
        protected int _maxIndex;

        public UndoManager(int capacity)
        {
            _steps = new MemoryStream[capacity];
        }

        public bool Enabled { get; set; }
        public bool CanUndo => _index > 0;
        public bool CanRedo => _index < _maxIndex;

        public void AddUndo(object o)
        {
            if (!Enabled) return;

            ClearStepsAbove();
            OpenSpaceForSteps();

            _steps[_index] = SerializeObject(o);
            _maxIndex = _index;
            _index++;
        }
        private void ClearStepsAbove()
        {
            for (var i = _index; i < _steps.Length; i++)
            {
                _steps[i]?.Close();
                _steps[i]?.Dispose();
                _steps[i] = null;
            }
        }
        private void OpenSpaceForSteps()
        {
            if (_index < _steps.Length) return;

            _steps[0]?.Close();
            _steps[0]?.Dispose();

            for (var i = 1; i < _steps.Length; i++)
                _steps[i - 1] = _steps[i];

            _steps[_steps.Length - 1] = null;
            _index--;
        }

        public object Undo()
        {
            if (!CanUndo) throw new ApplicationException("Can't Undo.");

            return _index >= 0
                ? DeserializeObject(_steps[--_index])
                : null;
        }
        private object DeserializeObject(MemoryStream stream)
        {
            stream.Position = 0;
            var formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        public object Redo()
        {
            if (!CanRedo) throw new ApplicationException("Can't Undo.");

            return _index < _maxIndex
                ? DeserializeObject(_steps[_index++])
                : null;
        }
        private MemoryStream SerializeObject(object obj)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            stream.Position = 0;

            return stream;
        }
    }
}
