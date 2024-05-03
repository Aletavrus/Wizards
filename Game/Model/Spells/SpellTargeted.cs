using Game.Model.Player;

namespace Game.Model.Spells;

public abstract class SpellTargeted
{
    protected PlayerBase caster;
    protected int[] position;

    public abstract void Cast(int castX, int castY);
}
