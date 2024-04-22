using Avalonia.Controls;
using Avalonia.Interactivity;
using Game.ViewModels;
using Tmds.DBus.Protocol;
using Wizards.Player;

namespace Game.Views;

public partial class MainView : UserControl
{
    MainViewModel viewModel = new MainViewModel();
    public MainView()
    {
        InitializeComponent();
    }

    public void ClickHandler(Button sender, RoutedEventArgs e)
    {
        int x = Grid.GetColumn(sender);
        int y = Grid.GetRow(sender);
        int[] pos = { x, y };
        (this.DataContext as MainViewModel).MakeMove(pos);
    }
}
