using Rocket.Core.Plugins;
using Rocket.API.Collections;
using SDG.Unturned;
using System.Reflection;
using HarmonyLib;
using RestoreMonarchy.UPets.Providers;
using RestoreMonarchy.UPets.Services;
using Rocket.Unturned.Chat;
using Rocket.Core.Logging;
using System;
using Rocket.API;
using System.Threading;
using Rocket.Core.Utils;
using Rocket.Core.Commands;

namespace RestoreMonarchy.UPets
{
    public class PetsPlugin : RocketPlugin<PetsConfiguration>
    {
        public static PetsPlugin Instance { get; private set; }
        public UnityEngine.Color MessageColor { get; private set; }

        public const string HarmonyInstanceId = "com.restoremonarchy.upets";
        private Harmony HarmonyInstance { get; set; }

        public IPetsDatabaseProvider Database { get; private set; }

        public PetsService PetsService { get; private set; }
        public PetsMovementService PetsMovementService { get; private set; }

        protected override void Load()
        {
            Instance = this;
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, UnityEngine.Color.green);

            HarmonyInstance = new Harmony(HarmonyInstanceId);
            HarmonyInstance.PatchAll(Assembly);

            if (Configuration.Instance.UseMySQL)
                Database = new MySQLPetsDatabaseProvider();
            else
                Database = new JsonPetsDatabaseProvider();

            Database.Reload();

            PetsService = gameObject.AddComponent<PetsService>();
            PetsMovementService = gameObject.AddComponent<PetsMovementService>();

            Logger.Log($"Made by AdamAdam, maintained by Restore Monarchy.", ConsoleColor.Yellow);
            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Instance = null;

            HarmonyInstance.UnpatchAll(HarmonyInstanceId);

            Destroy(PetsService);
            Destroy(PetsMovementService);
            
            Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        public void ReplyPlayer(IRocketPlayer player, string translationsKey, params object[] args)
        {
            if (!ThreadUtil.IsGameThread(Thread.CurrentThread))
                TaskDispatcher.QueueOnMainThread(Send);
            else
                Send();
            
            void Send()
            {
                UnturnedChat.Say(player, Translate(translationsKey, args), MessageColor);
            }
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "PetHelpLine1", "/pet list - Displays a list of your pets" },
            { "PetHelpLine2", "/pet buy <name> - Buys a pet with specified name" },
            { "PetHelpLine3", "/pet shop - Displays a list of available pets in the shop" },
            { "PetHelpLine4", "/pet <name> - Spawns/Despawns a specified pet" },            
            { "PetShopAvailable", "Available pets:" },
            { "PetShopNoPets", "There isn't any pet available in the shop" },
            { "PetList", "Your Pets: {0}" },
            { "PetListNone", "You don't have any pets" },
            { "PetNameRequired", "You have to specify pet name" },
            { "PetNotFound", "Failed to find any pet called {0}" },
            { "PetSpawnSuccess", "Successfully spawned {0}!" },
            { "PetSpawnFail", "You don't have {0}" },
            { "PetDespawnSuccess", "Successfully despawned your {0}!" },
            { "PetCantAfford", "You can't afford to buy {0} for ${1}" },
            { "PetBuySuccess", "You successfully bought {0} for ${1}!" },
            { "PetBuyAlreadyHave", "You already have {0}!" },
            { "PetBuyNoPermission", "You don't have permission to buy {0}!" },
            { "PetKilledByOwner", "You killed your pet {0}!" }
        };

        [RocketCommand("pets", "Displays a list of your pets", "", AllowedCaller.Player)]
        public void PetsCommand(IRocketPlayer caller, string[] command)
        {
            PetCommand.ListCommand(caller);
        }

        [RocketCommand("petshop", "Displays a list of pets in shop", "", AllowedCaller.Player)]
        [RocketCommandAlias("petsshop")]
        public void PetShopCommand(IRocketPlayer caller, string[] command)
        {
            PetCommand.ShopCommand(caller);
        }

        [RocketCommand("pethelp", "Displays a help of pet commands", "", AllowedCaller.Player)]
        [RocketCommandAlias("petshelp")]
        public void PetHelpCommand(IRocketPlayer caller, string[] command)
        {
            PetCommand.HelpCommand(caller);
        }
    }
}

