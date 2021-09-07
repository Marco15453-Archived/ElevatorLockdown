using Exiled.API.Extensions;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;

namespace ElevatorLockdown.Events {
    internal sealed class PlayerHandler {

        public void onInteractingElevator(InteractingElevatorEventArgs ev) {
            if (ev.Lift.enabled) return;

            if (ev.Lift.Type() == ElevatorType.GateA) {
                if (ElevatorLockdown.Instance.Config.broadcast) ev.Player.Broadcast(ElevatorLockdown.Instance.Config.broadcast_time, ElevatorLockdown.Instance.Config.broadcast_message.Replace("{GATE}", "Gate A"), Broadcast.BroadcastFlags.Normal, true);

                ev.IsAllowed = false;
                return;
            } else if (ev.Lift.Type() == ElevatorType.GateB) {
                if (ElevatorLockdown.Instance.Config.broadcast) ev.Player.Broadcast(ElevatorLockdown.Instance.Config.broadcast_time, ElevatorLockdown.Instance.Config.broadcast_message.Replace("{GATE}", "Gate B"), Broadcast.BroadcastFlags.Normal, true);

                ev.IsAllowed = false;
                return;
            }
        }
    }
}
