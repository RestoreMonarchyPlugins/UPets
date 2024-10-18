using RestoreMonarchy.UPets.Models;
using Rocket.API;
using System.Collections.Generic;

namespace RestoreMonarchy.UPets
{
    public class PetsConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public string MessageIconUrl { get; set; } = "https://i.imgur.com/AdIgUbl.png";
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public bool UseMySQL { get; set; }        
        public string DatabaseAddress { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseTableName { get; set; }
        public int DatabasePort { get; set; }
        public bool EnableOwnerKill { get; set; } = true;

        public List<PetConfig> Pets { get; set; }

        public void LoadDefaults()
        {
            MessageColor = "#FF00FF";
            MessageIconUrl = "https://i.imgur.com/AdIgUbl.png";
            MinDistance = 5;
            MaxDistance = 50;
            UseMySQL = false;
            DatabaseAddress = "127.0.0.1";
            DatabaseUsername = "unturned";
            DatabasePassword = "password";
            DatabaseName = "unturned";
            DatabaseTableName = "PlayersPets";
            DatabasePort = 3306;
            EnableOwnerKill = true;

            Pets = new List<PetConfig>() 
            {
                new PetConfig(6, "cow", 100, ""),
                new PetConfig(5, "bear", 250, ""),
                new PetConfig(3, "wolf", 150, "pet.wolf"),
                new PetConfig(7, "reindeer", 500, "pet.reindeer"),
                new PetConfig(4, "pig", 150, "pet.pig"),
                new PetConfig(1, "deer", 150, "pet.deer")
            };
        }
    }
}
