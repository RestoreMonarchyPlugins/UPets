using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Adam.PetsPlugin.Helpers
{
    public class AnimalsHelper
    {
        public static void KillAnimal(Animal animal)
        {
            AnimalManager.sendAnimalDead(animal, Vector3.zero);
        }
    }
}
