using System;
using System.Diagnostics;
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
}
