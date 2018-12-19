using AOFL.Kabash.V1.Interfaces;
using System;

namespace AOFL.Kabash.V1.Events
{
    public class ObservedValueChangedEventArgs<TValue> : EventArgs
    {
        public TValue PreviousValue { get; private set; }
        public TValue CurrentValue { get; private set; }

        public ObservedValueChangedEventArgs(TValue previousValue, TValue currentValue)
        {
            PreviousValue = previousValue;
            CurrentValue = currentValue;
        }
    }

    public delegate void ObservedValueChangedHandler<TValue>(IObservable<TValue> sender, ObservedValueChangedEventArgs<TValue> e);
}
