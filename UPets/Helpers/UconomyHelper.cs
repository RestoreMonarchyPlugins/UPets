using fr34kyn01535.Uconomy;

namespace RestoreMonarchy.UPets.Helpers
{
    public class UconomyHelper
    {
        public static void IncreaseBalance(string playerId, decimal amount)
        {
            Uconomy.Instance.Database.IncreaseBalance(playerId, amount);
        }

        public static decimal GetPlayerBalance(string playerId)
        {
            return Uconomy.Instance.Database.GetBalance(playerId);
        }
    }
}
