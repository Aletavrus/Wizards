using System;
using System.Reflection.Metadata.Ecma335;

public abstract class PlayerBase
{
	public int health = 100;
	protected int[] position = { 0, 0 };
	public int moves = 3;

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
