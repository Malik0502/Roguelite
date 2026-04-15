using Engine.Core.Components.Base;

namespace Engine.Core.Manager.ComponentSystem;

public class ComponentManager
{
    public readonly Dictionary<Type, object> _pools = new();

    // Gets an existing pool or creates a pool from given type and returns it
    public ComponentPool<T> GetPool<T>() where T : struct, IComponent
    {
        if (_pools.TryGetValue(typeof(T), out var pool)) 
            return (ComponentPool<T>)pool;
        
        pool = new ComponentPool<T>();
        _pools[typeof(T)] = pool;

        return (ComponentPool<T>)pool;
    }

    public void RemovePool<T>(ComponentPool<T> componentPool) where T : struct, IComponent 
        => _pools.Remove(typeof(T));
}