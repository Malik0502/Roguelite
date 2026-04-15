using Engine.Core.Enums;

namespace Engine.Core.Manager.EntitySystem;

public class EntityFactory
{
    private readonly EntityManager _entityManager;
    public EntityFactory(EntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Entity Create(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.Player:
                return _entityManager.CreatePlayer(entityType);
            case EntityType.Melee:
                break;
            case EntityType.Range:
                break;
            case EntityType.Mage:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }

        throw new ArgumentNullException(nameof(entityType), "An error occured while creating an entity");
    }
}