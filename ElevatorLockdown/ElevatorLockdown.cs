using ElevatorLockdown.Events;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;

namespace ElevatorLockdown 
{
    public class ElevatorLockdown : Plugin<Config> 
    {
        internal static ElevatorLockdown Instance;

        public override string Author => "Marco15453";
        public override string Name => "ElevatorLockdown";
        public override Version Version => new Version(1, 3, 0);
        public override Version RequiredExiledVersion => new Version(3, 0, 0);

        private ServerHandler serverHandler;
        private PlayerHandler playerHandler;

        public HashSet<ElevatorType> disabledElevators = new HashSet<ElevatorType>();
        public HashSet<string> Elevators = new HashSet<string> { "gatea", "gateb", "lcza", "lczb", "nuke", "scp049" };
        public Dictionary<string, ElevatorType> StringToElevator = new Dictionary<string, ElevatorType>()
        {
            { "gatea", ElevatorType.GateA},
            { "gateb", ElevatorType.GateB },
            { "lcza", ElevatorType.LczA},
            { "lczb", ElevatorType.LczB},
            { "nuke", ElevatorType.Nuke},
            { "scp049", ElevatorType.Scp049}
        };

        public override void OnEnabled() 
        {
            Instance = this;
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled() 
        {
            UnregisterEvents();
            base.OnDisabled();
        }


        private void RegisterEvents() 
        {
            serverHandler = new ServerHandler();
            playerHandler = new PlayerHandler();

            // Server
            Exiled.Events.Handlers.Server.RoundStarted += serverHandler.onRoundStarted;

            // Player
            Exiled.Events.Handlers.Player.InteractingElevator += playerHandler.onInteractingElevator;
        }

        private void UnregisterEvents() 
        {
            // Server
            Exiled.Events.Handlers.Server.RoundStarted -= serverHandler.onRoundStarted;

            // Player
            Exiled.Events.Handlers.Player.InteractingElevator -= playerHandler.onInteractingElevator;

            serverHandler = null;
            playerHandler = null;
        }
    }
}
