using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellTargeted : SpellBase
{
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
        Log("Clicked on cell. Executing spell");
        
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

    
}
