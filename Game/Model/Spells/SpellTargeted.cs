using System;
using System.Windows.Input;
using Avalonia;
using DynamicData;
using Game.Model.Player;
using Game.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellTargeted : SpellBase
{
    private static Random rand = new Random();

    private MainViewModel viewModel;
    public SpellTargeted(Point location, MainViewModel viewModel) : base(location)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.viewModel = viewModel;
    }

    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        viewModel.Player.CurrentAction = 1;
        Log("Spell icon clicked. Waiting for a cell click");
    }

    public void Execute(Point location)
    {
        viewModel.GameControl.TargetLocation = location;
        viewModel.Fireball.Active = true;
        while (location!=viewModel.Fireball.Location)
        {
            continue;
        }
        Log("Clicked on cell. Executing spell");
        if (viewModel.Player.Location != location)
        {
            Log("Not on target. No damage");
        }
        else
        {
            Log("Player found. Damaging player");
            viewModel.Player.Damage(rand.Next(0, 10));
        }
        Log("Stopped casting spell");
        //viewModel.Fireball.Location = new Point(0, 0);
    }
}
