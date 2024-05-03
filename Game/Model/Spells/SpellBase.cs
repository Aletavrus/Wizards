namespace Game.Model.Spells;

public abstract class SpellBase
{
    public int Cost;
    public abstract void Cast();
}