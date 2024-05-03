using ReactiveUI;

using Point = Avalonia.Point;

namespace Game.Model;

public class GameObject : ReactiveObject
{
    private Point _location;

    public GameObject(Point location)
    {
        Location = location;
    }

    public Point Location
    {
        get { return _location; }
        set
        {
            this.RaiseAndSetIfChanged(ref _location, value);
        }
    }
}