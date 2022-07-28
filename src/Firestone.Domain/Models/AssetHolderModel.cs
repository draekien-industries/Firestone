namespace Firestone.Domain.Models;

using Data;

public class AssetHolderModel : EntityDomainModel<AssetHolder>
{
    public AssetHolderModel(Guid tableId, string name, double monthlyIncome, double monthlyContribution)
    {
        Name = name;
        TableId = tableId;
        PlannedIndividualContribution = new PlannedIndividualContributionModel(monthlyIncome, monthlyContribution);
        IndividualAssetsTotals = new List<IndividualAssetsTotalModel>();
    }

    public AssetHolderModel(
        AssetHolder entity) : base(entity.Id)
    {
        TableId = entity.TableId;
        Name = entity.Name;
        PlannedIndividualContribution =
            new PlannedIndividualContributionModel(entity.PlannedIndividualContributionConfiguration);
        IndividualAssetsTotals = entity.IndividualAssetValues.Select(x => new IndividualAssetsTotalModel(x)).ToList();
    }

    public Guid TableId { get; }

    public string Name { get; }

    public PlannedIndividualContributionModel PlannedIndividualContribution { get; }

    public List<IndividualAssetsTotalModel> IndividualAssetsTotals { get; }

    /// <inheritdoc />
    protected override AssetHolder CreateEntity()
    {
        AssetHolder entity = new()
        {
            Name = Name,
            TableId = TableId,
            IndividualAssetValues = IndividualAssetsTotals.Select(x => x.ToEntity()).ToList(),
            PlannedIndividualContributionConfiguration = PlannedIndividualContribution.ToEntity(),
        };

        return entity;
    }
}
