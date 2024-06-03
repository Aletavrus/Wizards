using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Avalonia;
using ReactiveUI;

namespace Game.Model.Spells;

[JsonDerivedType(typeof(SpellAOE), "SpellAOE")]
[JsonDerivedType(typeof(SpellTargeted), "SpellTargeted")]
public class SpellBase : GameObject
{
    private Point _location;
    public GameMap GameMap { get; set; }

    public SpellBase(Point location, GameMap GameMap) : base(location)
    {
        Location = location;
        this.GameMap = GameMap;
    }
    public Point Location
    {
        get { return _location; }
        set
        {
            this.RaiseAndSetIfChanged(ref _location, value);
        }
    }

    public void Execute(Point location)
    {
        
    }
}