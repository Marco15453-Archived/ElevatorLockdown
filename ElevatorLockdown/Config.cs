using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ElevatorLockdown {
    public sealed class Config : IConfig {
        [Description("Should the plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("How much time that needs to pass before the elevator lockdown (Seconds)")]
        public int DelayMin { get; set; } = 300;
        public int DelayMax { get; set; } = 500;

        [Description("What is the Chance of a Gate Elevator Failure? 100 means everytime, 1 means very rarly")]
        public int GateAFailureChance { get; set; } = 50;
        public int GateBFailureChance { get; set; } = 50;

        [Description("How long the elevator is deactivated")]
        public int LockdownTimeMax { get; set; } = 30;
        public int LockdownTimeMin { get; set; } = 60;

        [Description("Cassie message if an elevator gets deactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)")]
        public string CassieMessage { get; set; } = "{GATE} elevator critical power failure";
        
        [Description("Cassie message if an elevator gets reactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)")]
        public string CassieMessageReactivated { get; set; } = "{GATE} elevator is back in operational mode";

        [Description("How long should the broadcast be displayed? (-1 disables it)")]
        public ushort HintTime { get; set; } = 3;

        [Description("What message should be displayed when player trys to call/use a deactivated Elevator? {GATE} will be replaced with the Gate Name")]
        public string HintMessage { get; set; } = "<color=red>The Elevator has a malfunction!</color>";

        [Description("How long should the global broadcast be displayed? (-1 disables it)")]
        public ushort GlobalBroadcastTime { get; set; } = 5;

        [Description("What message should be global broadcasted when a elevator gets deactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)")]
        public string GlobalBroadcastMessage { get; set; } = "<color=red>{GATE}</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>";

        [Description("What message should be global broadcasted when a elevator gets reactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)")]
        public string GlobalBroadcastMessageReactivated { get; set; } = "<color=red>{GATE}</color> <color=green>Elevator back in operational mode</color>";
    }
}
