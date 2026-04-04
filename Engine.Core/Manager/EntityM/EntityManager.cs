using Engine.Core.Config;
using Engine.Core.Enums;
using Engine.Core.Manager.SceneM;

namespace Engine.Core.Manager.EntityM;

public class EntityManager
{
    private readonly ConfigDeserializer _configDeserializer;
    private readonly SceneManager _sceneManager;
    private int _entityId;
    public EntityManager(ConfigDeserializer configDeserializer, SceneManager sceneManager)
    {
        _configDeserializer = configDeserializer;
        _sceneManager = sceneManager;
    }

    public Entity CreateEntity(EntityType entityType)
    {
        EntityConfig config = _configDeserializer.GetEntityConfig(entityType);
        var entity = new Entity
        {
            Id = _entityId
        };
        _entityId++;
        
        _sceneManager.GetCurrentCene().AddEntity(entity);

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