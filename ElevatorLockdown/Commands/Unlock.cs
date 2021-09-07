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
        public string Command { get; } = "el_unlock";
        public string[] Aliases { get; } = null;
        public string Description { get; } = "Reactivates GateA or GateB Elevator";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            HashSet<Lift> disabledElevators = ElevatorLockdown.Instance.disabledElevators;

            if (!p.CheckPermission("el.unlock") && sender is PlayerCommandSender) {
                response = "You need the 'el.unlock' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty()) {
                response = "You must provide the argument 'GateA' or 'GateB'";
                return false;
            }

            var argument = arguments.ToList();

            if(argument[0].ToLower() == "gatea") {
                Lift lift = Map.Lifts.First(e => e.Type() == ElevatorType.GateA);
                if(!disabledElevators.Contains(lift)) {
                    response = "Gate A Lift is already enabled by Automatic Failure System";
                    return false;
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("{GATE}", "Gate A"));
                disabledElevators.Remove(lift);
                response = "Gate A Lift has been reactivated by Administrator!";
                return true;
            } else if(argument[0].ToLower() == "gateb") {
                Lift lift = Map.Lifts.First(e => e.Type() == ElevatorType.GateB);
                if (!disabledElevators.Contains(lift)) {
                    response = "Gate B Lift is already enabled by Automatic Failure System";
                    return false;
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("{GATE}", "Gate B"));
                disabledElevators.Remove(lift);
                response = "Gate B Lift has been reactivated by Administrator!";
                return true;
            } else {
                response = "Argument must be 'GateA' or 'GateB'!";
                return false;
            }
        }
    }
}
