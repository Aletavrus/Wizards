using System;

public abstract class PlayerBase
{
	public int health = 100;
	protected int[] position = { 0, 0 };
	public int moves;

	public virtual void Move(int x, int y)
	{
		position[0] += x;
		position[1] += y;
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
