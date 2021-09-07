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
    public class Lock : ICommand 
    {
        public string Command { get; } = "elock";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Locks an Elevator";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            if (p != null && !p.CheckPermission("el.lock")) 
            {
                response = "You need the 'el.lock' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty()) 
            {
                response = "Use elist to get all available elevators";
                return false;
            }

            if(!ElevatorLockdown.Instance.Elevators.Contains(arguments.At(0).ToLower())) 
            {
                response = "This isn't a valid Elevator";
                return false;
            }

            foreach (var argument in arguments)
            {
                if (ElevatorLockdown.Instance.Elevators.Contains(argument.ToLower()) && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorToType(argument.ToLower())))
                {
                    Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessage.Replace("{ELEVATOR}", CassieReadable(ElevatorToType(argument.ToLower()))));
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorToType(argument.ToLower()));
                    response = $"{argument.ToLower()} Elevator has been disabled by Administrator!";
                    return true;
                } else
                {
                    response = $"{argument.ToLower()} Elevator was already disabled by Automatic Failure System";
                    return false;
                }
            }

            response = "";
            return false;
        }

        private ElevatorType ElevatorToType(string str)
        {
            switch(str)
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
