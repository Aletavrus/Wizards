using System;
using System.Reflection.Metadata.Ecma335;
using Game.Model.Other;
using Wizards.Spells;
namespace Wizards.Player;

public abstract class PlayerBase
{
	// Spells
	SpellNotTargeted spellNotTargeted;
	SpellTargeted spellTargeted;
	
	// Map
	Map map;
	
	// Health and postion
	public int health = 100;
	protected int[] position = { 0, 0 };
	
	// How many moves left and how many stamina one move costs
	protected int movesLeft = 4;
	protected int moveCost = 1;
	
	// Stamina
	protected int stamina = 10;

	/*
	 
	 COMMENTED BECAUSE THERE IS NO NEED IN IT - Jeany
	 
	public virtual void Move(int x, int y)
	{
		int diff1 = Math.Abs(x - position[0]);
		int diff2 = Math.Abs(y - position[1]);
		if (moves<diff1+diff2)
		{
			//should be a cycle to wait until player clicks on a possible check
		}
        position[0] = x;
		position[1] = y;
		
	}*/

	/// <summary>
	/// Use this method to move player to another cell
	/// </summary>
	/// <param name="x">coordinate X</param>
	/// <param name="y">coordinate Y</param>
	public virtual void Move(int x, int y)
	{
		int cost = Utilities.CountMovesFromCellToCell(position[0], position[1], x, y);
		if (cost > movesLeft)
		{
			Console.WriteLine("Sorry. I can't move that far");
		}
		else
		{
			movesLeft -= cost; // Removing moves
			map.PutValueToCell(0, position[0], position[1]); // Changing old cell to 0
			(position[0], position[1]) = (x, y); // Changing coordinates in our class
			map.PutValueToCell(1, x ,y); // Changing new cell to 1
		}
	}

	public virtual void Damage(int x)
	{
		health -= x;
	}

	public int[] GetPosition()
	{
		return position;
	}

	public int GetPositionX()
	{
		return position[0];
	}

	public int GetPositionY()
	{
		return position[1];
	}
}
