using UnityEngine;

public static class ProbabilityUtil
{
    public static float CalculateBinomialProbability(int n, int k, float p)
    {
        float probability = CalculateCombination(n, k) * Mathf.Pow(p, k) * Mathf.Pow(1 - p, n - k);
        return probability;
    }

    public static float CalculateCombination(int n, int k)
    {
        return CalculateFactorial(n) / (CalculateFactorial(k) * CalculateFactorial(n - k));
    }

    public static float CalculateFactorial(int n)
    {
        float factorial = 1;

        for (int i = 1; i <= n; i++)
        {
            factorial *= i;
        }

        return factorial;
    }
}
