using Game.Model.Player;
using System;
using System.Diagnostics;
using Point = Avalonia.Point;

namespace Game.Model;

public class GameControl
{
	PlayerBase player {  get; set; }
	Fireball fireball { get; set; }

    public double xDiff {get; set; }
    public double yDiff {get; set; }
	public Point targetLocation;
    public Point TargetLocation 
	{ 
		get => targetLocation; 
		set
		{
			targetLocation = value;
			xDiff = targetLocation.X / (1000/60);
			yDiff = targetLocation.Y / (1000 / 60);
			Debug.WriteLine(targetLocation);
			Debug.WriteLine(string.Format("xDiff[{0}] yDiff[{1}]", xDiff, yDiff));
		}
	}
    public GameControl(PlayerBase player, Fireball fireball)
	{
		this.player = player;
		this.fireball = fireball;
	}
}
