

namespace ModelSMP.Ships.Behavior.Ship_States
{
    class ShipWaitingForLoadingState : State
    {
        public static event StateChangedEventHadler? CheckNodeForLoading;
        public override State NextState()
        {
            return new ShipOnLoadingState();
        }
        public override void OnEntry(ShipBehavior sb)
        {
            CheckNodeForLoading?.Invoke();
            Console.WriteLine("Корабль успешно поменял свое состояние на " + sb.State);

        }

        public override void OnExit(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно вышел из состояния" + sb.State);
        }
    }
}
