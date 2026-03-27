using Engine.Core.Enums;

namespace Engine.Core.Manager.EntityM;

public class EntityFactory
{
    private readonly EntityManager _entityManager;
    public EntityFactory(EntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Entity Create(EntityType entityType)
    {
        return _entityManager.CreateEntity(entityType);
    }
}