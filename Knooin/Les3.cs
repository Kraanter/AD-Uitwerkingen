namespace Knooin;

public static class Les3
{
    public static void Run()
    {
        Console.WriteLine(sum(123));
        Console.WriteLine(sum(2525));
        
        Console.WriteLine(reverse("hallo"));
        Console.WriteLine(reverse("Dit is een test"));
    }

    private static int sum(int n)
    {
        if (n < 10) return n;
        
        return n % 10 + sum(n / 10);
    }

    private static string reverse(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";

        return reverse(s.Substring(1)) + s[0];
    }
}