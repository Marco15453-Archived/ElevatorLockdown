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
    public class List : ICommand
    {
        public string Command { get; } = "elist";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "List of all available Elevators";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player p = Player.Get((CommandSender)sender);

            if (p != null && !sender.CheckPermission("el.list"))
            {
                response = "You need the 'el.list' permission to use this Command!";
                return false;
            }

            string list = "";

            foreach(var item in ElevatorLockdown.Instance.Elevators)
                list += $"{item}\n";

            response = $"Available Elevators: \n{list}";
            return true;
        }
    }
}
