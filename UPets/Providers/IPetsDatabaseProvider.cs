using RestoreMonarchy.UPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoreMonarchy.UPets.Providers
{
    public interface IPetsDatabaseProvider
    {
        void AddPlayerPet(PlayerPet playerPet);
        IEnumerable<PlayerPet> GetPlayerPets(string playerId);
        void Reload();
    }
}
