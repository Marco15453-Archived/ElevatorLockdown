using Exiled.API.Enums;

namespace ElevatorLockdown
{
    public static class Extensions
    {
        public static ElevatorType ElevatorToType(string str)
        {
            foreach (var readable in ElevatorLockdown.Instance.StringToElevator)
                if (readable.Key == str) return readable.Value;
            return ElevatorType.Unknown;
        }

        public static string CassieReadable(ElevatorType type)
        {
            foreach (var readable in ElevatorLockdown.Instance.Config.CassieReadable)
                if (readable.Key == type) return readable.Value;
            return "";
        }
    }
}
