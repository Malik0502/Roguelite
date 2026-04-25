using System;
using System.Linq;
using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SpatialGridSystem;
using Engine.Core.Manager.System;

namespace Game.Core.Systems.Npc;

public class NpcMovementSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private readonly SpatialGrid _spatialGrid;
    private  ComponentPool<Transform> _transformPool;
    private ComponentPool<EnemyTag> _enemyPool;
    private int _playerId;
    private const float Velocity = 100f;

    public NpcMovementSystem(ComponentManager componentManager, SpatialGrid spatialGrid)
    {
        _componentManager = componentManager;
        _spatialGrid = spatialGrid;
    }


    public void Initialize()
    {
        _transformPool = _componentManager.GetPool<Transform>();
        _enemyPool = _componentManager.GetPool<EnemyTag>();
        
        _playerId = _componentManager.GetPool<PlayerTag>().GetIds().First();
    }

    public void Update(float deltaTime)
    {
        var playerPosition = _transformPool.Get(_playerId).Position;

        foreach (var enemyId in _enemyPool.GetIds())
        {
            var randomRadiantDeviation = new Random().NextDouble();
            ref var position = ref _transformPool.Get(enemyId).Position;
            var directionVector = (playerPosition - position);
            directionVector.Rotate((float)randomRadiantDeviation);
            directionVector.Normalize();

            position += directionVector * Velocity * deltaTime;

            _spatialGrid.SetEntity(enemyId, Cell.Create(position.X, position.Y));
        }
    }
}