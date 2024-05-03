namespace Game.Model;

public class GameMap
{
    public const int CellSize = 100;

    public int Height { get; set; } = 5;
    public int Width { get; set; } = 5;

    public GameMap()
    {
        GameObjects = new object[5, 5];
        for (int i = 0; i < GameObjects.GetLength(0); i++)
        {
            for (int j = 0; j < GameObjects.GetLength(1); j++)
            {
                GameObjects[i, j] = 0; // 0 means empty
            }
        }
    }

    public object[,] GameObjects { get; set; }

    //public void PutValueToCell(int value, int x, int y)
    //{
    //    GameObjects[x, y] = value;
    //}
}