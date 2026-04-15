using System.Runtime.InteropServices;
using Engine.Core.Components.Base;

namespace Engine.Core.Manager.ComponentSystem;

public class ComponentPool<T> where T : struct, IComponent
{
    private readonly Dictionary<int, T> _components = new();

    public void Add(int entityId, T component) 
        => _components[entityId] = component;

    // Gets a reference to a component, allowing its data to be modified
    public ref T Get(int entityId) 
        => ref CollectionsMarshal.GetValueRefOrNullRef(_components, entityId);

    public IEnumerable<int> GetIds() => 
        _components.Keys;

    public bool Has(int entityId) 
        => _components.ContainsKey(entityId);

    public void Remove(int entityId) 
        => _components.Remove(entityId);
}