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

namespace ElevatorLockdown {
    internal sealed class ServerHandler {
        private IEnumerator<float> startLockdown() {
            for (;; ) {
                int startdelay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.DelayMin, ElevatorLockdown.Instance.Config.DelayMax);
                yield return Timing.WaitForSeconds(startdelay);

                int a = UnityEngine.Random.Range(1, 100);
                int b = UnityEngine.Random.Range(1, 100);

                if (a <= ElevatorLockdown.Instance.Config.GateAFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA)) 
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.GateA);
                if (b <= ElevatorLockdown.Instance.Config.GateBFailureChance && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) 
                    ElevatorLockdown.Instance.disabledElevators.Add(ElevatorType.GateB);

                string broadcastMsg = ElevatorLockdown.Instance.Config.GlobalBroadcastMessage;
                string cassieMsg = ElevatorLockdown.Instance.Config.CassieMessage;
                string gateNames = string.Empty;

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    gateNames += $"{Regex.Replace(type.ToString(), @"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1").Trim()}, ";
                gateNames = gateNames.Remove(gateNames.LastIndexOf(','));
                broadcastMsg = broadcastMsg.Replace("{ELEVATOR}", gateNames);
                cassieMsg = cassieMsg.Replace("{ELEVATOR}", gateNames).Replace(",", string.Empty);

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0 && broadcastMsg != null)
                    Map.Broadcast(3, broadcastMsg, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassieMsg);

                int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.LockdownTimeMin, ElevatorLockdown.Instance.Config.LockdownTimeMax);
                yield return Timing.WaitForSeconds(random_delay);

                // Reactive the Elevators
                string broadcastMsgde = ElevatorLockdown.Instance.Config.GlobalBroadcastMessageReactivated;
                string cassieMsgde = ElevatorLockdown.Instance.Config.CassieMessageReactivated;
                string gateNamesde = string.Empty;

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    gateNamesde += $"{Regex.Replace(type.ToString(), @"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1").Trim()}, ";
                gateNamesde = gateNamesde.Remove(gateNamesde.LastIndexOf(','));
                broadcastMsgde = broadcastMsgde.Replace("{ELEVATOR}", gateNamesde);
                cassieMsgde = cassieMsgde.Replace("{ELEVATOR}", gateNamesde).Replace(",", string.Empty);

                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateA);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB))
                    ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateB);

                if(ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0 && broadcastMsgde != null) 
                    Map.Broadcast(3, broadcastMsgde, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassieMsgde);
            }
        }

        private CoroutineHandle coroutine;
        public void onRoundStarted() {
            if (coroutine != null && coroutine.IsRunning) Timing.KillCoroutines(coroutine);

            coroutine = Timing.RunCoroutine(startLockdown());
        }
    }
}
