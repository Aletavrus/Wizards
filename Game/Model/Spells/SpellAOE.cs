using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellAOE
{
    public GameMap GameMap { get; set; }
    public bool Active { get; set; }
    public int AoeRange = 3;
    public bool InvokeCommand { get; set; }
    
    public SpellAOE(GameMap GameMap)
    {
        this.GameMap = GameMap;
        Active = false;
    }
    public int Execute(Point location)
    {
        int damage = 0;
        Debug.WriteLine("Clicked on cell. Executing spell");
        bool inArea = GameMap.InsideArea(Convert.ToInt16(location.X) / 100, Convert.ToInt16(location.Y) / 100, AoeRange);
        if (!inArea)
        {
            Debug.WriteLine("Player too far away");
        }
        else
        {
            Debug.WriteLine("Player got in AOE. Damaging player");
            damage = Random.Shared.Next(1, 11);
        }
        Debug.WriteLine("Stopped casting spell");
        Active = false;
        return damage;
    }
}