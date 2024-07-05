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
using Avalonia.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls.Primitives;
using Game.Model.Effects;

namespace Game.ViewModels;

public class MainViewModel : ViewModelBase
{
    public GameMap GameMap { get; set; }
    public const int CellSize = 100;
    public int Height { get; set; } = 7;
    public int Width { get; set; } = 9;
    public PlayerBase Player { get; set; }
    public PlayerBase Player1 { get; set; }
    public PlayerBase Player2 { get; set; }
    private int index = 0;
    PlayerBase[] Players { get; set; }

    public SpellButtonAOE SpellButtonAOE { get; set; }
    public SpellButtonTargeted SpellButtonTargeted { get; set; }
    public SpellButtonGround SpellButtonGround { get; set; }
    public Fireball Fireball { get; set; }
    public GameControl GameControl { get; set; }
    public DispatcherTimer Timer {  get; set; }

    private const string path = "D:\\Git Projects\\Wizards\\Game\\Data.json";
    private JsonSerializerOptions options = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
        IncludeFields = true
    };


    public MainViewModel()
    {
        //string data = File.ReadAllText(path);
        //SpellBase[] spells = JsonSerializer.Deserialize<SpellBase[]>(data, options);
        //foreach (var spell in spells)
        //{
        //    Debug.WriteLine($"Name:{spell.GetType()}, Location:{spell.Location}");
        //}
        GameMap = new GameMap();

        GameObjects = [];

        // Creating first player
        Player1 = new PlayerBase( 
            new Point(3 * CellSize, 5 * CellSize),
            new SpellTargeted(GameMap),
            new SpellAOE(GameMap),
            new SpellGround(GameMap, new EffectSlow(2, 2)),
            GameMap);

        // Adding test effect !!
        //Player1.AddEffects(new EffectPoison(10, 3));
        //Player1.AddEffects(new EffectSlow(2, 2));
        
        // Creating second player
        Player2 = new PlayerBase( 
            new Point(3 * CellSize, 2 * CellSize),
            new SpellTargeted(GameMap),
            new SpellAOE(GameMap),
            new SpellGround(GameMap, new EffectPoison(2, 2)),
            GameMap);

        // Adding two players to game objects
        GameObjects.Add(Player1);
        GameObjects.Add(Player2);

        // Creating array of players
        Players = new PlayerBase[] { Player1, Player2 };
        for (int i = 0; i<Players.Length; i++)
        {
            Players[i].GameMap.PutValueToCell(i+1, Convert.ToInt16(Players[i].Location.X), Convert.ToInt16(Players[i].Location.Y));
            GameMap.PutValueToCell(i+1, Convert.ToInt16(Players[i].Location.X), Convert.ToInt16(Players[i].Location.Y));
        }
        foreach (PlayerBase player in Players)
        {
            player.GameMap.GameObjects = GameMap.GameObjects;
            player.spellAOE.GameMap.GameObjects = GameMap.GameObjects;
            player.spellTargeted.GameMap.GameObjects = GameMap.GameObjects;
        }
        
        // Setting Player1 as a current player
        Player = Players[index];

        // Creating buttons for spells
        SpellButtonTargeted = new SpellButtonTargeted(new Point(10, 3*CellSize + 10));
        SpellButtonAOE = new SpellButtonAOE(new Point((Width - 1) * CellSize + 10, 3 * CellSize + 10));
        SpellButtonGround = new SpellButtonGround(new Point(10, 6*CellSize + 10)); 
        
        GameObjects.Add(SpellButtonTargeted);
        GameObjects.Add(SpellButtonAOE);
        GameObjects.Add(SpellButtonGround);
        
        for (int i = 1; i < 8; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObjects.Add(new MapCell(new Point(i * CellSize, j * CellSize), this));
            }
        }

        // GameObjects.Add(Player.spellTargeted);
        // GameObjects.Add(Player.spellAOE);
        Fireball = new Fireball(Player.Location);
        GameObjects.Add(Fireball);

        //SpellBase[] spells = new SpellBase[]
        //{
        //    Player.spellTargeted,
        //    Player.spellAOE
        //};
        //string data = JsonSerializer.Serialize<SpellBase[]>(spells, options);
        //File.WriteAllText(path, data);

        GameControl = new(Player, Fireball);
        GameControl.TargetLocation = Player.Location;

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
        if (fireballToPlayer()) //moving fireball's starting location, when player's location changes
        {
            Fireball.Location = Player.Location;
            GameControl.Fireball.Location = Player.Location;
            GameControl.TargetLocation = Player.Location;
            return;
        }
        if (Fireball.Height > Fireball.MaxSize || nearBorder()) //controlling an area of the fireball (when using spellAOE)
        {
            Fireball.FireGrowing = false;
            Fireball.Height = 1;
            Fireball.Width = 1;
            Fireball.Opacity = 0.0;
            return;
        }
        if (Fireball.FireGrowing) //growing when AOE used
        {
            Fireball.Height += Fireball.sizeDiff;
            Fireball.Width += Fireball.sizeDiff;
            return;
        }

        if (fireballMoves()) //checking if fireball is moving to the target
        {
            Fireball.Location = new Point(Fireball.Location.X + GameControl.xDiff, Fireball.Location.Y + GameControl.yDiff);
        }
        else if (GameControl.TargetLocation == Fireball.Location && Fireball.Location!=Player.Location) //in case when its on target, but not inside a player
        {
            Fireball.ChangeState();
            ChangeInvokeCommand();
            if (Fireball.OnArea)
            {
                Fireball.MoveToArea();
                Fireball.OnArea = false;
                Fireball.FireGrowing = true;
                Fireball.Opacity = 1.0;
            }
            Player.CurrentAction = 0; 
        }
    }
    public void CellClicked(Point location)
    {
        if (SpellButtonTargeted.Active)
        {
            Player.CurrentAction = 1;
        }
        else if (SpellButtonAOE.Active)
        {
            Player.CurrentAction = 2;
            Fireball.OnArea = true;
        }
        else if (SpellButtonGround.Active)
        {
            Player.CurrentAction = 3;
        }
        else
        {
            Player.CurrentAction = 0;
        }
        if (Player.CurrentAction !=0 && Player.CurrentAction!=3)
        {
            ChangeInvokeCommand(); //to stop any interactions while some spell is being casted
            Fireball.ChangeState();
        }
        GameControl.TargetLocation = location;
        Player.DoAction(location);
        switch (Player.CurrentAction)
        {
            case 0:
                break;
            case 1:
                if (Player.spellTargeted.playerHit!=0)
                {
                    Players[Player.spellTargeted.playerHit - 1].Damage(Player.spellTargeted.damage);
                }
                SpellButtonTargeted.Active = false;
                break;
            case 2:
                if (Player.spellAOE.playerHit != null)
                {
                    foreach (int counter in Player.spellAOE.playerHit)
                    {
                        Players[counter-1].Damage(Player.spellAOE.damage);
                    }
                }
                SpellButtonAOE.Active = false;
                break;
            case 3:
                if (Player.spellGround.playerHit !=  0)
                {
                    Players[Player.spellGround.playerHit - 1].AddEffects(Player.spellGround.effect);
                }
                SpellButtonGround.Active = false;
                break;
            default:
                Debug.WriteLine("[ERROR] INVALID TYPE OF ACTION");
                throw new NotImplementedException();
        }
        if (Player.CurrentAction==0)
        {
            SyncGameMaps();
            MoveFinished();
        }
    }

    private bool fireballToPlayer()
    {
        return Player.Location != Fireball.Location && !Fireball.Active && Player.CurrentAction == 0 && !Fireball.FireGrowing;
    }
    private bool fireballMoves()
    {
        return GameControl.TargetLocation != Fireball.Location && Fireball.Active;
    }
    private bool nearBorder()
    {
        return Fireball.Location.X + (Fireball.Width*CellSize) > (Width-1)*CellSize || Fireball.Location.Y + (Fireball.Height * CellSize) > Height * CellSize;
    }
    private void ChangeInvokeCommand()
    {
        var mapCells = GameObjects.OfType<MapCell>().ToList();
        foreach (MapCell mapCell in mapCells)
        {
            mapCell.InvokeCommand = !mapCell.InvokeCommand;
        }
        SpellButtonAOE.InvokeCommand = !SpellButtonAOE.InvokeCommand;
        SpellButtonTargeted.InvokeCommand = !SpellButtonTargeted.InvokeCommand;
    }

    public ObservableCollection<GameObject> GameObjects { get; set; }

    public void MoveFinished()
    {
        if (index+1<Players.Length)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        Player.movesLeft = 4;
        Player.EffectsActions();
        Player = Players[index];
    }

    public void SyncGameMaps()
    {
        foreach (PlayerBase player in Players)
        {
            player.GameMap.GameObjects = Player.GameMap.GameObjects;
            player.spellTargeted.GameMap.GameObjects = Player.GameMap.GameObjects;
            player.spellAOE.GameMap.GameObjects = Player.GameMap.GameObjects;
        }
    }
}