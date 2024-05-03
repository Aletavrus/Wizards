using Game.Model.Player;

namespace Game.Model.Spells;

abstract class SpellNotTargeted : SpellBase
{
    protected PlayerBase caster;
    protected int[] position;
    
    public abstract void Cast(int castX, int castY);
}