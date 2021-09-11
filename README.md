# ElevatorLockdown
A complex plugin that allows the Elevators to failure after a specific delay and chance.

# Commands
Name | Permission | Description | CommandType
---- | ---------- | ----------- | -----------
elock | el.lock | Locks an Elevator | RemoteAdmin
eunlock | el.unlock | Unlocks an Elevator | RemoteAdmin
elist | el.list | List of all available Elevators | RemoteAdmin

# Config
Name | Type | Description | Default
---- | ---- | ----------- | -------
is_enabled | bool | Should the plugin be enabled? | true
auto_update | bool | Should the plugin automaticly update? | true
delay_min | int | How much time that needs to pass before the elevator lockdown min(Seconds) | 300
delay_max | int |How much time that needs to pass before the elevator lockdown max(Seconds) | 500
failure_chances | Dictionary | What is the Chance of a Elevator Failure? 100 means everytime, 0 = disabled | All on 50
lockdown_time_min | int | How long the elevator is deactivated? min | 30
lockdown_time_max | int | How long the elevator is deactivated? max | 60
cassie_readable | Dictionary | Cassie Replacements for ElevatorType | Gate A, Gate B, Light Containment Zone A, Light Containment Zone B, Nuke, SCP 0 4 9
cassie_message | string | Cassie message if an elevator gets deactivated? %ELEVATOR% will be replaced with the Elevator Names | %ELEVATOR% elevator critical power failure
cassie_message_reactivated | string | Cassie message if an elevator gets reactivated? %ELEVATOR% will be replaced with the Elevator Names | %ELEVATOR% elevator is back in operational mode
hint_time | int | How long should the broadcast be displayed? (-1 disables it) | 3
hint_message | string | What message should be displayed when player trys to call/use a deactivated Elevator? | <color=red>The Elevator has a malfunction!</color>
global_broadcast_time | int | How long should the global broadcast be displayed? (-1 disables it) | 5
global_broadcast_message | string | What message should be global broadcasted when a elevator gets deactivated? %ELEVATOR% will be replaced with the Elevator Names | <color=red>%ELEVATOR%</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>
global_broadcast_message_reactivated | string | What message should be global broadcasted when a elevator gets reactivated? %ELEVATOR% will be replaced with the Elevator Names | <color=red>%ELEVATOR%</color> <color=green>Elevator back in operational mode</color>

# Default Config
```yml
elevator_lockdown:
  # Should the plugin be enabled?
  is_enabled: true
  # Should the plugin automaticly update?
  auto_update: true
  # How much time that needs to pass before the elevator lockdown (Seconds)
  delay_min: 300
  delay_max: 500
  # What is the Chance of a Elevator Failure? 100 means everytime, 0 = disabled
  failure_chances:
    GateA: 50
    GateB: 50
    LczA: 50
    LczB: 50
    Nuke: 50
    Scp049: 50
  # How long the elevator is deactivated
  lockdown_time_min: 30
  lockdown_time_max: 60
  # Cassie Replacements for ElevatorType
  cassie_readable:
    GateA: Gate A
    GateB: Gate B
    LczA: Light Containment Zone A
    LczB: Light Containment Zone B
    Nuke: Nuke
    Scp049: SCP 0 4 9
  # Cassie message if an elevator gets deactivated? %ELEVATOR% will be replaced with the Elevator Names
  cassie_message: '%ELEVATOR% elevator critical power failure'
  # Cassie message if an elevator gets reactivated? %ELEVATOR% will be replaced with the Elevator Names
  cassie_message_reactivated: '%ELEVATOR% elevator is back in operational mode'
  # How long should the broadcast be displayed? (-1 disables it)
  hint_time: 3
  # What message should be displayed when player trys to call/use a deactivated Elevator?
  hint_message: <color=red>The Elevator has a malfunction!</color>
  # How long should the global broadcast be displayed? (-1 disables it)
  global_broadcast_time: 5
  # What message should be global broadcasted when a elevator gets deactivated? %ELEVATOR% will be replaced with the Elevator Names
  global_broadcast_message: <color=red>%ELEVATOR%</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>
  # What message should be global broadcasted when a elevator gets reactivated? %ELEVATOR% will be replaced with the Elevator Names
  global_broadcast_message_reactivated: <color=red>%ELEVATOR%</color> <color=green>Elevator back in operational mode</color>
```
