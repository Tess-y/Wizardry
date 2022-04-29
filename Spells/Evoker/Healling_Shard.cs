using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;

namespace Wizardry.Spells.Evoker
{
    internal class Healling_Shard : Spell
    {
        public override void CastAction(Player player)
        {
            for (int i = 1; i <= 20; ++i)
                Wizardry.instance.ExecuteAfterSeconds(0.1f*i, () => {
                    player.data.healthHandler.Heal(1);
                });
        }

        public override string GetDescription()
        {
            return "Heal 20 hp over 2 seconds";
        }
    }
}
