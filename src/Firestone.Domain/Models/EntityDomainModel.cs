namespace Firestone.Domain.Models;

using Data;

public abstract class EntityDomainModel<T> where T : EntityBase
{
    protected EntityDomainModel()
    { }

    protected EntityDomainModel(Guid id)
    {
        Id = id;
    }

    public Guid? Id { get; }

    public T ToEntity()
    {
        T entity = CreateEntity();

        SetIdIfNotNull(entity);

        return entity;
    }

    protected abstract T CreateEntity();

    private void SetIdIfNotNull(T entity)
    {
        if (Id.HasValue)
        {
            entity.Id = Id.Value;
        }
    }
}
