using Avalonia;
using Game.Model.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Spells;
public class SpellCurse
{
    public GameMap GameMap { get; set; }
    public int playerHit = 0;
    public EffectBase effect;
    public SpellCurse(GameMap gameMap, EffectBase effect)
    {
        GameMap = gameMap;
        this.effect = effect;
    }


    public virtual void Execute(Point location)
    {
        Debug.WriteLine("Clicked on cell. Executing spell");
        if (GameMap.GameObjects[Convert.ToInt16(location.X) / 100, Convert.ToInt16(location.Y) / 100] == 0)
        {
            Debug.WriteLine("Not on target. No curse");
            playerHit = 0;
        }
        else
        {
            Debug.WriteLine("Player found. Cursing player");
            playerHit = GameMap.GameObjects[Convert.ToInt16(location.X) / 100, Convert.ToInt16(location.Y) / 100];
        }
        Debug.WriteLine("Stopped casting spell");
    }
}
