namespace Firestone.Application.Assets.Commands;

using Contracts;
using Domain.Models;
using Services;

/// <summary>
/// A command for updating existing assets.
/// </summary>
public class UpdateAssetsCommand : IRequest<AssetsDto>
{
    /// <summary>
    /// The ID of the assets entry.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The updated assets data.
    /// </summary>
    public UpdateAssetsDto Data { get; init; } = default!;

    /// <summary>
    /// The validator for <see cref="UpdateAssetsCommand" />
    /// </summary>
    public class Validator : AbstractValidator<UpdateAssetsCommand>
    {
        /// <summary>
        /// Constructs the validator.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Data).SetValidator(new UpdateAssetsDto.Validator());
        }
    }

    /// <summary>
    /// The handler for <see cref="UpdateAssetsCommand" />
    /// </summary>
    public class Handler : IRequestHandler<UpdateAssetsCommand, AssetsDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructs the handler.
        /// </summary>
        /// <param name="assetsRepository">The <see cref="IAssetsRepository" /></param>
        /// <param name="mapper">The <see cref="IMapper" /></param>
        public Handler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<AssetsDto> Handle(UpdateAssetsCommand request, CancellationToken cancellationToken)
        {
            Assets assets = await _assetsRepository.UpdateAsync(request.Id, request.Data.Amount, cancellationToken);

            return _mapper.Map<AssetsDto>(assets);
        }
    }
}
