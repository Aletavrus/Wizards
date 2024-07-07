using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Avalonia;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellButton : GameObject
{
    protected Point _location;
    public bool InvokeCommand { get; set; }
    public bool Active { get; set; }

    public SpellButton(Point location) : base(location)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        Location = location;
        InvokeCommand = true;
        Active = false;
    }

    public ICommand ClickCommand { get; }

    protected void Clicked()
    {
        if (InvokeCommand)
        {
            Active = !Active;
            Log("Spell icon clicked. Waiting for a cell click");
            return;
        }
        Log("Doing other action");
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