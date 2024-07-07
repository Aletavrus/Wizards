using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Game.Model.Effects;

namespace Game.Model.Spells;
public class SpellUltimate
{
    public SpellUltimate(GameMap gameMap, EffectBase effect, int minDamage, int maxDamage)
    {
        GameMap = gameMap;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.effect = effect;
    }
    public GameMap GameMap { get; set; }
    public int minDamage;
    public int maxDamage;
    public EffectBase effect;
    public int AoeRange = 5;
    public List<int> playerHit;
    public int damage = 0;

    public void Execute(Point location)
    {
        damage = 0;
        Debug.WriteLine("Clicked on cell. Executing spell");
        playerHit = GameMap.InsideArea(Convert.ToInt16(location.X) / 100, Convert.ToInt16(location.Y) / 100, AoeRange);
        if (playerHit == null)
        {
            Debug.WriteLine("Players are too far away");
        }
        else
        {
            Debug.WriteLine("Player got in AOE. Damaging player");
            damage = Random.Shared.Next(minDamage, maxDamage + 1);
        }
        Debug.WriteLine("Stopped casting spell");
    }
}
