using System;
using System.Collections.Generic;
using System.Text;

namespace Wizardry.Spells.Evoker
{
    internal class Mass_Panic : Spell
    {
        public override void CastAction(Player player)
        {
            if (player.data.view.IsMine)
            {
                foreach (Player p in PlayerManager.instance.players)
                    if (p != player)
                        p.data.block.TryBlock();
            }
        }

        public override string GetDescription()
        {
            return "Make all other player block if able";
        }
    }
}
