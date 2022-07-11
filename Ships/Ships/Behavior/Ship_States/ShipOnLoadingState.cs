

namespace ModelSMP.Ships.Behavior.Ship_States
{
    class ShipOnLoadingState : State
    {
        public override State NextState()
        {
            return null;
        }

        public override void OnEntry(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно поменял свое состояние на " + sb.State);

        }

        public override void OnExit(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно вышел из состояния" + sb.State);
        }
    }
}
