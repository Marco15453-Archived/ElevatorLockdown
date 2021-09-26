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
        public string Description { get; } = "Locks Elevators";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            if (p != null && !sender.CheckPermission("el.lock")) 
            {
                response = "You need the 'el.lock' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty()) 
            {
                response = "Use elist to get all available elevators";
                return false;
            }

            string elevators = string.Empty;
            string notexist = string.Empty;

            foreach (var argument in arguments)
            {
                if (ElevatorLockdown.Instance.Elevators.Contains(argument.ToLower()) && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorToType(argument.ToLower())))
                {
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorToType(argument.ToLower()));
                    elevators += $"{CassieReadable(ElevatorToType(argument.ToLower())).Trim()}, ";
                } else
                {
                    notexist += $"{argument.ToLower()}, ";
                }
            }

            if(string.IsNullOrEmpty(notexist))
            {
                response = $"{notexist.Remove(notexist.LastIndexOf(","))} are not valid Elevators or was already disabled!";
                return false;
            }

            Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessage.Replace("%ELEVATOR%", elevators).Replace(",", string.Empty));
            response = $"{elevators} Elevators was disabled by Administrator";
            return true;
        }

        private ElevatorType ElevatorToType(string str)
        {
            foreach (var readable in ElevatorLockdown.Instance.StringToElevator)
            {
                if (readable.Key == str) return readable.Value;
            }
            return ElevatorType.Unknown;
        }

        private string CassieReadable(ElevatorType type)
        {
            foreach (var readable in ElevatorLockdown.Instance.Config.CassieReadable)
            {
                if (readable.Key == type) return readable.Value;
            }
            return "";
        }
    }
}
