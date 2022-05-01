using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnboundLib;
using UnityEngine;

namespace Wizardry.Spells.Evoker
{
    internal class Fling : Spell
    {
        public override void CastAction(Player player)
        {
            if(player.data.view.IsMine)
            foreach (var person in PlayerManager.instance.players.Where(p => p.teamID != player.teamID && PlayerManager.instance.CanSeePlayer(player.transform.position, p).canSee).ToArray())
            {
                person.data.healthHandler.CallTakeForce(new Vector2(UnityEngine.Random.Range(-5000,5000),(Vector2.up * 10000).y), ForceMode2D.Impulse,true,true);
            }
        }

        public override string GetDescription()
        {
            return "Throws visable players into the air";
        }
    }
}
