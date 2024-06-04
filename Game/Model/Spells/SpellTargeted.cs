using System;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellTargeted : SpellBase
{
    public GameMap GameMap { get; set; }
    public Point TargetLocation { get; set; }
    public bool Active { get; set; }

    public SpellTargeted(Point location, GameMap GameMap) : base(location, GameMap)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.GameMap = GameMap;
        Active = false;
    }

    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        Active = true;
        Log("Spell icon clicked. Waiting for a cell click");
    }

    public int Execute(Point location)
    {
        TargetLocation= location;
        int damage = 0;
        Log("Clicked on cell. Executing spell");
        if (GameMap.GameObjects[Convert.ToInt16(location.X)/100, Convert.ToInt16(location.Y)/100] != 1)
        {
            Log("Not on target. No damage");
        }
        else
        {
            Log("Player found. Damaging player");
            damage = Random.Shared.Next(0, 11);
        }
        Log("Stopped casting spell");
        Active = false;
        return damage;
    }
}
