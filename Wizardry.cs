using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
using UnityEngine;
using UnboundLib.Cards;
using BepInEx.Configuration;
using Wizardry.Cards.Evoker;

namespace Wizardry
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.itemshops", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class Wizardry : BaseUnityPlugin
    {
        internal static ConfigEntry<bool> DEBUG;
        internal const string ModId = "root.rounds.wizardy";
        internal const string ModName = "Wizardry";
        public const string Version = "1.0.0";
        public static Wizardry instance { get; private set; }

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();

            DEBUG = base.Config.Bind<bool>(ModName, "Debug", false, "Enable to turn on concole spam from our mod");
        }

        void Start()
        {
            instance = this;

            CustomCard.BuildCard<Evoker>();
        }



        public static void Debug(object message)
        {
            if (DEBUG.Value)
            {
                UnityEngine.Debug.Log($"{ModName}=> {message}");
            }
        }
    }
}
