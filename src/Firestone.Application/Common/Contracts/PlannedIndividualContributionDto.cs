namespace Firestone.Application.Common.Contracts;

using Domain.Data;
using Waystone.Common.Application.Contracts.Mappings;

public class PlannedIndividualContributionDto : IMapFrom<PlannedIndividualContributionConfiguration>
{
    public Guid Id { get; set; }

    public double MonthlyIncome { get; set; }

    public double MonthlyContribution { get; set; }
}
