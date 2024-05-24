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
    private static Random rand = new Random();

    private MainViewModel viewModel;

    private int aoeRange = 3;
    
    public SpellAOE(Point location, MainViewModel viewModel) : base(location)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.viewModel = viewModel;
    }

    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        Log("Spell icon clicked. Waiting for a cell click");
    }
    
    public void Execute(Point location)
    {
        viewModel.GameControl.TargetLocation = location;
        Log("Clicked on cell. Executing spell");
        if (Utilities.CountMovesFromCellToCell(location, viewModel.Player.Location) / MainViewModel.CellSize > aoeRange)
        {
            Log("Player too far away");
        }
        else
        {
            Log("Player got in AOE. Damaging player");
            viewModel.Player.Damage(rand.Next(0, 10));
        }
        Log("Stopped casting spell");
    }
}