using Exiled.API.Extensions;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using System.Collections.Generic;

namespace ElevatorLockdown.Events {
    internal sealed class PlayerHandler {
        public void onInteractingElevator(InteractingElevatorEventArgs ev) {
            if (!ElevatorLockdown.Instance.disabledElevators.Contains(ev.Lift.Type())) return;

            if (ElevatorLockdown.Instance.Config.HintTime > 0) 
                    ev.Player.ShowHint(ElevatorLockdown.Instance.Config.HintMessage, ElevatorLockdown.Instance.Config.HintTime);
            ev.IsAllowed = false;
        }
    }
}
