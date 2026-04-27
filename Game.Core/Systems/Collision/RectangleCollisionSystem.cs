using System.Linq;
using Engine.Core.Components.Collision;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;

namespace Game.Core.Systems.Collision;

public class RectangleCollisionSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private ComponentPool<RectangleCollider> _rectangleColliderPool;

    public RectangleCollisionSystem(ComponentManager componentManager)
    {
        _componentManager = componentManager;
    }
    
    public void Initialize()
    {
        _rectangleColliderPool = _componentManager.GetPool<RectangleCollider>();
    }

    public void Update(float deltaTime)
    {
        var entities = _rectangleColliderPool.GetIds().ToArray();
        
        foreach (var entity in entities)
        {
            ref var isColliding = ref _rectangleColliderPool.Get(entity).IsColliding;
            isColliding = false;
        }
        
        for (var i = 0; i < entities.Length; i++)
        {
            for (var j = i + 1; j < entities.Length; j++)
            {
                ref var a = ref _rectangleColliderPool.Get(entities[i]);
                ref var b = ref _rectangleColliderPool.Get(entities[j]);

                if (!a.Rectangle.Intersects(b.Rectangle)) continue;
                a.IsColliding = true;
                b.IsColliding = true;
            }
        }
        
        foreach (var entity in entities)
        {
            ref var collider = ref _rectangleColliderPool.Get(entity);
            collider.Color = collider.IsColliding ? Color.LimeGreen : Color.Red;
        }
    }
}