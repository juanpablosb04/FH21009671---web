using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("SumFor:");
        FindValidValues(SumFor, "SumFor");
        Console.WriteLine("\nSumIte:");
        FindValidValues(SumIte, "SumIte");
    }

    static int SumFor(int n)
    {
        return n * (n + 1) / 2;
    }

    static int SumIte(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i;
        }
        return sum;
    }

    static void FindValidValues(Func<int, int> method, string name)
    {
        int max = int.MaxValue;

        // Este es el ascendente
        int lastN = 0, lastSum = 0;
        for (int n = 1; n <= max; n++)
        {
            int sum = method(n);
            if (sum > 0)
            {
                lastN = n;
                lastSum = sum;
            }
            else
            {
                break;
            }
        }
        Console.WriteLine($"\tFrom 1 to Max → n: {lastN} → sum: {lastSum}");

        // Este es el descendente
        int firstN = 0, firstSum = 0;
        for (int n = max; n >= 1; n--)
        {
            int sum = method(n);
            if (sum > 0)
            {
                firstN = n;
                firstSum = sum;
                break;
            }
        }
        Console.WriteLine($"\tFrom Max to 1 → n: {firstN} → sum: {firstSum}");
    }
}
