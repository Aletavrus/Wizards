using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Reactive;
using Wizards.Player;

namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ReactiveCommand<(int Row, int Column), Unit> ButtonClickCommand { get; }

    public MainViewModel()
    {
        ButtonClickCommand = ReactiveCommand.Create<(int Row, int Column)>(HandleButtonClick);
    }

    private void HandleButtonClick((int Row, int Column) coordinates)
    {
        int row = coordinates.Row;
        int column = coordinates.Column;

    }
}
