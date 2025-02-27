using HarmonyLib;
using SDG.Unturned;

namespace RestoreMonarchy.UPets.Patches
{
    [HarmonyPatch(typeof(AnimalManager))]
    class AnimalManagerPatches
    {
        [HarmonyPatch("sendAnimalAttack", typeof(Animal), typeof(byte))]
        [HarmonyPrefix]
        public static bool PrefixAttack(Animal animal)
        {
            return !PetsPlugin.Instance.PetsService.IsPet(animal);
        }

        [HarmonyPatch("sendAnimalPanic")]
        [HarmonyPrefix]
        public static bool PrefixPanic(Animal animal)
        {
            return !PetsPlugin.Instance.PetsService.IsPet(animal);
        }

        [HarmonyPatch("sendAnimalStartle", typeof(Animal), typeof(byte))]
        [HarmonyPrefix]
        public static bool PrefixStartle(Animal animal)
        {
            return !PetsPlugin.Instance.PetsService.IsPet(animal);
        }
    }
}
