using AOFL.Kabash.V1.Events;
using AOFL.Kabash.V1.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace AOFL.Kabash.V1.Core
{
    public class ObservableList<T> : IObservableList<T>
    {
        private List<T> _backingList = new List<T>();

        public event ObservedListElementAddedHandler<T> ElementAdded;
        public event ObservedListElementRemovedHandler<T> ElementRemoved;
        public event ObservedListElementModifiedHandler<T> ElementModified;

        public int Count => _backingList.Count;
        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => _backingList[index];
            set
            {
                _backingList[index] = value;
                ElementModified?.Invoke(this, new ObservedListElementModifiedEventArgs<T>(index, value));
            }
        }

        public void Add(T item)
        {
            _backingList.Add(item);

            ElementAdded?.Invoke(this, new ObservedListElementAddedEventArgs<T>(item));
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach(var element in collection)
            {
                Add(element);
            }
        }

        public void Clear()
        {
            _backingList.Clear();
        }

        public bool Contains(T item)
        {
            return _backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _backingList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _backingList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _backingList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _backingList.Insert(index, item);

            ElementAdded?.Invoke(this, new ObservedListElementAddedEventArgs<T>(item));
        }

        public bool Remove(T item)
        {
            var isRemoved = _backingList.Remove(item);

            if (isRemoved)
            {
                ElementRemoved?.Invoke(this, new ObservedListElementRemovedEventArgs<T>(item));
            }

            return isRemoved;
        }

        public void RemoveAt(int index)
        {
            if (_backingList.Count > 0 && index < _backingList.Count)
            {
                _backingList.RemoveAt(index);

                var element = _backingList[index];
                ElementRemoved?.Invoke(this, new ObservedListElementRemovedEventArgs<T>(element));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _backingList.GetEnumerator();
        }

        public IObservableList<T> Mirror(IObservableList<T> observableList)
        {
            observableList.ElementAdded -= OnObservedListElementAdded;
            observableList.ElementAdded += OnObservedListElementAdded;

            observableList.ElementRemoved -= OnObservedListElementRemoved;
            observableList.ElementRemoved += OnObservedListElementRemoved;

            observableList.ElementModified -= OnObservedListElementModified;
            observableList.ElementModified += OnObservedListElementModified;

            return this;
        }

        private void OnObservedListElementModified(object sender, ObservedListElementModifiedEventArgs<T> e)
        {
            _backingList[e.Index] = e.Element;
        }

        private void OnObservedListElementRemoved(object sender, ObservedListElementRemovedEventArgs<T> e)
        {
            _backingList.Remove(e.Element);
        }

        private void OnObservedListElementAdded(object sender, ObservedListElementAddedEventArgs<T> args)
        {
            _backingList.Add(args.Element);
        }

        public IObservableList<T> UnMirror(IObservableList<T> observableList)
        {
            observableList.ElementAdded -= OnObservedListElementAdded;
            observableList.ElementRemoved -= OnObservedListElementRemoved;
            observableList.ElementModified -= OnObservedListElementModified;

            return this;
        }
    }
}
