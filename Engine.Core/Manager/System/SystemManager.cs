using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Core.Manager.System;

public class SystemManager
{
    private readonly Dictionary<Type, ISystem> _systems = new();

    public void AddSystem<T>(T system) where T : class, ISystem
    {
        _systems[typeof(T)] = system;
    }

    public T GetSystem<T>() where T : class, ISystem
    {
        return (T)_systems[typeof(T)];
    }

    public void Initialize()
    {
        foreach (var system in _systems.Values)
        {
            system.Initialize();
        }
    }
    
    public void Update(GameTime gameTime)
    {
        foreach (var system in _systems.Values)
            system.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var drawableSystem in _systems.Values.OfType<IDrawableSystem>())
        {
            drawableSystem.Draw(spriteBatch);
        }
    }
}