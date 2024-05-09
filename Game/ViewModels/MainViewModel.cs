using Game.Model;

using System.Collections.ObjectModel;

using Game.Model.Player;

using Point = Avalonia.Point;
using System.Diagnostics;

namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //private void ButtonClick(int x, int y)
    //{
    //    PlayerBase player = new PlayerBase(); // for an example. when we implement the init of a player in the beginning, I will change --ALEXIS--
    //    player.Move(x,y);
    //}

    public const int CellSize = 100;

    public int Height { get; set; } = 5;
    public int Width { get; set; } = 5;
    public PlayerBase Player { get; set; }

    public MainViewModel()
    {
        GameObjects = [];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObjects.Add(new MapCell(new Point(i * CellSize, j * CellSize), this));
            }
        }
        Player = new PlayerClass1(new Point(2 * CellSize, CellSize));
        GameObjects.Add(Player);
    }

    public void CellClicked(Point location)
    {
        Player.Move(location);
    }

    public ObservableCollection<GameObject> GameObjects { get; set; }
}