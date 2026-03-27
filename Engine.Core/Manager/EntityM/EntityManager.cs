using Engine.Core.Config;
using Engine.Core.Enums;

namespace Engine.Core.Manager.EntityM;

public class EntityManager
{
    private readonly ConfigDeserializer _configDeserializer;
    private int _entityId;
    public EntityManager(ConfigDeserializer configDeserializer)
    {
        _configDeserializer = configDeserializer;
    }

    public Entity CreateEntity(EntityType entityType)
    {
        EntityConfig config = _configDeserializer.GetEntityConfig(entityType);
        var entity = new Entity
        {
            Id = _entityId
        };
        _entityId++;

        AddComponentsToEntity(config);

        return entity;
    }

    private void AddComponentsToEntity(EntityConfig config)
    {
        foreach (var component in config.Components)
        {
            Console.WriteLine(component.ToString());
        }
    }
}