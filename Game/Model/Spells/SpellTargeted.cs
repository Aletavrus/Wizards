using System;
using System.Collections.Generic;
using System.Linq;
using Wizards.Player;

namespace Wizards.Spells
{
    public abstract class SpellTargeted
    {
        protected PlayerBase caster;
        protected int[] position;

        public abstract void Cast(int castX, int castY);
    }
}
