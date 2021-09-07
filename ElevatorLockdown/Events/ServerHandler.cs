using Exiled.API.Features;
using Exiled.API.Extensions;
using MEC;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;

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

                string cassie_message = ElevatorLockdown.Instance.Config.GlobalBroadcastMessage;
                string broad_message = ElevatorLockdown.Instance.Config.CassieMessage;

                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA) && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                    cassie_message = cassie_message.Replace("{GATE}", "Gate A");
                    broad_message = broad_message.Replace("{GATE}", "Gate A");
                } else if (!ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA) && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                    cassie_message = cassie_message.Replace("{GATE}", "Gate B");
                    broad_message = broad_message.Replace("{GATE}", "Gate B");
                } else if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA) && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                    cassie_message = cassie_message.Replace("{GATE}", "Gate A and Gate B");
                    broad_message = broad_message.Replace("{GATE}", "Gate A and Gate B");
                }

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0) Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, broad_message, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassie_message);

                int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.LockdownTimeMin, ElevatorLockdown.Instance.Config.LockdownTimeMax);
                yield return Timing.WaitForSeconds(random_delay);

                // Reactive the Elevators

                string cassie_messagede = ElevatorLockdown.Instance.Config.GlobalBroadcastMessageReactivated;
                string broad_messagede = ElevatorLockdown.Instance.Config.CassieMessageReactivated;

                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA) && !ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                    cassie_messagede = cassie_messagede.Replace("{GATE}", "Gate A");
                    broad_messagede = broad_messagede.Replace("{GATE}", "Gate A");
                } else if (!ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB) && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) {
                    cassie_messagede = cassie_messagede.Replace("{GATE}", "Gate B");
                    broad_messagede = broad_messagede.Replace("{GATE}", "Gate B");
                } else if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB) && ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA)) {
                    cassie_messagede = cassie_messagede.Replace("{GATE}", "Gate A and Gate B");
                    broad_messagede = broad_messagede.Replace("{GATE}", "Gate A and Gate B");
                }

                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateA)) ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateA);
                if (ElevatorLockdown.Instance.disabledElevators.Contains(ElevatorType.GateB)) ElevatorLockdown.Instance.disabledElevators.Remove(ElevatorType.GateB);

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0) Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, broad_messagede, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassie_messagede);
            }
        }

        private CoroutineHandle coroutine;
        public void onRoundStarted() {
            if (coroutine != null && coroutine.IsRunning) Timing.KillCoroutines(coroutine);

            coroutine = Timing.RunCoroutine(startLockdown());
        }
    }
}
