using ReactiveUI;

using System.Windows.Input;

using Point = Avalonia.Point;

namespace Game.Model;

internal class MapCell : GameObject
{
    public MapCell(Point location) : base(location)
    {
        CellClickCommand = ReactiveCommand.Create(CellClick);
    }

    public ICommand CellClickCommand { get; }

    private void CellClick()
    {
    }
}