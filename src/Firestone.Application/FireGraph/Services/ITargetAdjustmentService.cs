namespace Firestone.Application.FireGraph.Services;

using Domain.Models;

public interface ITargetAdjustmentService
{
    Task<IEnumerable<DataPoint>> GetAdjustedTargetsAsync(
        DataPoint adjustFrom,
        int numberOfMonths,
        double adjustmentRate,
        double monthlyContributions = 0,
        CancellationToken cancellationToken = default);
}

public class TargetAdjustmentService : ITargetAdjustmentService
{
    public Task<IEnumerable<DataPoint>> GetAdjustedTargetsAsync(
        DataPoint adjustFrom,
        int numberOfMonths,
        double adjustmentRate,
        double monthlyContributions = 0,
        CancellationToken cancellationToken = default)
    {
        List<DataPoint> adjustedTargets = new();

        double previousAmount = adjustFrom.Amount;

        for (var month = 1; month < numberOfMonths; month++)
        {
            if (cancellationToken.IsCancellationRequested) break;

            DateTime adjustedDate = adjustFrom.Date.AddMonths(month);
            double adjustedAmount = ProjectionCalculator.Calculate(
                previousAmount,
                adjustmentRate,
                monthlyContributions);

            DataPoint dataPoint = new(adjustedDate, adjustedAmount);

            adjustedTargets.Add(dataPoint);

            previousAmount = adjustedAmount;
        }

        return Task.FromResult<IEnumerable<DataPoint>>(adjustedTargets);
    }
}
