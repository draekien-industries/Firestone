namespace Firestone.Domain.Utils;

public static class MathUtils
{
    public static double IncreaseByPercentage(double value, double percentInDecimals)
    {
        return value * (1 + percentInDecimals);
    }

    public static double CompoundInterest(
        double principle,
        double interestRate,
        int numberOfTimesCompounded,
        int numberOfTimesToCompound)
    {
        return principle
             * Math.Pow(1 + interestRate / numberOfTimesCompounded, numberOfTimesToCompound * numberOfTimesCompounded);
    }
}
