

namespace ModelSMP.Ships.Behavior.Ship_States
{
    
    class ShipOnLoadingState : State
    {
        public event StateChangedEventHadler? ReadyLoad;

        public override State NextState()
        {
            return new SearchProfitRouteState();
        }

        public override void OnEntry(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно поменял свое состояние на " + sb.State);
            PatternObserver.EventObservable.NotifyObservers(this);
            ReadyLoad?.Invoke();
        }

        public override void OnExit(ShipBehavior sb)
        {
            
            Console.WriteLine("Корабль успешно вышел из состояния" + sb.State);
        }
    }
}
