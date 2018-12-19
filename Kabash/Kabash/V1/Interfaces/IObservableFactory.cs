namespace AOFL.Kabash.V1.Interfaces
{
    public interface IObservableFactory
    {
        IObservable<TValue> Create<TValue>();
    }
}
