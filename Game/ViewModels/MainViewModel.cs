using Avalonia.Controls;
using Wizards.Player;

namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public void MakeMove(int[] pos)
    {
        PlayerClass1 player = new PlayerClass1(); //this would happen at the initialization of our program
        player.Move(pos[0], pos[1]);
    }
}
