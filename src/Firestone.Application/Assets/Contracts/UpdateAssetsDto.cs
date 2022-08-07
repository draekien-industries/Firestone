namespace Firestone.Application.Assets.Contracts;

/// <summary>
/// Data transfer object for updating assets.
/// </summary>
public class UpdateAssetsDto
{
    /// <summary>
    /// The new total assets amount.
    /// </summary>
    public double Amount { get; init; }

    /// <summary>
    /// The validator for <see cref="UpdateAssetsDto" />
    /// </summary>
    public class Validator : AbstractValidator<UpdateAssetsDto>
    {
        /// <summary>
        /// Constructor for <see cref="Validator" />.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
