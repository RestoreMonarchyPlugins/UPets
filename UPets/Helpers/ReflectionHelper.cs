using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;

namespace RestoreMonarchy.UPets.Helpers
{
    internal static class ReflectionHelper
    {
        internal static List<Animal> SentryAnimalsInRadius = typeof(InteractableSentry).GetField("animalsInRadius", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as List<Animal>;
    }
}
