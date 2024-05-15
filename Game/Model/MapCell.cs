using Avalonia.Controls;
using Game.ViewModels;
using ReactiveUI;

using System.Windows.Input;
using Avalonia.Media;
using Point = Avalonia.Point;

namespace Game.Model;

internal class MapCell : GameObject
{
    private MainViewModel viewModel;

    public MapCell(Point location, MainViewModel viewModel) : base(location)
    {
        CellClickCommand = ReactiveCommand.Create(CellClick);
        this.viewModel = viewModel;
    }

    public ICommand CellClickCommand { get; }

    private void CellClick()
    {
        viewModel.CellClicked(Location);
    }
}