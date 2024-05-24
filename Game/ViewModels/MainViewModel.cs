using System;
using Game.Model;

using System.Collections.ObjectModel;

using Game.Model.Player;

using Point = Avalonia.Point;
using System.Diagnostics;
using Avalonia.Input;
using DynamicData;
using Game.Model.Spells;
using Game.Views;
using Avalonia.Controls;
using ReactiveUI;
using Tmds.DBus.Protocol;
using Avalonia.Threading;
using Avalonia.Collections;
using Microsoft.CodeAnalysis;

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
    public int Width { get; set; } = 7;
    public PlayerBase Player { get; set; }
    public Fireball Fireball { get; set; }
    public GameControl GameControl { get; set; }

    public MainViewModel()
    {
        GameObjects = [];
        Player = new PlayerClass1(
            new Point(3 * CellSize, 2 * CellSize),
            new SpellTargeted(new Point(10, CellSize + 10), this),
            new SpellAOE(new Point((Width - 1) * CellSize + 10, CellSize + 10), this));
        GameObjects.Add(Player);
        for (int i = 1; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObjects.Add(new MapCell(new Point(i * CellSize, j * CellSize), this));
            }
        }
        
        GameObjects.Add(Player.spellTargeted);
        GameObjects.Add(Player.spellAOE);
        Fireball = new Fireball(new Point(0, 0));
        Fireball.Active = false;
        GameObjects.Add(Fireball);

        GameControl = new(Player, Fireball);
        GameControl.TargetLocation = new Point(0, 0);

        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = new TimeSpan(0, 0, 0, 1000 / 60);
        timer.Tick += delegate
        {
            OnTimedEvent();
        };
        timer.IsEnabled = true;
        timer.Start();  

    }

/*        
PSEUDOCODE
Player logic (spell, move etc.)
Other objects
Move objects
Other actions
 */

    private void OnTimedEvent()
    {
        Debug.WriteLine("timer works");
        if (GameControl.TargetLocation!=Fireball.Location && Fireball.Active==true)
        {
            Debug.WriteLine("entered");
            Fireball.Location = new Point(Fireball.Location.X + GameControl.xDiff, Fireball.Location.Y + GameControl.yDiff);
        }
        if (GameControl.TargetLocation==Fireball.Location)
        {
            Fireball.Active = false;
            GameControl.TargetLocation = new Point(0, 0);
            Fireball.Location = new Point(0, 0);
        }
    }

    public void CellClicked(Point location)
    {
        Debug.WriteLine("Entered MapCell condition");
        Player.DoAction(location);
    }

    public ObservableCollection<GameObject> GameObjects { get; set; }
    
    
}