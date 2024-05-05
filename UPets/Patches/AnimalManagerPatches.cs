using HarmonyLib;
using SDG.Unturned;
using Steamworks;

namespace RestoreMonarchy.UPets.Patches
{
    [HarmonyPatch(typeof(AnimalManager))]
    class AnimalManagerPatches
    {
        [HarmonyPatch("askAnimalAttack")]
        [HarmonyPrefix]
        public static bool Prefix(CSteamID steamID, ushort index)
        {
            return !PetsPlugin.Instance.PetsService.IsPet(AnimalManager.animals[index]);
        }
    }
}
