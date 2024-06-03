using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellAOE : SpellBase
{
    public GameMap GameMap { get; set; }
    public Point TargetLocation { get; set; }
    public bool Active { get; set; }
    public int AoeRange = 3;
    
    public SpellAOE(Point location, GameMap GameMap) : base(location, GameMap)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.GameMap = GameMap;
    }

    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        Active = true;
        Log("Spell icon clicked. Waiting for a cell click");
    }
    
    public int Execute(Point targetLocation)
    {
        TargetLocation = targetLocation;
        int damage = 0;
        Log("Clicked on cell. Executing spell");
        bool inArea = GameMap.InsideArea(Convert.ToInt16(targetLocation.X) / 100, Convert.ToInt16(targetLocation.Y) / 100, AoeRange);


        if (!inArea)
        {
            Log("Player too far away");
        }
        else
        {
            Log("Player got in AOE. Damaging player");
            damage = Random.Shared.Next(0, 10);
        }
        Log("Stopped casting spell");
        return damage;
    }
}