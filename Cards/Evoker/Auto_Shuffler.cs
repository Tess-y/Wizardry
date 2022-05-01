using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Wizardry.Extensions;

namespace Wizardry.Cards.Evoker
{
    internal class Auto_Shuffler : Template
    {
        public static CardInfo card;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            base.SetupCard(cardInfo, gun, cardStats, statModifiers);
            className.className = EvokerClass.name;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().Evoker_AutoShuffle = true;
            characterStats.GetAdditionalData().Evoker_ShuffleCoolDown *= 0.1f;
            base.OnAddCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Automaticly shuffles your deck when you run out of cards. Disables manual shuffling";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    amount = "-90%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Shuffle time"
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
    }
}
