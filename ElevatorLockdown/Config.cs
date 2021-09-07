using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ElevatorLockdown {
    public sealed class Config : IConfig {
        [Description("Should the plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("How much time that needs to pass before the first elevator lockdown (Seconds)")]
        public int first_delay_min { get; set; } = 300;
        public int first_delay_max { get; set; } = 500;

        [Description("How much time that needs to pass before an elevator lockdown after the first elevator lockdowns down (Seconds)")]
        public int delay_min { get; set; } = 90;
        public int delay_max { get; set; } = 210;

        [Description("How long the elevator is deactivated")]
        public int lockdown_time_min { get; set; } = 30;
        public int lockdown_time_max { get; set; } = 60;

        [Description("Cassie message if an elevator gets deactivated? {GATE} will be replaced with the Gate Name")]
        public string cassie_message { get; set; } = "{GATE} elevator critical power failure";
        public string cassie_message_reactivate { get; set; } = "{GATE} elevator is back in operational mode";

        [Description("Should you get an broadcast if a player trys to call/use a deactivated Elevator?")]
        public bool broadcast { get; set; } = true;

        [Description("How long should the broadcast be displayed?")]
        public ushort broadcast_time { get; set; } = 3;

        [Description("What message should be displayed when player trys to call/use a deactivated Elevator? {GATE} will be replaced with the Gate Name")]
        public string broadcast_message { get; set; } = "<color=red>The Elevator has a malfunction!</color>";

        [Description("Should there be a global broadcast for all players when a Elevator gets deactivated and reactivated?")]
        public bool global_broadcast { get; set; } = true;

        [Description("How long should the global broadcast be displayed?")]
        public ushort global_broadcast_time { get; set; } = 5;

        [Description("What message should be global broadcasted when a elevator gets deactivated? {GATE} will be replaced with the Gate Name")]
        public string global_broadcast_message { get; set; } = "<color=red>{GATE}</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>";

        [Description("What message should be global broadcasted when a elevator gets reactivated? {GATE} will be replaced with the Gate Name")]
        public string global_broadcast_message_reactivated { get; set; } = "<color=red>{GATE}</color> <color=green>Elevator back in operational mode</color>";
    }
}
