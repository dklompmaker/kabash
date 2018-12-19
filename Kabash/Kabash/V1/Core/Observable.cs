using System;
using System.Collections.Generic;
using AOFL.Kabash.V1.Events;
using AOFL.Kabash.V1.Interfaces;

namespace AOFL.Kabash.V1.Core
{
    /// <inheritdoc/>
    public class Observable<TValue> : IObservable<TValue>
    {
        private TValue _value;
        private Dictionary<Func<TValue, bool>, ObservedCriteriaMetHandler<TValue>> _whenCallbackHandlers = new Dictionary<Func<TValue, bool>, ObservedCriteriaMetHandler<TValue>>();

        public Observable() { }

        public Observable(TValue defaultValue)
        {
            Value = defaultValue;
        }

        public TValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!EqualityComparer<TValue>.Default.Equals(_value, value))
                {
                    var previousValue = _value;

                    _value = value;

                    CheckWhenQueries();

                    ValueChanged?.Invoke(this, new ObservedValueChangedEventArgs<TValue>(previousValue, _value));
                }
            }
        }

        public static implicit operator Observable<TValue>(TValue value)
        {
            return new Observable<TValue>(value);
        }

        public event ObservedValueChangedHandler<TValue> ValueChanged;

        public IObservable<TValue> Mirror(IObservable<TValue> observable)
        {
            Value = observable.Value;

            observable.ValueChanged -= OnMirroredObservableChanged;
            observable.ValueChanged += OnMirroredObservableChanged;

            return this;
        }

        public IObservable<TValue> UnMirror(IObservable<TValue> observable)
        {
            observable.ValueChanged -= OnMirroredObservableChanged;

            return this;
        }

        public IObservable<TValue> When(Func<TValue, bool> query, ObservedCriteriaMetHandler<TValue> callback)
        {
            _whenCallbackHandlers[query] = callback;

            // Immediately call if the query returns true...
            if(query.Invoke(Value))
            {
                callback.Invoke(this, new ObservedCriteriaMetEventArgs<TValue>());
            }

            return this;
        }

        public IObservable<TValue> RemoveQuery(Func<TValue, bool> query)
        {
            _whenCallbackHandlers.Remove(query);

            return this;
        }

        private void OnMirroredObservableChanged(object sender, ObservedValueChangedEventArgs<TValue> e)
        {
            Value = e.CurrentValue;
        }

        private void CheckWhenQueries()
        {
            foreach(var pair in _whenCallbackHandlers)
            {
                if(pair.Key.Invoke(Value))
                {
                    pair.Value?.Invoke(this, new ObservedCriteriaMetEventArgs<TValue>());
                }
            }
        }
    }
}
