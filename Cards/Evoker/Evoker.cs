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
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            EvokerDeck deck = player.gameObject.GetOrAddComponent<EvokerDeck>();
            deck.spellDeck.Add(new Poke());
            deck.spellDeck.Add(new Poke());
            deck.spellDeck.Add(new Poke());
            deck.spellDeck.Add(new Healling_Shard());
            deck.spellDeck.Add(new Smite());
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Utilizing chaos magic, the Evoker casts from a deck of spells.";
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
