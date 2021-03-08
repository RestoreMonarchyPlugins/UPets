using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using UnityEngine;
using UPets.Reflection;

namespace Adam.PetsPlugin.Services
{
    public class PetsMovementService : MonoBehaviour
    {
        private PetsPlugin pluginInstance => PetsPlugin.Instance;

        void Awake()
        {

        }

        void Start()
        {
            UnturnedPlayerEvents.OnPlayerUpdatePosition += FollowPlayer;
        }

        void OnDestroy()
        {
            UnturnedPlayerEvents.OnPlayerUpdatePosition -= FollowPlayer;
        }

        private void FollowPlayer(UnturnedPlayer player, Vector3 position)
        {
            var pets = pluginInstance.PetsService.GetPlayerActivePets(player.Id);

            foreach (var pet in pets)
            {
                ReflectionUtil.setValue("_isFleeing", true, pet.Animal);
                ReflectionUtil.setValue("isWandering", false, pet.Animal);
                ReflectionUtil.setValue("isHunting", false, pet.Animal);
                ReflectionUtil.callMethod("updateTicking", pet.Animal);

                if (Vector3.Distance(pet.Animal.transform.position, pet.Player.transform.position) > pluginInstance.Configuration.Instance.MaxDistance && 
                    !player.IsInVehicle)
                {
                    pet.Animal.transform.position = pet.Player.transform.position;
                }
            }
        }
    }
}
