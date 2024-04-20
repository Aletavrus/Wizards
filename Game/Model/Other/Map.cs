namespace Game.Model.Other;

public class Map
{
    private int[,] _map = new int[5,5];

    public Map()
    {
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                _map[i, j] = 0; // 0 means empty
            }
        }
    }

    public void PutValueToCell(int value, int x, int y)
    {
        _map[x, y] = value;
    }
}