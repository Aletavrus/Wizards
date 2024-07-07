using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Game.ViewModels;
using ReactiveUI;
namespace Game.Model;

public class Fireball : GameObject
{
    public bool Active { get; set; }
    public bool OnArea {  get; set; }
    public double sizeDiff = 0.1D;
    public double MaxSize;
    public double range;
    public bool FireGrowing {get; set; }
	public Fireball(Point location, double range):base(location)
	{
        Active = false;
        OnArea = false;
        FireGrowing = false;
        MaxSize = range * 2 + 1;
        this.range = range;
	}

    private double _opacity = 0.0;
    public double Opacity
    {
        get
        {
            return _opacity;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref _opacity, value);
        }
    }

    private double _height = 1D;
    public double Height
    {
        get
        {
            return _height;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref _height, value);
        }
    }

    private double _width = 1D;
    public double Width
    {
        get
        {
            return _width;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref _width, value);
        }
    }

    public void MoveToArea()
    {
        double leftX = Location.X - range*100;
        if (leftX < 0)
        {
            leftX = Location.X;
        }
        double leftY = Location.Y - range*100;
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
            Opacity = 1.0;
            return;
        }
        Opacity = 0.0;
    }
}
