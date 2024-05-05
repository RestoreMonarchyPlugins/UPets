using fr34kyn01535.Uconomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
