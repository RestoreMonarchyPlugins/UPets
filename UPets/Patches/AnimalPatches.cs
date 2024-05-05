using RestoreMonarchy.UPets.Helpers;
using RestoreMonarchy.UPets.Models;
using HarmonyLib;
using SDG.Unturned;
using UnityEngine;
using UPets.Reflection;

namespace RestoreMonarchy.UPets.Patches
{
    [HarmonyPatch(typeof(Animal))]
    class AnimalPatches
    {
        [HarmonyPatch("tick")]
        [HarmonyPrefix]
        static bool tick_Prefix(Animal __instance)
        {
            var pet = PetsPlugin.Instance.PetsService.GetPet(__instance);
            if (pet == null)
            {
                return true;
            }                

            Vector3 spawnPos = AnimalsHelper.GetPosition(pet.Player);

            float delta = (float)(Time.timeAsDouble - (double)ReflectionUtil.getValue("lastTick", __instance));
            ReflectionUtil.setValue("lastTick", Time.timeAsDouble, __instance);

            ReflectionUtil.setValue("target", spawnPos, __instance);
            ReflectionUtil.setValue("_isFleeing", true, __instance);
            ReflectionUtil.setValue("currentTargetPlayer", null, __instance);
            ReflectionUtil.setValue("isAttacking", false, __instance);
            ReflectionUtil.setValue("_isFleeing", true, pet.Animal);
            ReflectionUtil.setValue("isWandering", false, pet.Animal);
            ReflectionUtil.setValue("isHunting", false, pet.Animal);
            ReflectionUtil.callMethod("move", __instance, delta);
            return false;
        }
    }
}
