
namespace ModelSMP.PatternObserver.Observer
{
    public interface IEventObserver<Event> : IEventObservableBase where Event : class
    {
        public void Update(Event ev);
    }
}
