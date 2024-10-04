using Rocket.Core;

namespace RestoreMonarchy.UPets.Helpers
{
    public class UconomyHelper
    {
        public static bool IsLoaded()
        {
            return R.Plugins.GetPlugin("Uconomy") != null;
        }

        public static void IncreaseBalance(string playerId, decimal amount)
        {
            fr34kyn01535.Uconomy.Uconomy.Instance.Database.IncreaseBalance(playerId, amount);
        }

        public static decimal GetPlayerBalance(string playerId)
        {
            return fr34kyn01535.Uconomy.Uconomy.Instance.Database.GetBalance(playerId);
        }
    }
}
