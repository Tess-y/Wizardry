using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
using UnityEngine;
using UnboundLib.Cards;
using BepInEx.Configuration;
using Wizardry.Cards.Evoker;
using Wizardry.Spells.Evoker;
using System.Collections;
using Wizardry.Extensions;
using UnboundLib.GameModes;
using ItemShops.Utils;

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
        public const string Version = "0.1.2";
        public Shop Evoker_Removal_Shop;
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

            CustomCard.BuildCard<Evoker>(c => Evoker.card = c);
            CustomCard.BuildCard<Auto_Shuffler>(c => Auto_Shuffler.card = c);
            CustomCard.BuildCard<Rebirth_Magics>(c => Rebirth_Magics.card = c);
            CustomCard.BuildCard<Faster_Casting>(c => Faster_Casting.card = c);
            CustomCard.BuildCard<Chaoss_Magics>(c => Chaoss_Magics.card = c);
            CustomCard.BuildCard<Spell_Removal>(c => Spell_Removal.card = c);
            CustomCard.BuildCard<Defencive_Magics>(c => Defencive_Magics.card = c);

            EvokerSpells.spells.Add(new Bottled_Phoenix().GetName(), new Bottled_Phoenix());
            EvokerSpells.spells.Add(new Healling_Shard().GetName(), new Healling_Shard());
            EvokerSpells.spells.Add(new Poke().GetName(), new Poke());
            EvokerSpells.spells.Add(new Smite().GetName(), new Smite());
            EvokerSpells.spells.Add(new Mass_Panic().GetName(), new Mass_Panic());
            EvokerSpells.spells.Add(new Fling().GetName(), new Fling());
            EvokerSpells.spells.Add(new Bullet_Shield().GetName(), new Bullet_Shield());

            GameModeManager.AddHook(GameModeHooks.HookGameStart, (gm) => ResetStats());
            GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, (gm) => Spell_Removal.RemovalScreen());

        }


        public static IEnumerator ResetStats()
        {
            foreach(Player player in PlayerManager.instance.players)
            {
                player.data.stats.GetAdditionalData().Evoker_Spells_To_Add.Clear();
                player.data.stats.GetAdditionalData().Evoker_Spells_To_Remove.Clear();
                player.data.stats.GetAdditionalData().Evoker_Removal = false;
            }
            yield break;
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
