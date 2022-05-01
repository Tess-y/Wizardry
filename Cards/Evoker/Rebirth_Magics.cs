using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;
using UnityEngine;
using Wizardry.MonoBehavors;
using Wizardry.Spells.Evoker;

namespace Wizardry.Cards.Evoker
{
    internal class Rebirth_Magics : Template
    {
        public static CardInfo card;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            base.SetupCard(cardInfo, gun, cardStats, statModifiers);
            className.className = EvokerClass.name;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.respawns += 1;
            base.OnAddCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Revive"
                },
                new CardInfoStat()
                {
                    positive = true,
                    amount = "+2 Spells:",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bottled Phoenix"
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }
    }
}
