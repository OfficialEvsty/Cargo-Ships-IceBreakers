using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSMP.Ships.Behavior.Ship_States
{
    class SearchProfitRouteState : State
    {
        public override State NextState()
        {
            return new ShipOnRouteState();
        }

        public override void OnEntry(ShipBehavior sb)
        {
            sb.Navigation.SetToNode(sb.GetFraghtInfo().ToNode);

            sb.Navigation.InstallRoute();
            if (sb.Navigation.ChosenRoute != null)
                sb.GoNextState();
        }

        public override void OnExit(ShipBehavior sb)
        {
            if(sb.Navigation.FromNode != null)
                sb.Navigation.FromNode.ShipLeaveNode(sb.Ship);    
        }
    }
}
