using ElevatorLockdown.Events;
using Exiled.API.Features;
using System;

namespace ElevatorLockdown {
    public class ElevatorLockdown : Plugin<Config> {
        internal static ElevatorLockdown Instance;

        public override string Author => "Marco15453";
        public override string Name => "ElevatorLockdown";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(2, 14, 0);

        private ServerHandler serverHandler;
        private PlayerHandler playerHandler;

        public override void OnEnabled() {
            Instance = this;
            registerEvents();
            base.OnEnabled();
        }

        public override void OnDisabled() {
            unregisterEvents();
            base.OnDisabled();
        }


        private void registerEvents() {
            serverHandler = new ServerHandler();
            playerHandler = new PlayerHandler();

            // Server
            Exiled.Events.Handlers.Server.RoundStarted += serverHandler.onRoundStarted;

            // Player
            Exiled.Events.Handlers.Player.InteractingElevator += playerHandler.onInteractingElevator;
        }

        private void unregisterEvents() {
            // Server
            Exiled.Events.Handlers.Server.RoundStarted -= serverHandler.onRoundStarted;

            // Player
            Exiled.Events.Handlers.Player.InteractingElevator -= playerHandler.onInteractingElevator;

            serverHandler = null;
            playerHandler = null;
        }
    }
}
