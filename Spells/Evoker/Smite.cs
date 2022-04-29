using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Wizardry.Spells.Evoker
{
    internal class Smite : Spell
    {
        public override void CastAction(Player player)
        {
            if (player.data.lastSourceOfDamage)
            {
                player.data.lastSourceOfDamage.data.healthHandler.DoDamage(Vector2.down.normalized * 100, player.transform.position, Color.red, null, player, false, true, true);
            }
        }

        public override string GetDescription()
        {
            return "Smite your last socrce of damage";
        }
    }
}
