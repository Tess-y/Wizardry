using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;
using UnityEngine;
using Wizardry.MonoBehavors;
using Wizardry.Spells.Evoker;

namespace Wizardry.Cards.Evoker
{
    public class Evoker : Template
    {
        public static CardInfo card;
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            EvokerDeck deck = player.gameObject.GetOrAddComponent<EvokerDeck>();
            base.OnAddCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetComponent<EvokerDeck>());
            base.OnRemoveCard(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Utilizing chaos magic, the Evoker casts from a deck of spells.\n (Press Q to shuffle and E to cast)";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[] { };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

    }
}
