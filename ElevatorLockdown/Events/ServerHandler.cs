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

            int a = UnityEngine.Random.Range(1, 100);
            int b = UnityEngine.Random.Range(1, 100);

            if(a <= ElevatorLockdown.Instance.Config.gatea_failure_chance && gatea.enabled) gatea.enabled = false;
            if(b <= ElevatorLockdown.Instance.Config.gateb_failure_chance && gateb.enabled) gateb.enabled = false;

            if(!gatea.enabled && gateb.enabled) { // Gate A
                if (ElevatorLockdown.Instance.Config.global_broadcast) foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message.Replace("{GATE}", "Gate A"), Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate A"));
            } else if(gatea.enabled && !gateb.enabled) { // Gate B
                if (ElevatorLockdown.Instance.Config.global_broadcast) foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message.Replace("{GATE}", "Gate B"), Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate B"));
            } else if(!gatea.enabled && !gateb.enabled) { // Both Gates
                if (ElevatorLockdown.Instance.Config.global_broadcast) foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message.Replace("{GATE}", "Gate A and Gate B"), Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message.Replace("{GATE}", "Gate A and Gate B"));
            }

            int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.lockdown_time_min, ElevatorLockdown.Instance.Config.lockdown_time_max);
            Timing.RunCoroutine(UnlockElevator(random_delay));
        }

        private IEnumerator<float> UnlockElevator(int delay) {
            yield return Timing.WaitForSeconds(delay);

            Lift gatea = Map.Lifts.First(e => e.Type() == ElevatorType.GateA);
            Lift gateb = Map.Lifts.First(e => e.Type() == ElevatorType.GateB);

            if (!gatea.enabled && gateb.enabled) {
                if (ElevatorLockdown.Instance.Config.global_broadcast) foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message_reactivated.Replace("{GATE}", "Gate A"), Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message_reactivate.Replace("{GATE}", "Gate A"));
            } else if (gatea.enabled && !gateb.enabled) {
                if (ElevatorLockdown.Instance.Config.global_broadcast) foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message_reactivated.Replace("{GATE}", "Gate B"), Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message_reactivate.Replace("{GATE}", "Gate B"));
            } else if (!gatea.enabled && !gateb.enabled) {
                if (ElevatorLockdown.Instance.Config.global_broadcast) foreach (Player p in Player.List) p.Broadcast(ElevatorLockdown.Instance.Config.global_broadcast_time, ElevatorLockdown.Instance.Config.global_broadcast_message_reactivated.Replace("{GATE}", "Gate A and Gate B"), Broadcast.BroadcastFlags.Normal, true);
                Cassie.Message(ElevatorLockdown.Instance.Config.cassie_message_reactivate.Replace("{GATE}", "Gate A and Gate B"));
            }

            if (!gatea.enabled) gatea.enabled = true;
            if (!gateb.enabled) gateb.enabled = true;

            int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.delay_min, ElevatorLockdown.Instance.Config.delay_max);
            Timing.RunCoroutine(LockDownElevator(random_delay));
        }

        public void onRoundStarted() {
            int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.first_delay_min, ElevatorLockdown.Instance.Config.first_delay_max);
            Timing.RunCoroutine(LockDownElevator(random_delay));
        }
    }
}
