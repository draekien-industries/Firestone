namespace Firestone.Application.FireTable.Contracts;

using AssetHolder.Contracts;
using Domain.Models;
using LineItem.Contracts;
using Waystone.Common.Application.Contracts.Mappings;

/// <summary>
/// Data transfer object for a FIRE table
/// </summary>
public class FireTableDto : IMapFrom<FireTable>
{
    /// <summary>
    /// The ID of the FIRE table
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The yearly inflation rate. Used to calculate various projections
    /// </summary>
    /// <example>0.025</example>
    public double YearlyInflationRate { get; set; }

    /// <summary>
    /// The monthly inflation rate. Used to calculate various projections.
    /// </summary>
    public double MonthlyInflationRate { get; set; }

    /// <summary>
    /// The yearly nominal return rate. Used to calculate various projections.
    /// </summary>
    /// <example>0.07</example>
    public double YearlyNominalReturnRate { get; set; }

    /// <summary>
    /// The monthly nominal return rate.
    /// </summary>
    public double MonthlyNominalReturnRate { get; set; }

    /// <summary>
    /// The assets value that you want to achieve when you reach retirement.
    /// </summary>
    public double RetirementTargetBeforeInflation { get; set; }

    /// <summary>
    /// The number of years until you want to retire.
    /// </summary>
    public int YearsToRetirement { get; set; }

    /// <summary>
    /// The number of months until retirement.
    /// </summary>
    public int MonthsToRetirement { get; set; }

    /// <summary>
    /// The table's line items.
    /// </summary>
    public IEnumerable<LineItemDto> LineItems { get; set; } = Array.Empty<LineItemDto>();

    /// <summary>
    /// The table's asset holders
    /// </summary>
    public IEnumerable<AssetHolderDto> AssetHolders { get; set; } = Array.Empty<AssetHolderDto>();
}
