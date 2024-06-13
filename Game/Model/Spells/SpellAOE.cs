using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellAOE(GameMap GameMap)
{
    public GameMap GameMap { get; set; } = GameMap;
    public int AoeRange = 3;

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
        return damage;
    }
}