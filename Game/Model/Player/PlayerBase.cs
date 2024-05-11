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
	private SpellNotTargeted spellNotTargeted;
	private SpellTargeted spellTargeted;

	// Map
	private GameMap _map;

	// Health and postion
	public int health = 100;

	// How many moves left and how many stamina one move costs
	protected int movesLeft = 4;
	protected int moveCost = 1;
	
	// Stamina
	protected int stamina = 10;

    public PlayerBase(Point location) : base(location)
    {
    }

    /// <summary>
    /// Use this method to move player to another cell
    /// </summary>
    public virtual void Move(Point newLocation)
	{
		int cost = (Utilities.CountMovesFromCellToCell(Location, newLocation))/100;
		if (cost > movesLeft)
		{
			Debug.WriteLine("Sorry. I can't move that far");
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
		health -= x;
	}
}
