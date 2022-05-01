using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;
using Wizardry.Extensions;
using Wizardry.MonoBehavors;
using Wizardry.Spells;
using Wizardry.Spells.Evoker;

namespace Wizardry.Cards.Evoker
{
    internal class Chaoss_Magics : Template
    {
        public static CardInfo card;
        public static Spell[] chaossMagicsBooster = new Spell[] { new Fling(), new Mass_Panic() };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            base.SetupCard(cardInfo, gun, cardStats, statModifiers);
            className.className = EvokerClass.name;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var pack = chaossMagicsBooster.ToArray();
            pack.Shuffle();
            characterStats.GetAdditionalData().Evoker_Spells_To_Add.Add(pack[0]);
            base.OnAddCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }


        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Get a spell from the Chaoss Magics booster pack";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[0];
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
    }
}
