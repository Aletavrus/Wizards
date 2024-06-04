using Avalonia;
using Game.Model.Spells;

namespace Game.Model.Player;

public class PlayerClass1 : PlayerBase
{
    public PlayerClass1(Point location, SpellTargeted spellTargeted, SpellAOE spellAOE, GameMap gameMap) : base(location, spellTargeted, spellAOE, gameMap)
    {

    }
}
