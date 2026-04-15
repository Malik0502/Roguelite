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

    public PlayerMovementSystem(ComponentManager componentManager)
    {
        _componentManager = componentManager;
    }

    public void Update(float deltaTime)
    {
        var input = Vector2.Zero;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            input.Y -= 1;
        if(Keyboard.GetState().IsKeyDown(Keys.A))
            input.X -= 1;
        if (Keyboard.GetState().IsKeyDown(Keys.S)) 
            input.Y += 1;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            input.X += 1;

        if (input != Vector2.Zero)
            input.Normalize();

        var player = _componentManager.GetPool<PlayerTag>().GetIds().First();

        ref var playerTransform = ref _componentManager.GetPool<Transform>().Get(player);

        playerTransform.Position += input * Velocity * deltaTime;

    }
}