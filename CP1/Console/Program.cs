﻿public class Numbers
{
    private static readonly double N = 25;

    public static double Formula(double z)
    {
        return Round((z + Math.Sqrt(4 + Math.Pow(z, 2))) / 2);
    }

    public static double Recursive(double z)
    {
        return Round(Recursive(z, N) / Recursive(z, N - 1));
    }

    public static double Iterative(double z)
    {
        return Round(Iterative(z, N) / Iterative(z, N - 1));
    }

    private static double Recursive(double z, double n)
    {
            //ChatGPT
        int Nint = (int)n;

        if (Nint == 0 || Nint == 1)
        return 1;

        return z * Recursive(z, Nint - 1) + Recursive(z, Nint - 2);
    }

    private static double Iterative(double z, double n)
    {
            //ChatGPT
        int Nint = (int)n;

        if (Nint == 0 || Nint == 1)
            return 1;

        double prev2 = 1;
        double prev1 = 1;
        double current = 0;

        for (int i = 2; i <= Nint; i++)
        {
            current = z * prev1 + prev2;
            prev2 = prev1;
            prev1 = current;
        }

        return current;
    }

    private static double Round(double value)
    {
        return Math.Round(value, 10);
    }

    public static void Main(String[] args)
    {
        String[] metallics = [
            "Platinum", // [0]
            "Golden", // [1]
            "Silver", // [2]
            "Bronze", // [3]
            "Copper", // [4]
            "Nickel", // [5]
            "Aluminum", // [6]
            "Iron", // [7]
            "Tin", // [8]
            "Lead", // [9]
        ];
        for (var z = 0; z < metallics.Length; z++)
        {
            Console.WriteLine("\n[" + z + "] " + metallics[z]);
            Console.WriteLine(" ↳ formula(" + z + ")   ≈ " + Formula(z));
            Console.WriteLine(" ↳ recursive(" + z + ") ≈ " + Recursive(z));
            Console.WriteLine(" ↳ iterative(" + z + ") ≈ " + Iterative(z));
        }
    }
}
