using Adam.PetsPlugin.Models;
using Adam.PetsPlugin.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Adam.PetsPlugin.Providers
{
    public class JsonPetsDatabaseProvider : IPetsDatabaseProvider
    {
        private PetsPlugin pluginInstance => PetsPlugin.Instance;

        private DataStorage<List<PlayerPet>> DataStorage { get; set; }

        public JsonPetsDatabaseProvider()
        {
            DataStorage = new DataStorage<List<PlayerPet>>(pluginInstance.Directory, "PlayersPets.json");
        }

        private List<PlayerPet> playersPets;
        private int GetIDForPet() => playersPets.OrderBy(x => x.Id).LastOrDefault()?.Id + 1 ?? 1;
        
        public void Reload()
        {
            playersPets = DataStorage.Read();
            if (playersPets == null)
                playersPets = new List<PlayerPet>();
        }

        public void AddPlayerPet(PlayerPet playerPet)
        {
            playerPet.Id = GetIDForPet();
            playersPets.Add(playerPet);
            DataStorage.Save(playersPets);
        }

        public IEnumerable<PlayerPet> GetPlayerPets(string playerId)
        {
            return playersPets.Where(x => x.PlayerId == playerId); 
        }
    }
}
