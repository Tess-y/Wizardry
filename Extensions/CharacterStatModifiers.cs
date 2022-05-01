using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Wizardry.Spells;

namespace Wizardry.Extensions
{
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {
        public float Evoker_SpellCoolDown;
        public float Evoker_ShuffleCoolDown;
        public bool Evoker_AutoShuffle;
        public List<Spell> Evoker_Spells_To_Add;
        public List<Spell> Evoker_Spells_To_Remove;
        public bool Evoker_Removal;
        public CharacterStatModifiersAdditionalData()
        {
            Evoker_SpellCoolDown = 2.5f;
            Evoker_ShuffleCoolDown = 2.5f;
            Evoker_AutoShuffle = false;
            Evoker_Spells_To_Add = new List<Spell>();
            Evoker_Spells_To_Remove = new List<Spell>();
            Evoker_Removal = false;
        }
    }

    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data =
            new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers characterstats)
        {
            return data.GetOrCreateValue(characterstats);
        }

        public static void AddData(this CharacterStatModifiers characterstats, CharacterStatModifiersAdditionalData value)
        {
            try
            {
                data.Add(characterstats, value);
            }
            catch (Exception) { }
        }

    }

    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    class CharacterStatModifiersPatchResetStats
    {
        private static void Prefix(CharacterStatModifiers __instance)
        {
            __instance.GetAdditionalData().Evoker_SpellCoolDown = 2.5f;
            __instance.GetAdditionalData().Evoker_ShuffleCoolDown = 2.5f;
            __instance.GetAdditionalData().Evoker_AutoShuffle = false;
        }
    }
}
