namespace Wizards.Spells;

abstract class SpellNotTargeted : SpellBase
{
    protected PlayerBase caster;
    protected int[] positionXY;
    
    public abstract void Cast(int castX, int castY);
}