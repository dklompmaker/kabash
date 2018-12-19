using System;

namespace AOFL.Kabash.V1.Events
{
    public class ObservedListElementAddedEventArgs<T> : EventArgs
    {
        public T Element { get; set; }

        public ObservedListElementAddedEventArgs(T element)
        {
            Element = element;
        }
    }

    public delegate void ObservedListElementAddedHandler<T>(object sender, ObservedListElementAddedEventArgs<T> e);
}
