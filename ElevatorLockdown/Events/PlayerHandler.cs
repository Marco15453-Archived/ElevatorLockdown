using Exiled.API.Extensions;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;

namespace ElevatorLockdown.Events {
    internal sealed class PlayerHandler {

        public void onInteractingElevator(InteractingElevatorEventArgs ev) {
            if (ev.Lift.enabled) return;

            if (ev.Lift.Type() == ElevatorType.GateA) {
                if (ElevatorLockdown.Instance.Config.hint) ev.Player.ShowHint(ElevatorLockdown.Instance.Config.hint_message.Replace("{GATE}", "Gate A"), ElevatorLockdown.Instance.Config.hint_time);
                
                ev.IsAllowed = false;
                return;
            } else if (ev.Lift.Type() == ElevatorType.GateB) {
                if (ElevatorLockdown.Instance.Config.hint) ev.Player.ShowHint(ElevatorLockdown.Instance.Config.hint_message.Replace("{GATE}", "Gate B"), ElevatorLockdown.Instance.Config.hint_time);

                ev.IsAllowed = false;
                return;
            }
        }
    }
}
