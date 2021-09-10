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

                foreach(var failures in ElevatorLockdown.Instance.Config.FailureChances)
                {
                    int random = UnityEngine.Random.Range(1, 100);
                    if (random <= failures.Value && !ElevatorLockdown.Instance.disabledElevators.Contains(failures.Key)) 
                        ElevatorLockdown.Instance.disabledElevators.Add(failures.Key);
                }

                string broadcastMsg = ElevatorLockdown.Instance.Config.GlobalBroadcastMessage;
                string cassieMsg = ElevatorLockdown.Instance.Config.CassieMessage;
                string gateNames = string.Empty;

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    gateNames += $"{CassieReadable(type).Trim()}, ";
                gateNames = gateNames.Remove(gateNames.LastIndexOf(','));
                broadcastMsg = broadcastMsg.Replace("%ELEVATOR%", gateNames);
                cassieMsg = cassieMsg.Replace("%ELEVATOR%", gateNames).Replace(",", string.Empty).Replace("-", " ");

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
                broadcastMsgde = broadcastMsgde.Replace("%ELEVATOR%", gateNamesde);
                cassieMsgde = cassieMsgde.Replace("%ELEVATOR%", gateNamesde).Replace(",", string.Empty);

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    ElevatorLockdown.Instance.disabledElevators.Remove(type);

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
            foreach(var readable in ElevatorLockdown.Instance.Config.CassieReadable)
            {
                if (readable.Key == type) return readable.Value;
            }
            return "";
        }
    }
}
