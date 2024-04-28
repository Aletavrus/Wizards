using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Reactive;
using Wizards.Player;

namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private void ButtonClick(int x, int y)
    {
        PlayerBase player = new PlayerBase(); // for an example. when we implement the init of a player in the beginning, I will change --ALEXIS--
        player.Move(x,y);
    }
}
