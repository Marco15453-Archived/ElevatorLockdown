using Exiled.API.Enums;

namespace ElevatorLockdown
{
    public static class Extensions
    {
        public static ElevatorType ElevatorToType(string str)
        {
            if (ElevatorLockdown.StringToElevator.TryGetValue(str, out ElevatorType type))
                return type;
            return ElevatorType.Unknown;
        }

        public static string CassieReadable(ElevatorType type)
        {
            if (ElevatorLockdown.Instance.Config.CassieReadable.TryGetValue(type, out string str))
                return str;
            return "";
        }
    }
}
