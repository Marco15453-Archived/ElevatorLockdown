using Exiled.API.Features;
using Exiled.API.Extensions;
using MEC;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using System.Text.RegularExpressions;

namespace ElevatorLockdown 
{
    internal sealed class ServerHandler 
    {
        private IEnumerator<float> startLockdown() 
        {
            for (;; ) 
            {
                int startdelay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.DelayMin, ElevatorLockdown.Instance.Config.DelayMax);
                yield return Timing.WaitForSeconds(startdelay);

                int a = UnityEngine.Random.Range(1, 100);
                int b = UnityEngine.Random.Range(1, 100);
                int LCZA = UnityEngine.Random.Range(1, 100);
                int LCZB = UnityEngine.Random.Range(1, 100);
                int Nuke = UnityEngine.Random.Range(1, 100);
                int Scp049 = UnityEngine.Random.Range(1, 100);

                if (a <= ElevatorLockdown.Instance.Config.GateAFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA)) 
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.GateA);
                if (b <= ElevatorLockdown.Instance.Config.GateBFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) 
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.GateB);
                if (LCZA <= ElevatorLockdown.Instance.Config.LCZAFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.LczA))
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.LczA);
                if (LCZB <= ElevatorLockdown.Instance.Config.LCZBFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.LczB))
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.LczB);
                if (Nuke <= ElevatorLockdown.Instance.Config.NukeFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.Nuke))
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.Nuke);
                if (Scp049 <= ElevatorLockdown.Instance.Config.Scp049FailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.Scp049))
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.Scp049);

                string broadcastMsg = ElevatorLockdown.Instance.Config.GlobalBroadcastMessage;
                string cassieMsg = ElevatorLockdown.Instance.Config.CassieMessage;
                string gateNames = string.Empty;

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    gateNames += $"{CassieReadable(type).Trim()}, ";
                gateNames = gateNames.Remove(gateNames.LastIndexOf(','));
                broadcastMsg = broadcastMsg.Replace("{ELEVATOR}", gateNames);
                cassieMsg = cassieMsg.Replace("{ELEVATOR}", gateNames).Replace(",", string.Empty).Replace("-", " ");

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0 && broadcastMsg != null)
                    Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, broadcastMsg, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassieMsg);

                int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.LockdownTimeMin, ElevatorLockdown.Instance.Config.LockdownTimeMax);
                yield return Timing.WaitForSeconds(random_delay);

                // Reactive the Elevators
                string broadcastMsgde = ElevatorLockdown.Instance.Config.GlobalBroadcastMessageReactivated;
                string cassieMsgde = ElevatorLockdown.Instance.Config.CassieMessageReactivated;
                string gateNamesde = string.Empty;

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    gateNamesde += $"{CassieReadable(type).Trim()}, ";
                gateNamesde = gateNamesde.Remove(gateNamesde.LastIndexOf(','));
                broadcastMsgde = broadcastMsgde.Replace("{ELEVATOR}", gateNamesde);
                cassieMsgde = cassieMsgde.Replace("{ELEVATOR}", gateNamesde).Replace(",", string.Empty);

                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateA);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateB);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.LczA))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.LczA);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.LczB))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.LczB);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.Nuke))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.Nuke);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.Scp049))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.Scp049);

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0 && broadcastMsgde != null) 
                    Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, broadcastMsgde, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassieMsgde);
            }
        }

        private CoroutineHandle coroutine;
        public void onRoundStarted() 
        {
            if (coroutine != null && coroutine.IsRunning) Timing.KillCoroutines(coroutine);

            coroutine = Timing.RunCoroutine(startLockdown());
        }

        private string CassieReadable(ElevatorType type) 
        {
            switch (type)
            {
                case ElevatorType.GateA:
                    return "Gate A";
                case ElevatorType.GateB:
                    return "Gate B";
                case ElevatorType.LczA:
                    return "Light Containment Zone A";
                case ElevatorType.LczB:
                    return "Light Containment Zone B";
                case ElevatorType.Nuke:
                    return "Nuke";
                case ElevatorType.Scp049:
                    return "SCP 0 4 9";
                default:
                    return "";
            }
        }
    }
}
