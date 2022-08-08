namespace Firestone.Application.FireGraph.Services;

using Domain.Utils;

public class ProjectionCalculator
{
    public static double Calculate(double previous, double monthlyNominalReturnRate, double contribution)
    {
        return MathUtils.IncreaseByPercentage(previous, monthlyNominalReturnRate) + contribution;
    }
}
