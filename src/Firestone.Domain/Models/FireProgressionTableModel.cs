namespace Firestone.Domain.Models;

using Data;

public class FireProgressionTableModel : EntityDomainModel<FireProgressionTable>
{
    public FireProgressionTableModel()
    {
        AssetHolders = new List<AssetHolderModel>();
        Entries = new List<FireProgressionTableEntryModel>();
    }

    public FireProgressionTableModel(
        FireProgressionTable entity) : base(entity.Id)
    {
        if (entity.InflationRateConfiguration != null)
        {
            InflationRate = new InflationRateModel(entity.InflationRateConfiguration);
        }

        if (entity.NominalReturnRateConfiguration != null)
        {
            NominalReturnRate = new NominalReturnRateModel(entity.NominalReturnRateConfiguration);
        }

        if (entity.RetirementTargetConfiguration != null)
        {
            RetirementTarget = new RetirementTargetModel(entity.RetirementTargetConfiguration);
        }

        AssetHolders = entity.AssetHolders.Select(x => new AssetHolderModel(x)).ToList();

        Entries = entity.FireProgressionTableEntries.Select(x => new FireProgressionTableEntryModel(x)).ToList();
    }

    public InflationRateModel? InflationRate { get; }

    public NominalReturnRateModel? NominalReturnRate { get; }

    public RetirementTargetModel? RetirementTarget { get; }

    public List<AssetHolderModel> AssetHolders { get; }

    public List<FireProgressionTableEntryModel> Entries { get; }

    public void EnsureTableCanBePopulated()
    {
        if (Entries.Count != 1)
        {
            throw new InvalidOperationException("Can only populate when table contains a single initial investment");
        }

        if (InflationRate is null)
        {
            throw new InvalidOperationException("Inflation Rate must be configured before populating table");
        }

        if (NominalReturnRate is null)
        {
            throw new InvalidOperationException("Nominal Return Rate must be configured before populating table");
        }

        if (RetirementTarget is null)
        {
            throw new InvalidOperationException("Retirement Target must be configured before populating table");
        }

        if (!AssetHolders.Any())
        {
            throw new InvalidOperationException("Asset Holders must be configured before populating table");
        }

        if (!RetirementTarget.MinimumMonthlyContributionValue.HasValue)
        {
            throw new InvalidOperationException(
                "Retirement Target must have a minimum monthly contribution value before populating table");
        }

        if (!RetirementTarget.MinimMonthlyGrowthRate.HasValue)
        {
            throw new InvalidOperationException(
                "Retirement Target must have a minimum monthly growth rate before populating table");
        }
    }

    /// <inheritdoc />
    protected override FireProgressionTable CreateEntity()
    {
        return new FireProgressionTable
        {
            InflationRateConfiguration = InflationRate?.ToEntity(),
            NominalReturnRateConfiguration = NominalReturnRate?.ToEntity(),
            RetirementTargetConfiguration = RetirementTarget?.ToEntity(),
            AssetHolders = AssetHolders.Select(x => x.ToEntity()).ToList(),
        };
    }
}
