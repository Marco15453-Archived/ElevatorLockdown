# ElevatorLockdown
A complex plugin that allows the Elevators to failure after a specific delay and chance.

# Commands
Name | Permission | Description | CommandType
---- | ---------- | ----------- | -----------
elock | el.lock | Locks an Elevator | RemoteAdmin
eunlock | el.unlock | Unlocks an Elevator | RemoteAdmin

# Config
Name | Type | Description | Default
---- | ---- | ----------- | -------


# Default Config
```yml
elevator_lockdown:
  # Should the plugin be enabled?
  is_enabled: true
  # How much time that needs to pass before the elevator lockdown (Seconds)
  delay_min: 300
  delay_max: 500
  # What is the Chance of a Elevator Failure? 100 means everytime, 0 = disabled
  gate_a_failure_chance: 50
  gate_b_failure_chance: 50
  l_c_z_a_failure_chance: 50
  l_c_z_b_failure_chance: 50
  nuke_failure_chance: 50
  scp049_failure_chance: 50
  # How long the elevator is deactivated
  lockdown_time_max: 30
  lockdown_time_min: 60
  # Cassie message if an elevator gets deactivated? {ELEVATOR} will be replaced with the Elevator Names
  cassie_message: '{ELEVATOR} elevator critical power failure'
  # Cassie message if an elevator gets reactivated? {ELEVATOR} will be replaced with the Elevator Names
  cassie_message_reactivated: '{ELEVATOR} elevator is back in operational mode'
  # How long should the broadcast be displayed? (-1 disables it)
  hint_time: 3
  # What message should be displayed when player trys to call/use a deactivated Elevator?
  hint_message: <color=red>The Elevator has a malfunction!</color>
  # How long should the global broadcast be displayed? (-1 disables it)
  global_broadcast_time: 5
  # What message should be global broadcasted when a elevator gets deactivated? {ELEVATOR} will be replaced with the Elevator Names
  global_broadcast_message: <color=red>{ELEVATOR}</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>
  # What message should be global broadcasted when a elevator gets reactivated? {ELEVATOR} will be replaced with the Elevator Names
  global_broadcast_message_reactivated: <color=red>{GELEVATORATE}</color> <color=green>Elevator back in operational mode</color>
```
