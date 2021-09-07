using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorLockdown.Commands 
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Unlock : ICommand 
    {
        public string Command { get; } = "eunlock";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Unlocks an Elevator";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            if (p != null && !p.CheckPermission("el.unlock"))
            {
                response = "You need the 'el.unlock' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty())
            {
                response = "Use elist to get all available elevators";
                return false;
            }

            string elevators = string.Empty;
            string notexist = string.Empty;

            foreach (var argument in arguments.ToList())
            {
                if (ElevatorLockdown.Instance.Elevators.Contains(argument.ToLower()) && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorToType(argument.ToLower())))
                {
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorToType(argument.ToLower()));
                    elevators += $"{CassieReadable(ElevatorToType(argument.ToLower())).Trim()}, ";
                }
                else
                {
                    notexist += $"{argument.ToLower()}, ";
                }
            }

            if (notexist != null)
            {
                response = $"{notexist.Remove(notexist.LastIndexOf(","))} are not valid Elevators!";
                return false;
            }

            Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("{ELEVATOR}", elevators).Replace(",", string.Empty));
            response = $"{elevators} Elevators was enabled by Administrator";
            return true;
        }

        private ElevatorType ElevatorToType(string str)
        {
            switch (str)
            {
                case "gatea":
                    return ElevatorType.GateA;
                case "gateb":
                    return ElevatorType.GateB;
                case "lcza":
                    return ElevatorType.LczA;
                case "lczb":
                    return ElevatorType.LczB;
                case "nuke":
                    return ElevatorType.Nuke;
                case "scp049":
                    return ElevatorType.Scp049;
                default:
                    return ElevatorType.Unknown;
            }
        }

        private string CassieReadable(ElevatorType type)
        {
            switch (type)
            {
                case ElevatorType.GateA:
                    return "Gate A";
                case ElevatorType.GateB:
                    return "Gate B";
                case ElevatorType.LczA:
                    return "Light Containment Zone A";
                case ElevatorType.LczB:
                    return "Light Containment Zone B";
                case ElevatorType.Nuke:
                    return "Nuke";
                case ElevatorType.Scp049:
                    return "SCP 0 4 9";
                default:
                    return "";
            }
        }
    }
}
