namespace Knooin;

public static class Zeef
{
    public static void Run()
    {
        List<int> numbers = Enumerable.Range(2, 500).ToList();

        for (int i = 0; i < numbers.Count; i++)
        {
            int multiple = numbers[i];
            for (int j = i + 1; j < numbers.Count; j++)
                if (numbers[j] % multiple == 0) 
                    numbers.RemoveAt(j);
        }
        
        numbers.Print();
    }
}
