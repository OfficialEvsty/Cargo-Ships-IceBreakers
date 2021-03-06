using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSMP.Ships.Behavior.Ship_States
{
    class ShipInNodeState : State
    {
        public override State NextState()
        {
            return new LookingForFraghtState();
        }

        public override void OnEntry(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно поменял свое состояние на " + sb.State);
            //временно
            if (sb.GetFraghtInfo() != null && sb.Navigation.ToNode == sb.Navigation.CurrentNode)
            {
                Console.WriteLine("Груз доставлен!!!");
                sb.BoardCargo.Unloading();
            }
                
            sb.GoNextState();
        }

        public override void OnExit(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно вышел из состояния" + sb.State);
        }
    }
}
