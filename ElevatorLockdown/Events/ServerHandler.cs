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
        private IEnumerator<float> LockDownElevator(int delay) {
            yield return Timing.WaitForSeconds(delay);

            Lift gatea = Map.Lifts.First(e => e.Type() == ElevatorType.GateA);
            Lift gateb = Map.Lifts.First(e => e.Type() == ElevatorType.GateB);

            int r = UnityEngine.Random.Range(1, 2);

            if(r == 1 && gatea.enabled) { // Gate A
                gatea.enabled = false;
                if (ElevatorLockdown.Instance.Config.global_broadcast) {
                    foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message.Replace("{GATE}", "Gate A"), Broadcast.BroadcastFlags.Normal, true);
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate A"));
            } else if(r == 2 && gateb.enabled) { // Gate B
                gateb.enabled = false;
                if (ElevatorLockdown.Instance.Config.global_broadcast) {
                    foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message.Replace("{GATE}", "Gate B"), Broadcast.BroadcastFlags.Normal, true);
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate B"));
            }

            int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.lockdown_time_min, ElevatorLockdown.Instance.Config.lockdown_time_max);
            Timing.RunCoroutine(UnlockElevator(random_delay));
        }

        private IEnumerator<float> UnlockElevator(int delay) {
            yield return Timing.WaitForSeconds(delay);

            Lift gatea = Map.Lifts.First(e => e.Type() == ElevatorType.GateA);
            Lift gateb = Map.Lifts.First(e => e.Type() == ElevatorType.GateB);

            if(!gatea.enabled) {
                gatea.enabled = true;
                if (ElevatorLockdown.Instance.Config.global_broadcast) {
                    foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message_reactivated.Replace("{GATE}", "Gate A"), Broadcast.BroadcastFlags.Normal, true);
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message_reactivate.Replace("{GATE}", "Gate A"));
            } else if(!gateb.enabled) {
                gateb.enabled = true;
                if (ElevatorLockdown.Instance.Config.global_broadcast) {
                    foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message_reactivated.Replace("{GATE}", "Gate B"), Broadcast.BroadcastFlags.Normal, true);
                }
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message_reactivate.Replace("{GATE}", "Gate B"));
            }

            int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.delay_min, ElevatorLockdown.Instance.Config.delay_max);
            Timing.RunCoroutine(LockDownElevator(random_delay));
        }

        public void onRoundStarted() {
            int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.first_delay_min, ElevatorLockdown.Instance.Config.first_delay_max);
            Timing.RunCoroutine(LockDownElevator(random_delay));
        }
    }
}
