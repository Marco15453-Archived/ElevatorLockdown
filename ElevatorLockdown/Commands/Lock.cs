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
    public class Lock : ICommand {
        public string Command { get; } = "elock";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Locks GateA or GateB Elevator";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            if (p != null && !p.CheckPermission("el.lock")) {
                response = "You need the 'el.lock' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty()) {
                response = "You must provide the argument 'GateA' or 'GateB'";
                return false;
            }

            if(!ElevatorLockdown.Instance.Elevators.Contains(arguments.At(0))) {
                response = "This isn't a valid Elevator";
                return false;
            }

            if(arguments.At(0) == "GateA" && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA)) {
                Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessage.Replace("{GATE}", "Gate A"));
                ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.GateA);
                response = "Gate A Lift has been disabled by Administrator!";
                return true;
            } else if(arguments.At(0) == "GateB" && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessage.Replace("{GATE}", "Gate B"));
                ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.GateB);
                response = "Gate B Lift has been disabled by Administrator!";
                return true;
            } else {
                response = $"{arguments.At(0)} Lift is already disabled by Automatic Failure System";
                return false;
            }
        }
    }
}
