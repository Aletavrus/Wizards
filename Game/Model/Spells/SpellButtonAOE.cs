using System.Windows.Input;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellButtonAOE : SpellBase
{
    public bool Active { get; set; }
    
    public SpellButtonAOE(Point location, GameMap GameMap, MainViewModel mainViewModel) : base(location, GameMap)
    {
        ClickCommand = ReactiveCommand.Create(Clicked);
        this.GameMap = GameMap;
        Active = false;
    }
    
    public ICommand ClickCommand { get; }

    private void Clicked()
    {
        if (InvokeCommand)
        {
            Active = !Active;
            Log("Spell icon clicked. Waiting for a cell click");
            return;
        }
        Log("Doing other action");
    }
}