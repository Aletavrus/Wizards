using System.Windows.Input;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellButtonTargeted : SpellBase
{
    public bool Active { get; set; }
    
    public SpellButtonTargeted(Point location, GameMap GameMap) : base(location, GameMap)
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