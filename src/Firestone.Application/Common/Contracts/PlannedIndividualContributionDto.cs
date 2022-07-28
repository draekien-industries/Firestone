namespace Firestone.Application.Common.Contracts;

using Domain.Models;
using Waystone.Common.Application.Contracts.Mappings;

public class PlannedIndividualContributionDto : IMapFrom<PlannedIndividualContributionModel>
{
    public Guid Id { get; set; }

    public double MonthlyIncome { get; set; }

    public double MonthlyContribution { get; set; }
}
