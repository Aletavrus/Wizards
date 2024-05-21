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
        viewModel.isCastingSpell = !viewModel.isCastingSpell;
        viewModel.typeOfSpell = 2;
    }
    
    public void Execute(Point location)
    {
        Log("Clicked on cell. Executing spell");
        if (Utilities.CountMovesFromCellToCell(location, viewModel.Player.Location) / MainViewModel.CellSize > aoeRange)
        {
            Log("Player too far away");
        }
        else
        {
            Log("Player got in AOE. Damaging player");
            Random rand = new Random();
            viewModel.Player.Damage(rand.Next(0, 10));
        }
        
        viewModel.isCastingSpell = !viewModel.isCastingSpell;
        viewModel.typeOfSpell = 0;
        Log("Stopped casting spell");
    }
}