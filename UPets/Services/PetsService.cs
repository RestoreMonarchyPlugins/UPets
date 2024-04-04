using Adam.PetsPlugin.Helpers;
using Adam.PetsPlugin.Models;
using Rocket.Core.Utils;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Extensions;
using UnityEngine;
using System.Collections;
using Rocket.Unturned.Chat;
using Steamworks;

namespace Adam.PetsPlugin.Services
{
    public class PetsService : MonoBehaviour
    {
        private PetsPlugin pluginInstance => PetsPlugin.Instance;
        
        public event PetSpawned OnPetSpawned;
        public event PetDespawned OnPetDespawned;
        
        public List<PlayerPet> ActivePets { get; private set; }

        void Awake()
        {
            ActivePets = new List<PlayerPet>();
        }

        void Start()
        {
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            DamageTool.damageAnimalRequested += OnDamageAnimalRequested;
        }

        void OnDestroy()
        {
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;
            DamageTool.damageAnimalRequested -= OnDamageAnimalRequested;
            
            //we want to also kill all pets on shutdown
            foreach (var pet in ActivePets.ToArray())
            {
                RemovePet(pet);
                SendDeadPet(pet);
            }
        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            foreach (var pet in GetPlayerActivePets(player.Id).ToArray())
            {
                InvokeKillPet(pet);
            }
        }

        private void OnDamageAnimalRequested(ref DamageAnimalParameters parameters, ref bool shouldAllow)
        {
            PlayerPet pet = GetPet(parameters.animal);

            if (pet != null)
            {
                if (parameters.instigator is Player instigator && instigator == pet.Player)
                {
                    InvokeKillPet(pet);
                    
                    CSteamID steamID = pet.Player.channel.owner.playerID.steamID;
                    string animalName = pet.Animal.asset.animalName;

                    UnturnedChat.Say(steamID, pluginInstance.Translate("PetKilledByOwner", animalName), pluginInstance.MessageColor);
                }

                shouldAllow = false;
            }
        }

        public void SpawnPet(UnturnedPlayer player, PlayerPet pet)
        {
            foreach (var activePet in GetPlayerActivePets(player.Id).ToArray())
            {
                InvokeKillPet(activePet);
            }

            Vector3 point = player.Player.transform.position;
            AnimalManager.spawnAnimal(pet.AnimalId, point, player.Player.transform.rotation);

            // remove animal spawn
            AnimalManager.packs.RemoveAll(x => x.spawns.Exists(y => y.point == point));            

            // I know it's crap and but that's the simplest way atm, please pr if you know better
            var animals = new List<Animal>();
            AnimalManager.getAnimalsInRadius(player.Position, 1, animals);

            pet.Animal = animals.FirstOrDefault(x => x.asset.id == pet.AnimalId);            
            pet.Player = player.Player;

            ActivePets.Add(pet);
            OnPetSpawned.TryInvoke(pet);
        }

        private readonly Vector3 undergroundPosition = new Vector3(0, 0, 0);
        private const float killSecondsDelay = 3;

        private void RemovePet(PlayerPet pet)
        {
            pet.Animal.transform.position = undergroundPosition;
            ActivePets.Remove(pet);
            OnPetDespawned.TryInvoke(pet);
        }

        private void SendDeadPet(PlayerPet pet)
        {
            AnimalsHelper.KillAnimal(pet.Animal);
        }

        public void InvokeKillPet(PlayerPet pet)
        {
            StartCoroutine(KillPet(pet));
        }

        private IEnumerator KillPet(PlayerPet pet)
        {
            RemovePet(pet);   

            yield return new WaitForSeconds(killSecondsDelay);

            SendDeadPet(pet);
        }

        public bool IsPet(Animal animal) => ActivePets.Exists(x => x.Animal == animal);
        public PlayerPet GetPet(Animal animal) => ActivePets.FirstOrDefault(x => x.Animal == animal);
        public IEnumerable<PlayerPet> GetPlayerActivePets(string playerId) => ActivePets.Where(x => x.PlayerId == playerId);
    }

    public delegate void PetSpawned(PlayerPet pet);
    public delegate void PetDespawned(PlayerPet pet);
}
