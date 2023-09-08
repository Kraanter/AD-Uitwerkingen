namespace Knooin;

public static class Util
{
    public static void Print<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(i + ": " + list[i]);
        }
    }
}