using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;
namespace Game.Model;

public class Fireball : GameObject
{
    public bool Active { get; set; }
    public bool OnArea {  get; set; }
    public double sizeDiff = 0.1D;
    public double MaxSize = 2.99D;
    public bool fireGrowing = false;
	public Fireball(Point location):base(location)
	{
        Active = false;
        OnArea = false;
	}

    private double fireOpacity = 0.0;
    public double FireOpacity
    {
        get
        {
            return fireOpacity;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref fireOpacity, value);
        }
    }

    private double height = 1D;
    public double FireHeight
    {
        get
        {
            return height;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref height, value);
        }
    }

    private double width = 1D;
    public double FireWidth
    {
        get
        {
            return width;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref width, value);
        }
    }

    public void MoveToArea()
    {
        double leftX = Location.X - 100;
        if (leftX < 0)
        {
            leftX = Location.X;
        }
        double leftY = Location.Y - 100;
        if (leftY < 0)
        {
            leftY = Location.Y;
        }
        Location = new Point(leftX, leftY);
    }

    public void ChangeState()
    {
        Active = !Active;
        if (Active)
        {
            FireOpacity = 1.0;
            return;
        }
        FireOpacity = 0.0;
    }
}
