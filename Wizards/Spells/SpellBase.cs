namespace Wizards.Spells;

public abstract class SpellBase : IEmptySpell
{
    public int Cost;
    public abstract void Cast();
}