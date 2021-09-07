﻿using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using System.Linq;

namespace ElevatorLockdown.Commands {
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Lockdown : ICommand {
        public string Command { get; } = "lockdown";
        public string[] Aliases { get; } = null;
        public string Description { get; } = "Lockdown an GateA or GateB Elevator";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            Player p = Player.Get((CommandSender)sender);

            if (!p.CheckPermission("el.lockdown") && sender is PlayerCommandSender) {
                response = "You need the 'el.lockdown' permission to use this Command!";
                return false;
            }

            if (arguments.IsEmpty()) {
                response = "You must provide the argument 'GateA' or 'GateB'";
                return false;
            }

            var argument = arguments.ToList();

            if(argument[0].ToLower() == "gatea") {
                Lift lift = Map.Lifts.First(e => e.Type() == ElevatorType.GateA);
                if(!lift.enabled) {
                    response = "Gate A Lift is already disabled by Automatic Failure System";
                    return false;
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate A"));
                lift.enabled = false;
                response = "Gate A Lift has been disabled by Administrator!";
                return true;
            } else if(argument[0].ToLower() == "gateb") {
                Lift lift = Map.Lifts.First(e => e.Type() == ElevatorType.GateB);
                if (!lift.enabled) {
                    response = "Gate B Lift is already disabled by Automatic Failure System";
                    return false;
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate B"));
                lift.enabled = false;
                response = "Gate B Lift has been disabled by Administrator!";
                return true;
            } else {
                response = "Argument must be 'GateA' or 'GateB'!";
                return false;
            }
        }
    }
}
