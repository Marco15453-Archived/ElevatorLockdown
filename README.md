# ElevatorLockdown
A complex plugin that allows the Gate Elevators to failure after a specific chance.

# Config
Name | Type | Description | Default
---- | ---- | ----------- | -------
is_enabled | bool | Should the plugin be enabled? | true
first_delay_min | int | How much time that needs to pass before the first elevator lockdown min(Seconds) | 300
first_delay_max | int | How much time that needs to pass before the first elevator lockdown max(Seconds) | 500
gatea_failure_chance | int | What is the Chance of a Gate Elevator Failure? (Gate A) | 50
gateb_failure_chance | int | What is the Chance of a Gate Elevator Failure? (Gate B) | 50
delay_min | int | How much time that needs to pass before an elevator lockdown after the first elevator min(Seconds) | 90
delay_max | int | How much time that needs to pass before an elevator lockdown after the first elevator max(Seconds) | 210
lockdown_time_min | int | How long the elevator is deactivated min | 30
lockdown_time_max | int | How long the elevator is deactivated max | 60
cassie_message | string | Cassie message if an elevator gets deactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B) | {GATE} elevator critical power failure
cassie_message_reactivate | string | Cassie message if an elevator gets reactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B) | {GATE} elevator is back in operational mode
hint | bool | Should you get an hint if a player trys to call/use a deactivated Elevator? | true
hint_time | ushort | How long should the broadcast be displayed? | 3
hint_message | string | What message should be displayed when player trys to call/use a deactivated Elevator? {GATE} will be replaced with the Gate Name | <color=red>The Elevator has a malfunction!</color>
global_broadcast | bool | Should there be a global broadcast for all players when a Elevator gets deactivated and reactivated? | true
global_broadcast_time | ushort | How long should the global broadcast be displayed? | 5
global_broadcast_message | string | What message should be global broadcasted when a elevator gets deactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B) | <color=red>{GATE}</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>
global_broadcast_message_reactivated | string | What message should be global broadcasted when a elevator gets reactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B) | <color=red>{GATE}</color> <color=green>Elevator back in operational mode</color>

# Default Config
```
elevator_lockdown:
# Should the plugin be enabled?
  is_enabled: true
  # How much time that needs to pass before the first elevator lockdown (Seconds)
  first_delay_min: 300
  first_delay_max: 500
  # What is the Chance of a Gate Elevator Failure? 100 means everytime, 1 means very rarly
  gatea_failure_chance: 50
  gateb_failure_chance: 50
  # How much time that needs to pass before an elevator lockdown after the first elevator (Seconds)
  delay_min: 90
  delay_max: 210
  # How long the elevator is deactivated
  lockdown_time_min: 30
  lockdown_time_max: 60
  # Cassie message if an elevator gets deactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)
  cassie_message: '{GATE} elevator critical power failure'
  # Cassie message if an elevator gets reactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)
  cassie_message_reactivate: '{GATE} elevator is back in operational mode'
  # Should you get an hint if a player trys to call/use a deactivated Elevator?
  hint: true
  # How long should the broadcast be displayed?
  hint_time: 3
  # What message should be displayed when player trys to call/use a deactivated Elevator? {GATE} will be replaced with the Gate Name
  hint_message: <color=red>The Elevator has a malfunction!</color>
  # Should there be a global broadcast for all players when a Elevator gets deactivated and reactivated?
  global_broadcast: true
  # How long should the global broadcast be displayed?
  global_broadcast_time: 5
  # What message should be global broadcasted when a elevator gets deactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)
  global_broadcast_message: <color=red>{GATE}</color> <color=blue>Elevator Critical Power Failure! Rebooting!</color>
  # What message should be global broadcasted when a elevator gets reactivated? {GATE} will be replaced with the Gate Name (If Both gets deactivated it {GATE} will be Gate A and Gate B)
  global_broadcast_message_reactivated: <color=red>{GATE}</color> <color=green>Elevator back in operational mode</color>
```