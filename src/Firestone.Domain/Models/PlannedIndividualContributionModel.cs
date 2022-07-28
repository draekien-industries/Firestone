namespace Firestone.Domain.Models;

using Data;

public class PlannedIndividualContributionModel : EntityDomainModel<PlannedIndividualContributionConfiguration>
{
    public PlannedIndividualContributionModel(double monthlyIncome, double monthlyContribution)
    {
        MonthlyIncome = monthlyIncome;
        MonthlyContribution = monthlyContribution;
    }

    public PlannedIndividualContributionModel(PlannedIndividualContributionConfiguration entity) : base(entity.Id)
    {
        MonthlyIncome = entity.MonthlyIncome;
        MonthlyContribution = entity.MonthlyContribution;
    }

    public double MonthlyIncome { get; }

    public double MonthlyContribution { get; }

    public double SavingsRate => MonthlyContribution / MonthlyIncome;

    public double ExpenseRate => 1 - SavingsRate;

    /// <inheritdoc />
    protected override PlannedIndividualContributionConfiguration CreateEntity()
    {
        return new PlannedIndividualContributionConfiguration
        {
            MonthlyContribution = MonthlyContribution,
            MonthlyIncome = MonthlyIncome,
        };
    }
}
