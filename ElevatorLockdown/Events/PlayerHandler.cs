using Exiled.API.Extensions;
using Exiled.Events.EventArgs;

namespace ElevatorLockdown.Events 
{
    internal sealed class PlayerHandler 
    {
        public void OnInteractingElevator(InteractingElevatorEventArgs ev) {
            if (!ElevatorLockdown.Instance.DisabledElevators.Contains(ev.Lift.Type())) return;

            if (ElevatorLockdown.Instance.Config.HintTime > 0) 
                    ev.Player.ShowHint(ElevatorLockdown.Instance.Config.HintMessage, ElevatorLockdown.Instance.Config.HintTime);
            ev.IsAllowed = false;
        }
    }
}
