using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ElevatorLockdown 
{
    public sealed class Config : IConfig 
    {
        [Description("Should the plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("How much time that needs to pass before the elevator lockdown (Seconds)")]
        public int DelayMin { get; set; } = 300;
        public int DelayMax { get; set; } = 500;

        [Description("What is the Chance of a Elevator Failure? 100 means everytime, 0 = disabled")]
        public int GateAFailureChance { get; set; } = 50;
        public int GateBFailureChance { get; set; } = 50;
        public int LCZAFailureChance { get; set; } = 50;
        public int LCZBFailureChance { get; set; } = 50;
        public int NukeFailureChance { get; set; } = 50;
        public int Scp049FailureChance { get; set; } = 50;

        [Description("How long the elevator is deactivated")]
        public int LockdownTimeMax { get; set; } = 30;
        public int LockdownTimeMin { get; set; } = 60;

        [Description("Cassie message if an elevator gets deactivated? {ELEVATOR} will be replaced with the Elevator Names")]
        public string CassieMessage { get; set; } = "{ELEVATOR} elevator critical power failure";
        
        [Description("Cassie message if an elevator gets reactivated? {ELEVATOR} will be replaced with the Elevator Names")]
        public string CassieMessageReactivated { get; set; } = "{ELEVATOR} elevator is back in operational mode";

        [Description("How long should the broadcast be displayed? (-1 disables it)")]
        public ushort HintTime { get; set; } = 3;

        [Description("What message should be displayed when player trys to call/use a deactivated Elevator?")]
        public string HintMessage { get; set; } = "<color=red>The Elevator has a malfunction!</color>";

        [Description("How long should the global broadcast be displayed? (-1 disables it)")]
        public ushort GlobalBroadcastTime { get; set; } = 5;

        [Description("What message should be global broadcasted when a elevator gets deactivated? {ELEVATOR} will be replaced with the Elevator Names")]
        public string GlobalBroadcastMessage { get; set; } = "<color=red>{ELEVATOR}</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>";

        [Description("What message should be global broadcasted when a elevator gets reactivated? {ELEVATOR} will be replaced with the Elevator Names")]
        public string GlobalBroadcastMessageReactivated { get; set; } = "<color=red>{GELEVATORATE}</color> <color=green>Elevator back in operational mode</color>";

    }
}
