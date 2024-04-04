using Newtonsoft.Json;
using SDG.Unturned;
using System;

namespace Adam.PetsPlugin.Models
{
    public class PlayerPet
    {
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public ushort AnimalId { get; set; }
        public DateTime PurchaseDate { get; set; }

        [JsonIgnore]
        public Animal Animal { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
    }
}