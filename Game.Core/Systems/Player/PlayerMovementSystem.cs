using System.Diagnostics;
using System.Linq;
using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Core.Systems.Player;

public class PlayerMovementSystem : ISystem
{
    private readonly ComponentManager _componentManager;

    private const float Velocity = 150f;

    private int _player;
    private ComponentPool<Transform> _transformPool;

    public PlayerMovementSystem(ComponentManager componentManager)
    {
        _componentManager = componentManager;
    }

    public void Initialize()
    {
        _player = _componentManager.GetPool<PlayerTag>().GetIds().First();
        _transformPool = _componentManager.GetPool<Transform>();
    }

    public void Update(float deltaTime)
    {
        var input = Vector2.Zero;

        var keyboard = Keyboard.GetState();
        
        if (keyboard.IsKeyDown(Keys.W))
            input.Y -= 1;
        if(keyboard.IsKeyDown(Keys.A))
            input.X -= 1;
        if (keyboard.IsKeyDown(Keys.S)) 
            input.Y += 1;
        if (keyboard.IsKeyDown(Keys.D))
            input.X += 1;

        if (input != Vector2.Zero)
            input.Normalize();

        ref var transform = ref _transformPool.Get(_player);
        
        transform.Position += input * Velocity * deltaTime;
    }
}