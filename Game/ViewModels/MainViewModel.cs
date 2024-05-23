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

namespace Game.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //private void ButtonClick(int x, int y)
    //{
    //    PlayerBase player = new PlayerBase(); // for an example. when we implement the init of a player in the beginning, I will change --ALEXIS--
    //    player.Move(x,y);
    //}

    public const int CellSize = 100;

    public bool isCastingSpell = false;
    public int typeOfSpell = 0; // 0 - no spell; 1 - SpellTargeted; 2 - SpellAOE

    public int Height { get; set; } = 5;
    public int Width { get; set; } = 7;
    public PlayerBase Player { get; set; }
    public Fireball Fireball { get; set; }

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
        /*GameObjects.Add(new SpellExample(new Point(10, (Height - 2) * CellSize + 10), this));
        GameObjects.Add(new SpellExample(new Point((Width - 1) * CellSize + 10, (Height - 2) * CellSize + 10), this));*/
    }

    public void CellClicked(Point location)
    {
        if (!isCastingSpell)
        {
            Player.Move(location);
        }
        else
        {
            Fireball.FireOpacity = 0.5;
            switch (typeOfSpell)
            {
                case 1:
                    Player.spellTargeted.Execute(location); 
                    break;
                case 2:
                    Player.spellAOE.Execute(location);
                    break;
                default:
                    Debug.WriteLine("[ERROR] INVALID TYPE OF SPELL");
                    break;
            }
            Debug.WriteLine("End of Spells");
            Fireball.Location = new Point(0, 0);
        }
    }

    public ObservableCollection<GameObject> GameObjects { get; set; }
    
    
}