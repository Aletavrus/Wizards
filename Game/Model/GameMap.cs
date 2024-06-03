using Avalonia;
using System;

namespace Game.Model;

public class GameMap
{
    public const int CellSize = 100;
    public int Height { get; set; } = 7;
    public int Width { get; set; } = 5;

    public GameMap()
    {
        GameObjects = new int[6, 5];
        for (int i = 0; i < GameObjects.GetLength(0); i++)
        {
            for (int j = 0; j < GameObjects.GetLength(1); j++)
            {
                GameObjects[i, j] = 0; // 0 means empty
            }
        }
    }

    public int[,] GameObjects { get; set; }

    public void PutValueToCell(int value, int x, int y)
    {
        GameObjects[x/100, y/100] = value;
    }

    public bool InsideArea(int x, int y, int range)
    {
        for (int i =x; i < x+range; i++)
        {
            for (int j = y; i < y + range; j++)
            {
                if (GameObjects[i, j] == 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
}