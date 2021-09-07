using Exiled.API.Extensions;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using System.Collections.Generic;

namespace ElevatorLockdown.Events {
    internal sealed class PlayerHandler {

        public void onInteractingElevator(InteractingElevatorEventArgs ev) {
            HashSet<Lift> disabledElevators = ElevatorLockdown.Instance.disabledElevators;

            if(disabledElevators.Contains(ev.Lift)) {
                if (ElevatorLockdown.Instance.Config.HintTime > 0) ev.Player.ShowHint(ElevatorLockdown.Instance.Config.HintMessage, ElevatorLockdown.Instance.Config.HintTime);
                ev.IsAllowed = false;
            }
        }
    }
}
