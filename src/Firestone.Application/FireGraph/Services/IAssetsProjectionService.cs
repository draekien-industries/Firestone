namespace Firestone.Application.FireGraph.Services;

using Domain.Models;

public interface IAssetsProjectionService
{
    Task<IEnumerable<DataPoint>> GetProjectedAssets(
        DataPoint projectFrom,
        int monthsElapsed,
        int numberOfMonthsToProject,
        double projectionRate,
        double monthlyContribution = 0,
        CancellationToken cancellationToken = default);
}

public class AssetsProjectionService : IAssetsProjectionService
{
    public Task<IEnumerable<DataPoint>> GetProjectedAssets(
        DataPoint projectFrom,
        int monthsElapsed,
        int numberOfMonthsToProject,
        double projectionRate,
        double monthlyContribution = 0,
        CancellationToken cancellationToken = default)
    {
        DataPoint previous = projectFrom;

        List<DataPoint> projectedAssets = new();

        for (int month = monthsElapsed; month < numberOfMonthsToProject; month++)
        {
            if (cancellationToken.IsCancellationRequested) break;

            DateTime projectedDate = previous.Date.AddMonths(1);
            double projectedAmount = ProjectionCalculator.Calculate(
                previous.Amount,
                projectionRate,
                monthlyContribution);

            DataPoint dataPoint = new(projectedDate, projectedAmount);

            projectedAssets.Add(dataPoint);

            previous = dataPoint;
        }

        return Task.FromResult<IEnumerable<DataPoint>>(projectedAssets);
    }
}
