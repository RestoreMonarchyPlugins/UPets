using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static Vector3 GetPosition(Player player)
        {
            Vector3 pos =  player.transform.position + ((player.transform.right + player.transform.forward) * PetsPlugin.Instance.Configuration.Instance.MinDistance);

            pos.y = LevelGround.getHeight(pos);

            return pos;
        }
    }
}
