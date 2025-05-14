using HarmonyLib;
using Org.BouncyCastle.Asn1.Cms;
using RestoreMonarchy.UPets.Helpers;
using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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

        [HarmonyPatch("getAnimalsInRadius")]
        [HarmonyPostfix]
        public static void PostfixGetAnimalsInRadius(List<Animal> result)
        {
            if (result != ReflectionHelper.SentryAnimalsInRadius)
            {
                return;
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (PetsPlugin.Instance.PetsService.IsPet(result[i]))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
