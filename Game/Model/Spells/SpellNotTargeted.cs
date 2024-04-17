namespace Wizards.Spells;
using Wizards.Player;

abstract class SpellNotTargeted : SpellBase
{
    protected PlayerBase caster;
    protected int[] position;
    
    public abstract void Cast(int castX, int castY);
}