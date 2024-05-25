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
	public Fireball(Point location):base(location)
	{
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
            Debug.WriteLine("Opacity changed");
        }
    }
}
