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
            while (true) 
            {
                int startdelay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.DelayMin, ElevatorLockdown.Instance.Config.DelayMax);
                yield return Timing.WaitForSeconds(startdelay);

                foreach(var failures in ElevatorLockdown.Instance.Config.FailureChances)
                {
                    int random = UnityEngine.Random.Range(1, 100);
                    if (random <= failures.Value && !ElevatorLockdown.Instance.disabledElevators.Contains(failures.Key)) 
                        ElevatorLockdown.Instance.disabledElevators.Add(failures.Key);
                }

                string gateNames = string.Empty;

                foreach (ElevatorType type in ElevatorLockdown.Instance.disabledElevators)
                    gateNames += $"{CassieReadable(type).Trim()}, ";
                gateNames = gateNames.Remove(gateNames.LastIndexOf(','));

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0 && ElevatorLockdown.Instance.Config.GlobalBroadcastMessage != null)
                    Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, ElevatorLockdown.Instance.Config.GlobalBroadcastMessage.Replace("%ELEVATOR%", gateNames), Broadcast.BroadcastFlags.Normal, true);
                if(ElevatorLockdown.Instance.Config.CassieMessage != null) Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessage.Replace("%ELEVATOR%", gateNames).Replace(",", string.Empty));

                int random_delay = UnityEngine.Random.Range(ElevatorLockdown.Instance.Config.LockdownTimeMin, ElevatorLockdown.Instance.Config.LockdownTimeMax);
                yield return Timing.WaitForSeconds(random_delay);

                ElevatorLockdown.Instance.disabledElevators.Clear();

                if (ElevatorLockdown.Instance.Config.GlobalBroadcastTime > 0 && ElevatorLockdown.Instance.Config.GlobalBroadcastMessageReactivated != null)
                    Map.Broadcast(ElevatorLockdown.Instance.Config.GlobalBroadcastTime, ElevatorLockdown.Instance.Config.GlobalBroadcastMessageReactivated.Replace("%ELEVATOR%", gateNames), Broadcast.BroadcastFlags.Normal, true);
                if(ElevatorLockdown.Instance.Config.CassieMessageReactivated != null)
                    Cassie.Message(ElevatorLockdown.Instance.Config.CassieMessageReactivated.Replace("%ELEVATOR%", gateNames));
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
