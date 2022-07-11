

namespace ModelSMP.Ships.Behavior.Ship_States
{
    abstract class State
    {
        public delegate void StateChangedEventHadler();
        public abstract void OnEntry(ShipBehavior sb);
        public abstract void OnExit(ShipBehavior sb);
        public abstract State NextState();



        private bool b_shipOnRoute;
        public bool IsShipOnRoute { get { return b_shipOnRoute; } }

        private bool b_shipInCaravan;
        public bool IsShipInCaravan { get { return b_shipInCaravan;} }

        private bool b_shipGoesToCaravan;
        public bool IsShipGoesToCaravan { get { return b_shipGoesToCaravan; } }

        private bool b_shipWaitOthersCaravan;
        public bool HasShipWaitOthersCaravan { get { return b_shipWaitOthersCaravan;} }
    }
}
