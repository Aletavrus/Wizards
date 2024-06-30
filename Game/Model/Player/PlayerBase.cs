using System;
using System.Collections.Generic;
using Avalonia;

using ReactiveUI;

using Game.Model.Spells;
using System.Diagnostics;
using System.Linq;
using Avalonia.Remote.Protocol.Input;
using Game.Model.Effects;
using Game.ViewModels;

namespace Game.Model.Player;

public class PlayerBase : GameObject
{
	// Spells
	public SpellTargeted spellTargeted;
	public SpellAOE spellAOE;

	// Map
	public GameMap GameMap {get; set;}

	// Health and position
	public int health = 100;

	// How many moves left and how many stamina one move costs
	public int movesLeft = 4;
	protected int moveCost = 1;

	// Current action
    protected int currentAction = 0; // 0 - move; 1 - SpellTargeted; 2 - SpellAOE
    
    // Effects
    protected List<EffectBase> effects = new List<EffectBase>();
    
	public int CurrentAction
	{
		get => currentAction;
		set => currentAction = value;
	}

	// Stamina
	protected int stamina = 10;	
    public PlayerBase(Point location, SpellTargeted spellTargeted, SpellAOE spellAOE, GameMap gameMap) : base(location)
    {
	    this.spellTargeted = spellTargeted;
	    this.spellAOE = spellAOE;
		GameMap = gameMap;
    }

	public void DoAction(Point location)
	{
        switch (currentAction)
        {
            case 0:
                Move(location);
				break;
            case 1:
                spellTargeted.Execute(location);
				break;
            case 2:
                spellAOE.Execute(location);
				break;
            default:
                Log("[ERROR] INVALID TYPE OF SPELL");
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Use this method to move player to another cell
    /// </summary>
    public virtual void Move(Point newLocation)
	{
		int cost = (Utilities.CountMovesFromCellToCell(Location, newLocation))/100;
		if (cost > movesLeft)
		{
			Debug.WriteLine("Sorry. I can't move that far :(");
		}
		else
		{
			movesLeft -= cost; // Removing moves
            int value = GameMap.GameObjects[Convert.ToInt32(Location.X)/100, Convert.ToInt32(Location.Y)/100]; //saving the index of a player on GameMap
            GameMap.PutValueToCell(0, Convert.ToInt32(Location.X), Convert.ToInt32(Location.Y)); // Changing old cell to 0
			Location = newLocation;
			Log("Location changed");
			GameMap.PutValueToCell(value, Convert.ToInt32(newLocation.X), Convert.ToInt32(newLocation.Y)); // Changing new cell to 1
			spellAOE.GameMap.GameObjects = GameMap.GameObjects;
			spellTargeted.GameMap.GameObjects = GameMap.GameObjects;
		}
	}

	public virtual void Damage(int x)
	{
		Debug.Write("[Player] HP went from " + health + " to ");
		health -= x;
		Debug.WriteLine(health);
	}


	public void AddEffects(EffectBase effect)
	{
		effects.Add(effect);
	}
	
	public void EffectsActions()
	{
		if (effects.Count != 0)
		{
			foreach (var effect in effects.ToList())
			{
				effect.ActivateEffects(this);
			
				if (!effect.ReduceDuration(1))
				{
					effects.Remove(effect);
				}
			}
		}
		
	}
}
