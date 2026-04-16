using Engine.Core.Enums;

namespace Engine.Core.Manager.EntitySystem;

public class EntityFactory
{
    private readonly EntityManager _entityManager;
    public EntityFactory(EntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Entity Create()
    {
        return _entityManager.CreatePlayer();
    }
}