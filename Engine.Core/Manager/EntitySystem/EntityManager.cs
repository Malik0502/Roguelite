using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Enums;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SceneSystem;
using Microsoft.Xna.Framework;

namespace Engine.Core.Manager.EntitySystem;

public class EntityManager
{
    private readonly SceneManager _sceneManager;
    private readonly ComponentManager _componentManager;
    private int _entityId;
    public EntityManager(SceneManager sceneManager, ComponentManager componentManager)
    {
        _sceneManager = sceneManager;
        _componentManager = componentManager;
    }

    public Entity CreatePlayer(EntityType entityType)
    {
        
        var entity = new Entity
        {
            Id = _entityId
        };
        _entityId++;
        
        _sceneManager.GetCurrentScene().AddEntity(entity);

        _componentManager.GetPool<Health>().Add(entity.Id, new Health());
        _componentManager.GetPool<Transform>().Add(entity.Id, new Transform(){Position = new Vector2(100, 100)});
        _componentManager.GetPool<Sprite>().Add(entity.Id, new Sprite());
        _componentManager.GetPool<PlayerTag>().Add(entity.Id, new PlayerTag());

        return entity;
    }

    public Entity CreateMelee(EntityType entityType)
    {

        var entity = new Entity
        {
            Id = _entityId
        };
        _entityId++;

        _sceneManager.GetCurrentScene().AddEntity(entity);

        _componentManager.GetPool<Health>().Add(entity.Id, new Health());
        _componentManager.GetPool<Transform>().Add(entity.Id, new Transform());
        _componentManager.GetPool<Sprite>().Add(entity.Id, new Sprite());
        _componentManager.GetPool<MeleeTag>().Add(entity.Id, new MeleeTag());

        return entity;
    }
}