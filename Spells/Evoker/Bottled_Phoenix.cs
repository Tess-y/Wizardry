using Sonigon;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;
using UnboundLib.GameModes;

namespace Wizardry.Spells.Evoker
{
    internal class Bottled_Phoenix : Spell
    {
        public override void CastAction(Player player)
        {
            player.data.healthHandler.isRespawning = true;
            SoundManager.Instance.Play((SoundEvent)player.data.healthHandler.GetFieldValue("soundDie"), player.transform);
            if (!player.data.healthHandler.DestroyOnDeath)
            {
                player.data.healthHandler.gameObject.SetActive(value: false);
                GamefeelManager.GameFeel(UnityEngine.Vector2.down.normalized * 3f);
                UnityEngine.Object.Instantiate(player.data.healthHandler.deathEffectPhoenix, player.transform.position, player.transform.rotation).GetComponent<DeathEffect>().PlayDeath(PlayerSkinBank.GetPlayerSkinColors(player.playerID).color, player.data.playerVel, UnityEngine.Vector2.down, player.playerID);
                ((DamageOverTime)player.data.healthHandler.GetFieldValue("dot")).StopAllCoroutines();
                player.data.stunHandler.StopStun();
                player.data.silenceHandler.StopSilence();
                float maxHealthDelta = player.data.maxHealth / 2f;
                player.data.maxHealth -= maxHealthDelta;
                GameModeManager.AddOnceHook(GameModeHooks.HookPointEnd, (gm) => { player.data.maxHealth += maxHealthDelta; return null; });
            }
        }

        public override string GetDescription()
        {
            return "Triggers a respawn but at half health";
        }
    }
}
