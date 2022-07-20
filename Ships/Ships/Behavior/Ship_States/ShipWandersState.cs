

namespace ModelSMP.Ships.Behavior.Ship_States
{
    class ShipWandersState : State
    {
        public override State NextState()
        {
            return new ShipInNodeState();
        }

        public override void OnEntry(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно поменял свое состояние на " + sb.State);
            if(sb.Navigation.CurrentNode != null)
                sb.Navigation.CurrentNode.ShipTryEnterInNode(sb.Ship);
            Console.WriteLine($"Текущий нод корабля {sb.Navigation.CurrentNode}");
        }

        public override void OnExit(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно вышел из состояния" + sb.State);
        }
    }
}
