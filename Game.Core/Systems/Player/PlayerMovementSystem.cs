using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Manager.ComponentM;
using Engine.Core.Manager.SceneM;
using Engine.Core.Manager.SystemM;
using Engine.Core.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Core.Systems.Player;

public class PlayerMovementSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private readonly SceneManager _sceneManager;
    private Scene _currentScene;



    private const float Velocity = 150f;

    public PlayerMovementSystem(ComponentManager componentManager, SceneManager sceneManager)
    {
        _componentManager = componentManager;
        _sceneManager = sceneManager;
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

        _currentScene = _sceneManager.GetCurrentScene();
        var player = _currentScene.GetEntity(EngineConstants.PlayerId);

        ref var playerTransform = ref _componentManager.GetPool<Transform>().Get(player.Id);

        playerTransform.Position += input * Velocity * deltaTime;

    }
}