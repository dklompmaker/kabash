using AOFL.Kabash.V1.Events;
using System;

namespace AOFL.Kabash.V1.Interfaces
{
    public interface IObservable<TValue>
    {
        event ObservedValueChangedHandler<TValue> ValueChanged;

        TValue Value { get; set; }

        /// <summary>
        /// Mirrors the value of the specified
        /// observable.
        /// </summary>
        /// <param name="observable"></param>
        IObservable<TValue> Mirror(IObservable<TValue> observable);

        /// <summary>
        /// Removes the mirroring of the target observable.
        /// </summary>
        /// <param name="observable"></param>
        IObservable<TValue> UnMirror(IObservable<TValue> observable);

        /// <summary>
        /// Provides a query to check against a changed value. The callback is invoked
        /// when the query meets the criteria.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        IObservable<TValue> When(Func<TValue, bool> query, ObservedCriteriaMetHandler<TValue> callback);

        /// <summary>
        /// Removes the specified query from the callbacks.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        IObservable<TValue> RemoveQuery(Func<TValue, bool> query);
    }
}
