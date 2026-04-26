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
        foreach (var entity in _rectangleColliderPool.GetIds())
        {
            ref var collider = ref _rectangleColliderPool.Get(entity);

            foreach (var otherEntity in _rectangleColliderPool.GetIds())
            {
                if (otherEntity == entity) continue;
                ref var otherCollider = ref _rectangleColliderPool.Get(otherEntity);

                // does not work -> problem with switching between collision states
                if (!collider.Rectangle.Intersects(otherCollider.Rectangle) && !collider.IsColliding && !otherCollider.IsColliding)
                {
                    ChangeColor(ref collider, ref otherCollider, Color.Red);
                    collider.IsColliding = false;
                    otherCollider.IsColliding = false;
                }
                else
                {
                    ChangeColor(ref collider, ref otherCollider, Color.LimeGreen);
                    collider.IsColliding = true;
                    otherCollider.IsColliding = true;
                    
                }
            }
        }
    }

    private void ChangeColor(ref RectangleCollider collider, ref RectangleCollider otherCollider, Color color)
    {
        collider.Color = color;
        otherCollider.Color = color;
    }
}