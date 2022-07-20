

namespace ModelSMP.Ships.Behavior.Ship_States
{
    class ShipOnRouteState : State
    {
        public override State NextState()
        {
            return new ShipWandersState();
        }

        public override void OnEntry(ShipBehavior sb)
        {
            sb.Engine.StartEngine();
            sb.Navigation.OnEndRoute += sb.GoNextState;
            sb.Navigation.OnEndRoute += sb.Engine.StopEngine;
        }

        public override void OnExit(ShipBehavior sb)
        {
            sb.Navigation.OnEndRoute -= sb.GoNextState;
            sb.Navigation.OnEndRoute -= sb.Engine.StopEngine;
        }


    }
}
