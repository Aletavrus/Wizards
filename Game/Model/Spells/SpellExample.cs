using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

internal class SpellExample : SpellBase
{
    private MainViewModel viewModel;
    public SpellExample(Point location, MainViewModel viewModel) : base(location)
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