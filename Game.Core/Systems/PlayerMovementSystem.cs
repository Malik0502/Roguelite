using System.Diagnostics;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Manager.ComponentM;
using Engine.Core.Manager.SceneM;
using Engine.Core.Manager.SystemM;
using Engine.Core.Constants;
using Microsoft.Xna.Framework.Input;

namespace Game.Core.Systems;

public class PlayerMovementSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private readonly SceneManager _sceneManager;
    private Scene _currentScene;
    private readonly float _velocity = 2.5f;

    public PlayerMovementSystem(ComponentManager componentManager, SceneManager sceneManager)
    {
        _componentManager = componentManager;
        _sceneManager = sceneManager;
    }

    // Doesnt work right now
    // It seems that player position resets every update
    // idk why
    public void Update(float deltaTime)
    {
        _currentScene = _sceneManager.GetCurrentCene();
        Entity player = _currentScene.GetEntity(EngineConstants.PlayerId);

        var playerTransform = _componentManager.GetPool<Transform>().Get(player.Id);

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            playerTransform.Position.Y += _velocity * deltaTime;

        if (Keyboard.GetState().IsKeyDown(Keys.A))
            playerTransform.Position.X -= _velocity * deltaTime;

        if (Keyboard.GetState().IsKeyDown(Keys.S))
            playerTransform.Position.Y -= _velocity * deltaTime;

        if (Keyboard.GetState().IsKeyDown(Keys.D))
            playerTransform.Position.X += _velocity * deltaTime;

        Debug.WriteLine(playerTransform.Position);
    }
}