using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public override void OnExit(ShipBehavior sb)
        {
            Console.WriteLine("Корабль успешно вышел из состояния" + sb.State);
        }
    }
}
