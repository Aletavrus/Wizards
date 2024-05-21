using System;
using System.Diagnostics;
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

    public void Execute(Point location)
    {
        
    }
    
    public void Log(String message)
    {
        Debug.WriteLine(String.Format("[{0}] {1}", this.GetType().Name, message));
    }
}