using System.Collections.Generic;
using Engine.Core.Components.Collision;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SpatialGridSystem;
using Engine.Core.Manager.System;

namespace Game.Core.Systems.Collision;

public class RectangleCollisionSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private readonly SpatialGrid _spatialGrid;
    private ComponentPool<RectangleCollider> _rectangleColliderPool;

    public RectangleCollisionSystem(ComponentManager componentManager, SpatialGrid spatialGrid)
    {
        _componentManager = componentManager;
        _spatialGrid = spatialGrid;
    }

    public void Initialize()
    {
        _rectangleColliderPool = _componentManager.GetPool<RectangleCollider>();
    }

    // Reminder: implement Spatial Partition when game is not stable in the future
    public void Update(float deltaTime)
    {
        ResetCollidingState();

        var cells = _spatialGrid.GetCells();

        foreach (var (cellPos, entitiesA) in cells)
        {
            CheckWithinCell(entitiesA);

            foreach (var offset in SpatialGrid.NeighborOffsets)
            {
                var neighborKey = Cell.Create(
                    cellPos.X + offset.x,
                    cellPos.Y + offset.y
                );

                if (!cells.TryGetValue(neighborKey, out var entitiesB))
                    continue;

                CheckBetweenCells(entitiesA, entitiesB);
            }
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

    private void CheckBetweenCells(List<int> a, List<int> b)
    {
        foreach (var idA in a)
        {
            foreach (var t in b)
            {
                TestCollision(idA, t);
            }
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