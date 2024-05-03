using System;

using Avalonia;

using ReactiveUI;

using Game.Model.Spells;

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
    /// <param name="x">coordinate X</param>
    /// <param name="y">coordinate Y</param>
    public virtual void Move(Point newLocation)
	{
		int cost = Utilities.CountMovesFromCellToCell(Location, newLocation);
		if (cost > movesLeft)
		{
			Console.WriteLine("Sorry. I can't move that far");
		}
		else
		{
			//movesLeft -= cost; // Removing moves
			//map.PutValueToCell(0, position[0], position[1]); // Changing old cell to 0
			//(position[0], position[1]) = (x, y); // Changing coordinates in our class
			//map.PutValueToCell(1, x ,y); // Changing new cell to 1
		}
	}

	public virtual void Damage(int x)
	{
		health -= x;
	}

	//public int[] GetPosition()
	//{
	//	return position;
	//}

	//public int GetPositionX()
	//{
	//	return position[0];
	//}

	//public int GetPositionY()
	//{
	//	return position[1];
	//}
}
