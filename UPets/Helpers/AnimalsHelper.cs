using SDG.Unturned;
using UnityEngine;

namespace RestoreMonarchy.UPets.Helpers
{
    public class AnimalsHelper
    {
        public static void KillAnimal(Animal animal)
        {
            AnimalManager.sendAnimalDead(animal, Vector3.zero);
        }

        public static Vector3 GetPosition(Player player)
        {
            Vector3 pos = player.transform.position + ((player.transform.right + player.transform.forward) * PetsPlugin.Instance.Configuration.Instance.MinDistance);

            pos.y = LevelGround.getHeight(pos);

            return pos;
        }
    }
}
