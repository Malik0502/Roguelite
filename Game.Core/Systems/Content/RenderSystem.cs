using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SceneSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems.Content;

public class RenderSystem : IDrawableSystem
{
    private readonly ComponentManager _componentManager;
    private readonly SpriteBatch _spriteBatch;
    private readonly SceneManager _sceneManager;
    private ComponentPool<Sprite> _spritePool;
    private ComponentPool<Transform> _transformPool;

    public RenderSystem(ComponentManager componentManager, SpriteBatch spriteBatch, SceneManager sceneManager)
    {
        _componentManager = componentManager;
        _spriteBatch = spriteBatch;
        _sceneManager = sceneManager;
    }

    public void Initialize()
    {
        _spritePool = _componentManager.GetPool<Sprite>();
        _transformPool = _componentManager.GetPool<Transform>();
    }

    public void Update(float deltaTime)
    {
    }

    public void Draw()
    {
        foreach (var entity in _sceneManager.GetCurrentScene().GetEntities())
        {
            DrawEntity(entity);
        }
    }

    private void DrawEntity(Entity entity)
    {
        _spriteBatch.Begin();
        _spriteBatch.Draw(_spritePool.Get(entity.Id).Texture, _transformPool.Get(entity.Id).Position, Color.White);
        _spriteBatch.End();
    }
}