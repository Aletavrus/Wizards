using System;

using Avalonia;

using ReactiveUI;

using Game.Model.Spells;
using System.Diagnostics;
using Avalonia.Remote.Protocol.Input;

namespace Game.Model.Player;

public abstract class PlayerBase : GameObject
{
	// Spells
	public SpellTargeted spellTargeted;
	public SpellAOE spellAOE;

	// Map
	private GameMap _map;

	// Health and position
	public int health = 100;

	// How many moves left and how many stamina one move costs
	protected int movesLeft = 4;
	protected int moveCost = 1;

    protected int currentAction = 0; // 0 - move; 1 - SpellTargeted; 2 - SpellAOE
	public int CurrentAction
	{
		get => currentAction;
		set => currentAction = value;
	}

	// Stamina
	protected int stamina = 10;
	
    public PlayerBase(Point location, SpellTargeted spellTargeted, SpellAOE spellAOE) : base(location)
    {
	    this.spellTargeted = spellTargeted;
	    this.spellAOE = spellAOE;
    }

	public void DoAction(Point location)
	{
		Log(currentAction.ToString());
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
                break;
        }
		currentAction = 0;
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
			//_map.PutValueToCell(0, Convert.ToInt32(Location.X), Convert.ToInt32(Location.Y)); // Changing old cell to 0
			Location = newLocation;
			Debug.WriteLine("Location changed");
			//_map.PutValueToCell(1, Convert.ToInt32(newLocation.X), Convert.ToInt32(newLocation.Y)); // Changing new cell to 1
			movesLeft = 4; //THIS IS GOING TO BE INSIDE A MVM, BUT NOW IT'S HERE. AT THE END IT WILL RESET AFTER THE END OF A TURN
		}
	}

	public virtual void Damage(int x)
	{
		Debug.Write("[Player] HP went from " + health + " to ");
		health -= x;
		Debug.WriteLine(health);
	}
}
