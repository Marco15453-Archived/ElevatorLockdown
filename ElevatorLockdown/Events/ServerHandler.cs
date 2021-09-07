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

                HashSet<Lift> disabledElevators = ElevatorLockdown.Instance.disabledElevators;

                Lift gatea = Map.Lifts.First(e => e.Type() == ElevatorType.GateA);
                Lift gateb = Map.Lifts.First(e => e.Type() == ElevatorType.GateB);

                int a = UnityEngine.Random.Range(1, 100);
                int b = UnityEngine.Random.Range(1, 100);

                if (a <= ElevatorLockdown.Instance.Config.GateAFailureChance && !disabledElevators.Contains(gatea)) ElevatorLockdown.Instance.disabledElevators.Add(gatea);
                if (b <= ElevatorLockdown.Instance.Config.GateBFailureChance && !disabledElevators.Contains(gateb)) ElevatorLockdown.Instance.disabledElevators.Add(gateb);

                string cassie_message = ElevatorLockdown.Instance.Config.GlobalBroadcastMessage;
                string broad_message = ElevatorLockdown.Instance.Config.CassieMessage;

                if (disabledElevators.Contains(gatea) && !disabledElevators.Contains(gateb)) {
                    cassie_message.Replace("{GATE}", "Gate A");
                    broad_message.Replace("{GATE}", "Gate A");
                } else if (!disabledElevators.Contains(gateb) && disabledElevators.Contains(gateb)) {
                    cassie_message.Replace("{GATE}", "Gate B");
                    broad_message.Replace("{GATE}", "Gate B");
                } else if (disabledElevators.Contains(gateb) && disabledElevators.Contains(gatea)) {
                    cassie_message.Replace("{GATE}", "Gate A and Gate B");
                    broad_message.Replace("{GATE}", "Gate A and Gate B");
                }

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0) Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, broad_message, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassie_message);

                int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.LockdownTimeMin, ElevatorLockdown.Instance.Config.LockdownTimeMax);
                yield return Timing.WaitForSeconds(random_delay);

                // Reactive the Elevators

                string cassie_messagede = ElevatorLockdown.Instance.Config.GlobalBroadcastMessageReactivated;
                string broad_messagede = ElevatorLockdown.Instance.Config.CassieMessageReactivated;

                if (disabledElevators.Contains(gatea) && !disabledElevators.Contains(gateb)) {
                    cassie_messagede.Replace("{GATE}", "Gate A");
                    broad_messagede.Replace("{GATE}", "Gate A");
                } else if (!disabledElevators.Contains(gateb) && disabledElevators.Contains(gateb)) {
                    cassie_messagede.Replace("{GATE}", "Gate B");
                    broad_messagede.Replace("{GATE}", "Gate B");
                } else if (disabledElevators.Contains(gateb) && disabledElevators.Contains(gatea)) {
                    cassie_messagede.Replace("{GATE}", "Gate A and Gate B");
                    broad_messagede.Replace("{GATE}", "Gate A and Gate B");
                }

                if (disabledElevators.Contains(gatea)) disabledElevators.Remove(gatea);
                if (disabledElevators.Contains(gateb)) disabledElevators.Remove(gateb);

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0) Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, broad_messagede, Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(cassie_messagede);
            }
        }

        public void onRoundStarted() {
            Timing.RunCoroutine(startLockdown());
        }
    }
}
