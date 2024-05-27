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
namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public const int CellSize = 100;

    public int Height { get; set; } = 5;
    public int Width { get; set; } = 7;
    public PlayerBase Player { get; set; }
    public Fireball Fireball { get; set; }
    public GameControl GameControl { get; set; }
    public DispatcherTimer Timer {  get; set; }

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
        GameObjects.Add(Fireball);

        GameControl = new(Player, Fireball);
        GameControl.TargetLocation = new Point(0, 0);

        Timer = new DispatcherTimer();
        Timer.Interval = new TimeSpan(0, 0, 0, 0, 1000/60);
        Timer.Tick += delegate
        {
            OnTimedEvent();
        };
        Timer.IsEnabled = true;
        Timer.Start();
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
        if (Player.Location!=Fireball.Location && !Fireball.Active && Player.CurrentAction==0 && !Fireball.fireGrowing)
        {
            Fireball.Location = Player.Location;
            GameControl.Fireball.Location = Player.Location;
        }
        if (Fireball.FireHeight==3)
        {
            Fireball.fireGrowing = false;
            Fireball.FireHeight = 1;
            Fireball.FireWidth = 1;
            GameControl.TargetLocation = Player.Location;
            Fireball.Location = Player.Location;
        }
        else if (Fireball.fireGrowing)
        {
            Fireball.FireHeight += Fireball.sizeDiff;
            Fireball.FireWidth += Fireball.sizeDiff;
        }
        else
        {
            if (!Fireball.Active)
            {
                Fireball.FireOpacity = 0.0;
            }
            else if (Fireball.Active)
            {
                Fireball.FireOpacity = 1.0;
            }
            if (GameControl.TargetLocation != Fireball.Location && Fireball.Active)
            {
                Fireball.Location = new Point(Fireball.Location.X + GameControl.xDiff, Fireball.Location.Y + GameControl.yDiff);
            }
            else if (GameControl.TargetLocation == Fireball.Location && Fireball.Location != Player.Location)
            {
                if (Fireball.OnArea)
                {
                    Fireball.Location = new Point(Fireball.Location.X - 100, Fireball.Location.Y - 100);
                    Fireball.FireHeight = 3;
                    Fireball.FireWidth = 3;
                    Fireball.OnArea = false;
                    Fireball.fireGrowing = true;
                }
                Fireball.Active = false;
            }
        }
    }
    public void CellClicked(Point location)
    {
        Player.DoAction(location);
    }

    public ObservableCollection<GameObject> GameObjects { get; set; }    
}