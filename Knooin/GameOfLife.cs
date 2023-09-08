using System.Text;

namespace Knooin;

public static class GameOfLife
{
    private static readonly Grid Grid;

    static GameOfLife()
    {
        Grid = new(Console.WindowWidth / 2, Console.WindowHeight - 4);
    }
    public static void Run()
    {
        while (true)
        {   
            Grid.Draw();
            Grid.NextGeneration();
            Thread.Sleep(175);
        }
    }
}

public class Grid
{
    private bool[,] _gridList;
    private readonly Random _random;
    private int Width { get; }
    private int Height { get; }
    
    public Grid(int width, int height)
    {
        if (width < 1) 
            throw new ArgumentOutOfRangeException(nameof(width), width, "Width should be more than or equal to 1");
        if (height < 1) 
            throw new ArgumentOutOfRangeException(nameof(height), height, "Height should be more than or equal to 1");
        
        Width = height;
        Height = width;

        _random = new Random();
        _gridList = this._createEmptyGridList();
        
        this._randomize();
    }

    private bool[,] _createEmptyGridList()
    {
        bool[,] gridList = new bool[Width,Height];

        return gridList;
    }

    private void _randomize()
    {
        for (int x = 0; x < this.Width; x++)
            for (int y = 0; y< this.Height; y++)
                this._setLocation(x, y,  _random.Next(100) < 15);  
    } 
    
    private bool _getLocation(int x, int y) => _gridList[x,y];

    // Will always return a value even for invalid coördinates
    public bool GetMaybeLocation(int x, int y)
    {
        if (x < 0 || Width <= x) return false;
        if (y < 0 || Height <= y) return false;

        return this._getLocation(x, y);
    }
    
    private void _setLocation(int x, int y, bool value) => _gridList[x,y] = value;

    private int _getAmountOfNeighbours(int x, int y)
    {
        int count = 0;

        for (int xToCheck = x - 1; xToCheck <= x + 1; xToCheck++)
            for (int yToCheck = y - 1; yToCheck <= y + 1; yToCheck++)
            {
                if (x == xToCheck && y == yToCheck) continue;
                if (this.GetMaybeLocation(xToCheck, yToCheck)) count++;
            }

        return count;
    }

    public void NextGeneration()
    {
        var nextGrid = this._createEmptyGridList();
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                int neigbours = this._getAmountOfNeighbours(x, y);
                bool curValue = this._getLocation(x, y);

                nextGrid[x, y] = _getNextGenValue(neigbours, curValue);
            }

        _gridList = nextGrid;
    }

    private static bool _getNextGenValue(int amountOfNeighbours, bool oldValue)
    {
        if (amountOfNeighbours >= 4) return false;
        if (amountOfNeighbours == 3) return true;
        if (oldValue && amountOfNeighbours == 2) return true;
        
        return false;
    }

    private static string _getPrintChar(bool on) => on ? "██" : "  ";

    public void Draw()
    {
        StringBuilder buffer = new();
        for (int x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
                buffer.Append(_getPrintChar(this._getLocation(x, y)));

            buffer.AppendLine();
        }
        
        Console.Clear();
        Console.Write(buffer.ToString());
    }
}
