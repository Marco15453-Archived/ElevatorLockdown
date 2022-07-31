using ElevatorLockdown.Events;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorLockdown 
{
    public class ElevatorLockdown : Plugin<Config> 
    {
        internal static ElevatorLockdown Instance;

        public override string Author => "Marco15453";
        public override string Name => "ElevatorLockdown";
        public override Version Version => new Version(1, 10, 0);
        public override Version RequiredExiledVersion => new Version(5, 3, 0);

        private ServerHandler serverHandler;
        private PlayerHandler playerHandler;

        public HashSet<ElevatorType> DisabledElevators = new HashSet<ElevatorType>();
        public List<string> Elevators = StringToElevator.Keys.ToList();
        public static Dictionary<string, ElevatorType> StringToElevator = new Dictionary<string, ElevatorType>()
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
            registerEvents();
            base.OnEnabled();
        }

        public override void OnDisabled() 
        {
            unregisterEvents();
            base.OnDisabled();
        }


        private void registerEvents() 
        {
            serverHandler = new ServerHandler();
            playerHandler = new PlayerHandler();

            // Server
            Exiled.Events.Handlers.Server.RoundStarted += serverHandler.OnRoundStarted;

            // Player
            Exiled.Events.Handlers.Player.InteractingElevator += playerHandler.OnInteractingElevator;
        }

        private void unregisterEvents() 
        {
            // Server
            Exiled.Events.Handlers.Server.RoundStarted -= serverHandler.OnRoundStarted;

            // Player
            Exiled.Events.Handlers.Player.InteractingElevator -= playerHandler.OnInteractingElevator;

            serverHandler = null;
            playerHandler = null;
        }
    }
}
