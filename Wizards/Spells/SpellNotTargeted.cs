namespace Wizards.Spells;

abstract class SpellNotTargeted : SpellBase
{
    protected PlayerBase caster;
    protected int[] position;

    public SpellNotTargeted(PlayerBase caster)
    {
        this.caster = caster;
    }
    
    public abstract void Cast(int castX, int castY);
}