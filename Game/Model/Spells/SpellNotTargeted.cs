using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using ReactiveUI;
using System.Diagnostics;
using System.Windows.Input;

namespace Game.Model.Spells;

abstract class SpellNotTargeted : SpellBase
{
    private MainViewModel viewModel;
    public SpellNotTargeted(Point location, MainViewModel viewModel) : base(location)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.viewModel = viewModel;
    }

    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        Debug.WriteLine("Spell clicked");
        viewModel.isCastingSpell = !viewModel.isCastingSpell;
    }
}