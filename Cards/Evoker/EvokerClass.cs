using ClassesManagerReborn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Wizardry.Cards.Evoker
{
    internal class EvokerClass : ClassHandler
    {
        internal static string name = "Evoker";

        public override IEnumerator Init()
        {
            while(!(Evoker.card && Auto_Shuffler.card && Rebirth_Magics.card && Faster_Casting.card && Chaos_Magics.card && Spell_Removal.card)) yield return null;
            ClassesRegistry.Register(Evoker.card, CardType.Entry, 1);
            ClassesRegistry.Register(Auto_Shuffler.card, CardType.Card, Evoker.card, 1);
            ClassesRegistry.Register(Rebirth_Magics.card, CardType.Card, Evoker.card, 1);
            ClassesRegistry.Register(Faster_Casting.card, CardType.Card, Evoker.card, 3);
            ClassesRegistry.Register(Chaos_Magics.card, CardType.Card, Evoker.card);
            ClassesRegistry.Register(Defensive_Magics.card, CardType.Card, Evoker.card);
            ClassesRegistry.Register(Spell_Removal.card, CardType.Card, Evoker.card);
            yield break;
        }

        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}
