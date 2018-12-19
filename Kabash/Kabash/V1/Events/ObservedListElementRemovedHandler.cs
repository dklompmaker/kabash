using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOFL.Kabash.V1.Events
{
    public class ObservedListElementRemovedEventArgs<T> : EventArgs
    {
        public T Element { get; set; }

        public ObservedListElementRemovedEventArgs(T element)
        {
            Element = element;
        }
    }

    public delegate void ObservedListElementRemovedHandler<T>(object sender, ObservedListElementRemovedEventArgs<T> e);
}
