using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnboundLib;
using UnityEngine;
using Wizardry.Utils;

namespace Wizardry.Spells.Evoker
{
    internal class Bullet_Shield : Spell
    {
        public override void CastAction(Player player)
        {
            Gun gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            Gun newGun = player.gameObject.AddComponent<Bullet_Shield_Gun>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();
            effect.SetPositions(GetPositions(player));
            effect.SetDirections(GetDirections());
            effect.SetNumBullets(1);
            effect.SetTimeBetweenShots(0f);
            effect.SetInitialDelay(0f);
            foreach(Vector3 v in GetDirections())
            {
                Wizardry.Debug($"Direction: ({v.x},{v.y},{v.z})");
            }
            foreach (Vector3 v in GetPositions(player))
            {
                Wizardry.Debug($"Position: ({v.x},{v.y},{v.z})");
            }
            SpawnBulletsEffect.CopyGunStats(gun, newGun);
            newGun.spread = 0f;
            newGun.numberOfProjectiles = 36;
            newGun.projectiles = new ProjectilesToSpawn[] { gun.projectiles[0] };
            newGun.gravity = 0;
            newGun.projectielSimulatonSpeed = 0.1f;
            newGun.projectileColor = new Color(30 / 255f, 200 / 255f, 255 / 255f);
            effect.SetGun(newGun);
        }

        public override string GetDescription()
        {
            return "Creates a ring of bullets around you";
        }
        private static List<Vector3> GetPositions(Player player)
        {
            List<Vector3> res = new List<Vector3>() { };
            for (int i = 0; i < 36; ++i)
            {
                res.Add(player.transform.position + (Quaternion.Euler(0, 0, i*10) * Vector3.up));
            }
            return res;
        }
        private static List<Vector3> GetDirections()
        {
            List<Vector3> res = new List<Vector3>() { };
            for(int i = 0; i < 36; ++i)
            {
                res.Add((Quaternion.Euler(0, 0, i*10) * Vector3.up).normalized);
            }
            return res;
        }
    }
    public class Bullet_Shield_Gun : Gun
    {

    }
}
