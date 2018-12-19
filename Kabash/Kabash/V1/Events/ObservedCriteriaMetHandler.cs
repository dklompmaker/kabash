using AOFL.Kabash.V1.Interfaces;
using System;

namespace AOFL.Kabash.V1.Events
{
    public class ObservedCriteriaMetEventArgs<TValue> : EventArgs
    {

    }

    public delegate void ObservedCriteriaMetHandler<TValue>(IObservable<TValue> sender, ObservedCriteriaMetEventArgs<TValue> e);
}
