using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Wizardry.Spells.Evoker
{
    internal class Poke : Spell
    {
        public override void CastAction(Player player)
        {
            Color purple = CardChoice.instance.cardThemes.Where(t => t.themeType == CardThemeColor.CardThemeColorType.EvilPurple).First().targetColor;
            player.data.healthHandler.TakeDamage(UnityEngine.Vector2.up.normalized, player.transform.position, purple, null, player, true, true);
        }

        public override string GetDescription()
        {
            return "Poke yourself.";
        }
    }
}
