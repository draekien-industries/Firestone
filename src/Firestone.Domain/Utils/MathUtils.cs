namespace Firestone.Domain.Utils;

public static class MathUtils
{
    public static double IncreaseByPercentage(double value, double percentInDecimals)
    {
        return value * (1 + percentInDecimals);
    }
}
