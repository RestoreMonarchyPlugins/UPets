using HarmonyLib;
using SDG.Unturned;
using UnityEngine;
using UPets.Reflection;

namespace Adam.PetsPlugin.Patches
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
                return true;

            Vector3 playerPos = pet.Player.transform.position;

            Vector3 spawnPos = playerPos + ((pet.Player.transform.right + pet.Player.transform.forward) * PetsPlugin.Instance.Configuration.Instance.MinDistance);

            spawnPos.y = LevelGround.getHeight(spawnPos);

            float delta = Time.time - (float)ReflectionUtil.getValue("lastTick", __instance);
            ReflectionUtil.setValue("lastTick", Time.time, __instance);

            ReflectionUtil.setValue("target", spawnPos, __instance);
            ReflectionUtil.setValue("_isFleeing", true, __instance);
            ReflectionUtil.setValue("player", null, __instance);
            ReflectionUtil.setValue("isAttacking", false, __instance);
            ReflectionUtil.setValue("_isFleeing", true, pet.Animal);
            ReflectionUtil.setValue("isWandering", false, pet.Animal);
            ReflectionUtil.setValue("isHunting", false, pet.Animal);
            ReflectionUtil.callMethod("move", __instance, delta);
            return false;
        }

        [HarmonyPatch("askDamage")]
        [HarmonyPrefix]
        static bool askDamage_Prefix(Animal __instance)
        {
            return !PetsPlugin.Instance.PetsService.IsPet(__instance);
        }
    }
}
