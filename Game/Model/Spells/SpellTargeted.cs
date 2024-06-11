using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellTargeted
{
    public GameMap GameMap { get; set; }
    public bool Active { get; set; }
    public bool InvokeCommand { get; set; }
    
    public SpellTargeted(GameMap GameMap)
    {
        this.GameMap = GameMap;
    }

    public int Execute(Point location)
    {
        int damage = 0;
        Debug.WriteLine("Clicked on cell. Executing spell");
        if (GameMap.GameObjects[Convert.ToInt16(location.X)/100, Convert.ToInt16(location.Y)/100] != 1)
        {
            Debug.WriteLine("Not on target. No damage");
        }
        else
        {
            Debug.WriteLine("Player found. Damaging player");
            damage = Random.Shared.Next(1, 11);
        }
        Debug.WriteLine("Stopped casting spell");
        return damage;
    }
}
