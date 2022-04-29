using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using ClassesManagerReborn.Util;
using UnboundLib;

namespace Wizardry.Cards
{
    public abstract class Template : CustomCard
    {
        ClassNameMono className;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            className = gameObject.GetOrAddComponent<ClassNameMono>();
            Wizardry.Debug($"Card {GetTitle()} has been setup");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            Wizardry.Debug($"Card {GetTitle()} has been added to player {player.playerID}");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            Wizardry.Debug($"Card {GetTitle()} has been removed from player {player.playerID}");
        }

        protected override string GetTitle()
        {
            return GetType().Name.Replace('_', ' '); ;
        }

        public override string GetModName()
        {
            return Wizardry.ModName;
        }
    }
}
