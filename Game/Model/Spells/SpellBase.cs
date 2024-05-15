using System;
using Avalonia;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellBase : GameObject
{
    private Point _location;

    public SpellBase(Point location) : base(location)
    {
        Location = location;
    }

    public Point Location
    {
        get { return _location; }
        set
        {
            this.RaiseAndSetIfChanged(ref _location, value);
        }
    }
}