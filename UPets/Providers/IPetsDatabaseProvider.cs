using Adam.PetsPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam.PetsPlugin.Providers
{
    public interface IPetsDatabaseProvider
    {
        void AddPlayerPet(PlayerPet playerPet);
        IEnumerable<PlayerPet> GetPlayerPets(string playerId);
        void Reload();
    }
}
