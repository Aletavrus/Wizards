using System.Windows.Input;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellButtonAOE : SpellBase
{
    public SpellButtonAOE(Point location) : base(location)
    {
    }
}