using System;
using System.Collections.Generic;
using System.Text;

namespace Wizardry.Spells
{
    internal abstract class Spell
    {
        public abstract void CastAction(Player player);
        public abstract string GetDescription();

        public string GetName()
        {
            return GetType().Name.Replace('_',' ');
        }
    }
}
