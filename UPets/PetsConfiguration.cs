using Adam.PetsPlugin.Models;
using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Adam.PetsPlugin
{
    public class PetsConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public bool UseMySQL { get; set; }        
        public string DatabaseAddress { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseTableName { get; set; }
        public int DatabasePort { get; set; }        
        
        public List<PetConfig> Pets { get; set; }

        public void LoadDefaults()
        {
            MessageColor = "#FF00FF";
            MinDistance = 5;
            MaxDistance = 50;
            UseMySQL = false;
            DatabaseAddress = "localhost";
            DatabaseUsername = "unturned";
            DatabasePassword = "password";
            DatabaseName = "unturned";
            DatabaseTableName = "PlayersPets";
            DatabasePort = 3306;
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
