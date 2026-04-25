using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Enums;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.ContentSystem;
using Engine.Core.Manager.SceneSystem;
using Engine.Core.Manager.SpatialGridSystem;
using Microsoft.Xna.Framework;

namespace Engine.Core.Manager.EntitySystem;

public class EntityManager
{
    private readonly SceneManager _sceneManager;
    private readonly ComponentManager _componentManager;
    private readonly SpriteRenderer _content;
    private readonly SpatialGrid _spatialGrid;
    private int _entityId;

    #region ComponentPools

    private ComponentPool<Sprite> _spritePool = null!;
    private ComponentPool<Transform> _transformPool = null!;
    private ComponentPool<MeleeTag> _meleePool = null!;
    private ComponentPool<PlayerTag> _playerPool = null!;
    private ComponentPool<Spawner> _spawnerPool = null!;
    private ComponentPool<Health> _healthPool = null!;
    private ComponentPool<RangeTag> _rangePool = null!;
    private ComponentPool<MageTag> _magePool = null!;
    private ComponentPool<EnemyTag> _enemyPool = null!;

    #endregion
    
    public EntityManager(SceneManager sceneManager, ComponentManager componentManager, SpriteRenderer content, SpatialGrid spatialGrid)
    {
        _sceneManager = sceneManager;
        _componentManager = componentManager;
        _content = content;
        _spatialGrid = spatialGrid;
        LoadPool();
    }

    public Entity CreatePlayer()
    {
        var spriteScale = 1f;
        var entity = CreateLivingEntity(new Vector2(200, 200), EntityType.Player, spriteScale);
        
        _spawnerPool.Add(entity.Id, new Spawner 
            { Radius = 150, SpawnLimit = 20, SpawnTimer = TimeSpan.FromSeconds(2), MaxSpawns = 200});

        return entity;
    }

    public Entity CreateEnemy(EntityType entityType, Vector2 spawnPos)
    {
        var spriteScale = 1.5f;
        var entity = CreateLivingEntity(spawnPos, entityType, spriteScale);
        
        switch (entityType)
        {
            case EntityType.Melee:
                _spritePool.Add(entity.Id, 
                    new Sprite{Texture = _content.GetTexture("Enemies/AngryRedSlime")});
                _meleePool.Add(entity.Id, new MeleeTag());
                break;
            case EntityType.Range:
                _spritePool.Add(entity.Id, new Sprite{Texture = _content.GetTexture("Enemies/AngryGreenSlime")});
                _rangePool.Add(entity.Id, new RangeTag());
                break;
            case EntityType.Mage:
                _spritePool.Add(entity.Id, new Sprite{Texture = _content.GetTexture("Enemies/AngryPurpleSlime")});
                _magePool.Add(entity.Id, new MageTag());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }
        
        return entity;
    }

    #region private methods
    
    private Entity CreateLivingEntity(Vector2 spawnPos, EntityType entityType, float scale)
    {
        var entity = new Entity
        {
            Id = _entityId
        };
        _entityId++;
        
        _healthPool.Add(entity.Id, new Health());
        _transformPool.Add(entity.Id, new Transform
            {Position = spawnPos, Scale = scale});

        _spatialGrid.SetEntity(entity.Id, Cell.Create(spawnPos.X, spawnPos.Y));

        if (entityType != EntityType.Player)
        {
            _enemyPool.Add(entity.Id, new EnemyTag());
        }
        else
        {
            _playerPool.Add(entity.Id, new PlayerTag());
            _spritePool.Add(entity.Id, new Sprite{Texture = _content.GetTexture("Player/BlackHead")});
        }
        
        _sceneManager.GetCurrentScene().AddEntity(entity);
        return entity;
    }

    private void LoadPool()
    {
        _healthPool = _componentManager.GetPool<Health>();
        _transformPool = _componentManager.GetPool<Transform>();
        _spritePool = _componentManager.GetPool<Sprite>();
        _playerPool = _componentManager.GetPool<PlayerTag>();
        _meleePool = _componentManager.GetPool<MeleeTag>();
        _rangePool =  _componentManager.GetPool<RangeTag>();
        _magePool =  _componentManager.GetPool<MageTag>();
        _spawnerPool = _componentManager.GetPool<Spawner>();
        _enemyPool = _componentManager.GetPool<EnemyTag>();
    }
    
    #endregion
}