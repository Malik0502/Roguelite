using System;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems;

public class EnemyRenderSystem : IDrawableSystem
{
    private readonly EnemySpawnSystem _enemySystem;
    private readonly ComponentManager _componentManager;
    private readonly SpriteBatch _spriteBatch;
    private ComponentPool<Sprite> _spritePool;
    private ComponentPool<Transform> _transformPool;

    public EnemyRenderSystem(EnemySpawnSystem enemySystem, ComponentManager componentManager, SpriteBatch spriteBatch)
    {
        _enemySystem = enemySystem;
        _componentManager = componentManager;
        _spriteBatch = spriteBatch;
    }

    public void Initialize()
    {
        _spritePool = _componentManager.GetPool<Sprite>();
        _transformPool = _componentManager.GetPool<Transform>();
    }

    public void Update(float deltaTime)
    {
        return;
    }

    public void Draw()
    {
        foreach (var enemy in _enemySystem.Enemies)
        {
            DrawEnemy(enemy);
        }
    }

    private void DrawEnemy(Entity enemy)
    {
        _spriteBatch.Begin();
        _spriteBatch.Draw(_spritePool.Get(enemy.Id).Texture, _transformPool.Get(enemy.Id).Position, Color.White);
        _spriteBatch.End();
    }
}