using Avalonia;
using System;
using System.Diagnostics;

namespace Game.Model;

public class GameMap
{
    public const int CellSize = 100;
    public int Height { get; set; } = 7;
    public int Width { get; set; } = 5;

    public GameMap()
    {
        GameObjects = new int[5, 5];
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
        int maxY = GameObjects.GetLength(1);
        if (y+range<maxY)
        {
            maxY = y+range;
        }
        int maxX = GameObjects.GetLength(0);
        if (x+range<maxX)
        {
            maxX = x+range;
        }
        for (int i = x; i < maxX; i++)
        {
            for (int j = y; i < maxY; j++)
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