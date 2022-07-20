

namespace ModelSMP.Ships.Behavior.Ship_States
{
    abstract class State
    {
        public delegate void StateChangedEventHadler();
        public abstract void OnEntry(ShipBehavior sb);
        public abstract void OnExit(ShipBehavior sb);
        public abstract State NextState();
    }
}
