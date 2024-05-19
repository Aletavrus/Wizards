using System;
using Game.Model;

using System.Collections.ObjectModel;

using Game.Model.Player;

using Point = Avalonia.Point;
using System.Diagnostics;
using Avalonia.Input;
using Game.Model.Spells;

namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //private void ButtonClick(int x, int y)
    //{
    //    PlayerBase player = new PlayerBase(); // for an example. when we implement the init of a player in the beginning, I will change --ALEXIS--
    //    player.Move(x,y);
    //}

    public static Random rand = new Random();

    public const int CellSize = 100;

    public bool isCastingSpell = false;

    public int Height { get; set; } = 5;
    public int Width { get; set; } = 7;
    public PlayerBase Player { get; set; }

    public MainViewModel()
    {
        
        GameObjects = [];
        Player = new PlayerClass1(new Point(3 * CellSize, 2 * CellSize));
        GameObjects.Add(Player);
        for (int i = 1; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObjects.Add(new MapCell(new Point(i * CellSize, j * CellSize), this));
            }
        }
        
        GameObjects.Add(new SpellExample(new Point(10, CellSize + 10), this));
        GameObjects.Add(new SpellExample(new Point((Width - 1) * CellSize + 10, CellSize + 10), this));
        GameObjects.Add(new SpellExample(new Point(10, (Height - 2) * CellSize + 10), this));
        GameObjects.Add(new SpellExample(new Point((Width - 1) * CellSize + 10, (Height - 2) * CellSize + 10), this));
    }

    public void CellClicked(Point location)
    {
        if (!isCastingSpell)
        {
            Player.Move(location);
        }
        else
        {
            Debug.WriteLine("Spell used");
            if (Player.Location == location)
            {
                Debug.WriteLine("Player got in way");
                Debug.Write("HP went from " + Player.health + " to ");
                Player.Damage(rand.Next(0, 10));
                Debug.WriteLine(Player.health);
            }
            isCastingSpell = !isCastingSpell;
            Debug.WriteLine("Stopped casting spell");
        }
    }

    public ObservableCollection<GameObject> GameObjects { get; set; }
    
    
}