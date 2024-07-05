using System.Windows.Input;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellButtonTargeted : SpellBase
{
    public SpellButtonTargeted(Point location) : base(location)
    {
    }
}