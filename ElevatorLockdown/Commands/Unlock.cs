using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace ElevatorLockdown.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Unlock : ICommand
    {
        public string Command { get; } = "eunlock";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Unlocks Elevators";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender != null && !sender.CheckPermission("el.unlock"))
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

            foreach (var argument in arguments)
            {
                if (ElevatorLockdown.Instance.Elevators.Contains(argument.ToLower()) && ElevatorLockdown.Instance.DisabledElevators.Contains(Extensions.ElevatorToType(argument.ToLower())))
                {
                    ElevatorLockdown.Instance.DisabledElevators.Remove(Extensions.ElevatorToType(argument.ToLower()));
                    elevators += $"{Extensions.CassieReadable(Extensions.ElevatorToType(argument.ToLower())).Trim()}, ";
                }
                else notexist += $"{argument.ToLower()}, ";
            }

            if (string.IsNullOrEmpty(notexist))
            {
                response = $"{notexist.Remove(notexist.LastIndexOf(","))} are not valid Elevators or was already enabled!";
                return false;
            }

            Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("%ELEVATOR%", elevators).Replace(",", string.Empty));
            response = $"{elevators} Elevators was enabled by Administrator";
            return true;
        }
    }
}
