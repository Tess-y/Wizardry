using System;
using System.Collections.Generic;
using System.Text;

namespace Wizardry.Spells
{
    public abstract class Spell
    {
        public abstract void CastAction(Player player);
        public abstract string GetDescription();

        public string GetName()
        {
            return GetType().Name.Replace('_',' ');
        }

        public override bool Equals(object obj)
        {
            return obj is Spell && ((Spell)obj).GetName() == this.GetName(); 
        }
    }
}
