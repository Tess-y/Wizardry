using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Wizardry.Extensions;

namespace Wizardry.Cards.Evoker
{
    internal class Faster_Casting : Template
    {
        public static CardInfo card;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            gun.damage = 0.85f;
            base.SetupCard(cardInfo, gun, cardStats, statModifiers);
            className.className = EvokerClass.name;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().Evoker_SpellCoolDown -= 0.5f;
            base.OnAddCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "By focusing on your spell casting, you have learned to cast spells faster";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    amount = "-0.5s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Spell Cooldown"
                },
                new CardInfoStat()
                {
                    positive = false,
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Gun Damage"
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
    }
}
