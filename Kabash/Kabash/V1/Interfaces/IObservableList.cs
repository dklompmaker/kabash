using AOFL.Kabash.V1.Events;
using System.Collections.Generic;

namespace AOFL.Kabash.V1.Interfaces
{
    public interface IObservableList<T> : IList<T>
    {
        event ObservedListElementAddedHandler<T> ElementAdded;
        event ObservedListElementRemovedHandler<T> ElementRemoved;
        event ObservedListElementModifiedHandler<T> ElementModified;

        /// <summary>
        /// Mirrors the elements in the observable list.
        /// </summary>
        /// <param name="observableList"></param>
        /// <returns></returns>
        IObservableList<T> Mirror(IObservableList<T> observableList);

        /// <summary>
        /// Removes the elements from the observable list.
        /// </summary>
        /// <param name="observableList"></param>
        /// <returns></returns>
        IObservableList<T> UnMirror(IObservableList<T> observableList);

        void AddRange(IEnumerable<T> collection);
    }
}
