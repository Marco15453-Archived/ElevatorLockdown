using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace ElevatorLockdown.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class List : ICommand
    {
        public string Command { get; } = "elist";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "List of all available Elevators";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender != null && !sender.CheckPermission("el.list"))
            {
                response = "You need the 'el.list' permission to use this Command!";
                return false;
            }

            response = "\nAvailable Elevators:\n";
            foreach(var item in ElevatorLockdown.Instance.Elevators)
                response += $"- {item}\n";
            return true;
        }
    }
}
