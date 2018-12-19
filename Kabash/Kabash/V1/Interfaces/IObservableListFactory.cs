using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOFL.Kabash.V1.Interfaces
{
    public interface IObservableListFactory
    {
        IObservableList<T> Create<T>();
    }
}
