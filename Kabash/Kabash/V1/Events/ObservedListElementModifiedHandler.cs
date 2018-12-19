using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOFL.Kabash.V1.Events
{
    public class ObservedListElementModifiedEventArgs<T> : EventArgs
    {
        public int Index { get; set; }
        public T Element { get; set; }

        public ObservedListElementModifiedEventArgs(int index, T element)
        {
            Index = index;
            Element = element;
        }
    }
    public delegate void ObservedListElementModifiedHandler<T>(object sender, ObservedListElementModifiedEventArgs<T> e);
}
