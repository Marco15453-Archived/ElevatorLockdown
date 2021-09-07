using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorLockdown.Commands {
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Unlock : ICommand {
        public string Command { get; } = "eunlock";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Reactivates GateA or GateB Elevator";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            if (p != null && !p.CheckPermission("el.unlock")) {
                response = "You need the 'el.unlock' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty()) {
                response = "You must provide the argument 'GateA' or 'GateB'";
                return false;
            }

            if(arguments.At(0) == "gatea" && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA)) {
                Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("{GATE}", "Gate A"));
                ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateA);
                response = "Gate A Lift has been reactivated by Administrator!";
                return true;
            } else if(arguments.At(0)== "gateb" && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("{GATE}", "Gate B"));
                ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateB);
                response = "Gate B Lift has been reactivated by Administrator!";
                return true;
            } else {
                response = $"{arguments.At(1)} is already deactivated by Automatic Failure System";
                return false;
            }
        }
    }
}
