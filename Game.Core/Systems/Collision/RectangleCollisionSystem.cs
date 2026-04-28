using System.Collections.Generic;
using System.Linq;
using Engine.Core.Components.Collision;
using Engine.Core.Components.Tags;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SpatialGridSystem;
using Engine.Core.Manager.System;

namespace Game.Core.Systems.Collision;

public class RectangleCollisionSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private readonly SpatialGrid _spatialGrid;
    private ComponentPool<RectangleCollider> _rectangleColliderPool;
    private int _player;

    public RectangleCollisionSystem(ComponentManager componentManager, SpatialGrid spatialGrid)
    {
        _componentManager = componentManager;
        _spatialGrid = spatialGrid;
    }

    public void Initialize()
    {
        _rectangleColliderPool = _componentManager.GetPool<RectangleCollider>();
        _player = _componentManager.GetPool<PlayerTag>().GetIds().First();
    }

    // Reminder: implement Spatial Partition when game is not stable enough
    public void Update(float deltaTime)
    {
        ResetCollidingState();

        if (!_spatialGrid.TryGetCell(_player, out var cell))
            return;

        if (!_spatialGrid.TryGetEntities(cell, out var entities))
            return;

        CheckWithinCell(entities);
        foreach (var offset in SpatialGrid.NeighborOffsets)
        {
            var neighbourKey = Cell.Create(cell.X + offset.x, cell.Y + offset.y);

            if (_spatialGrid.TryGetEntities(neighbourKey, out var entitiesB))
                continue;

            CheckBetweenCells(_player, entitiesB);
        }
    }

    private void ResetCollidingState()
    {
        foreach (var entity in _rectangleColliderPool.GetIds())
        {
            _rectangleColliderPool.Get(entity).IsColliding = false;
        }
    }

    private void CheckWithinCell(List<int> entities)
    {
        for (var i = 0; i < entities.Count; i++)
        {
            var a = entities[i];

            for (var j = i + 1; j < entities.Count; j++)
            {
                TestCollision(a, entities[j]);
            }
        }
    }

    private void CheckBetweenCells(int player, List<int> entities)
    {
        if (entities == null)
            return;

        foreach (var t in entities)
        {
            TestCollision(player, t);
        }
    }

    private void TestCollision(int a, int b)
    {
        ref var colliderA = ref _rectangleColliderPool.Get(a);
        ref var colliderB = ref _rectangleColliderPool.Get(b);

        if (!colliderA.Rectangle.Intersects(colliderB.Rectangle)) return;
        colliderA.IsColliding = true;
        colliderB.IsColliding = true;
    }
}