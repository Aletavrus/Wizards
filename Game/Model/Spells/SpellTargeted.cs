using System;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Timers;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellTargeted : SpellBase
{
    private static System.Timers.Timer aTimer;

    private MainViewModel viewModel;
    public SpellTargeted(Point location, MainViewModel viewModel) : base(location)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.viewModel = viewModel;
    }

    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        Log("Spell icon clicked. Waiting for a cell click");
        viewModel.isCastingSpell = !viewModel.isCastingSpell;
        viewModel.typeOfSpell = 1;
    }

    public void Execute(Point location)
    {
        viewModel.Fireball.FireOpacity = 0.7;
        Log("Clicked on cell. Executing spell");
        SetTimer(viewModel, location);
        Log("Timer Created");
        while(location!=viewModel.Fireball.Location)
        {
            continue;
        }
        if (viewModel.Player.Location != location)
        {
            Log("Not on target. No damage");
        }
        else
        {
            Log("Player found. Damaging player");
            Random rand = new Random();
            viewModel.Player.Damage(rand.Next(0, 10));
        }
        viewModel.isCastingSpell = !viewModel.isCastingSpell;
        viewModel.typeOfSpell = 0;
        Log("Stopped casting spell");
    }

    private static void SetTimer(MainViewModel viewModel, Point location)
    {
        aTimer = new System.Timers.Timer(1000);
        aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, viewModel, location);
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private static void OnTimedEvent(object? sender, ElapsedEventArgs e, MainViewModel viewModel, Point cellLocation)
    {
        Point fireLocation = viewModel.Fireball.Location;
        if (fireLocation.X!=cellLocation.X)
        {
            viewModel.Fireball.Location = new Point(fireLocation.X+100, fireLocation.Y);
        }
        else if (fireLocation.Y!=cellLocation.Y)
        {
            viewModel.Fireball.Location = new Point(fireLocation.X, fireLocation.Y + 100);
        }
        else
        {
            aTimer.AutoReset = false;
            aTimer.Enabled = false;
        }
        Debug.WriteLine($"Fireball Location: {viewModel.Fireball.Location.X}, {viewModel.Fireball.Location.Y}");
    }
}
