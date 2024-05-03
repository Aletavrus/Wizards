using Avalonia;
using System;

namespace Game.Model;

public static class Utilities
{
    /// <summary>
    /// Count how many moves need to be made to move from one point to the other
    /// </summary>
    /// <param name="xFrom">first X coordinate</param>
    /// <param name="yFrom">first Y coordinate</param>
    /// <param name="xTo">second X coordinate</param>
    /// <param name="yTo">second Y coordinate</param>
    /// <returns></returns>
    public static int CountMovesFromCellToCell(int xFrom, int yFrom, int xTo, int yTo)
    {
        int changesInX = Math.Abs(xTo - xFrom);
        int changesInY = Math.Abs(yTo - yFrom);

        return changesInX + changesInY;
    }

    public static int CountMovesFromCellToCell(Point from, Point to)
    {
        int changesInX = Math.Abs((int)to.X - (int)from.X);
        int changesInY = Math.Abs((int)to.Y - (int)from.Y);

        return changesInX + changesInY;
    }
}