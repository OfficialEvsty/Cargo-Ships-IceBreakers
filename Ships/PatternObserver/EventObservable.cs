using ModelSMP.Edges;
using ModelSMP.PatternObserver.Observer;

namespace ModelSMP.PatternObserver
{
    public interface IEventObservableBase { }

    public static class EventObservable
    {
        private static Dictionary<Type, List<IEventObservableBase>> m_subscribers = new Dictionary<Type, List<IEventObservableBase>>();

        public static void AddEventObserver<Event>(IEventObserver<Event> subscriber) where Event : class
        {
            if (m_subscribers.ContainsKey(typeof(Event)))
                m_subscribers[typeof(Event)].Add(subscriber);
            else
                m_subscribers.Add(typeof(Event), new List<IEventObservableBase>() { subscriber }) ;
        }
        public static void NotifyObservers<Event>(Event ev) where Event : class
        {
            var subscribers = m_subscribers[typeof(Event)];
            foreach (var subscriber in subscribers)
            {
                ((IEventObserver<Event>)subscriber).Update(ev);
            }
        }
        public static void RemoveEventObserver<Event>(IEventObserver<Event> subscriber) where Event : class
        {
            m_subscribers[typeof(Event)].Remove(subscriber);
        }
    }
}
