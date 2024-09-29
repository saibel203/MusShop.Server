using MapsterMapper;
using MusShop.Contracts.RepositoryAbstractions.Base;

namespace MusShop.Application.UseCases.Commons;

public abstract class BaseFeatureConfigs
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected BaseFeatureConfigs(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
}