using Engine.Core.Constants;

namespace Engine.Core.Components.Base;

public class ComponentArray<T> : IComponentArray where T : IComponent 
{
    private T[] GameComponents = new T[EngineConstants.MaxEntities];

    // Map from an entity ID to an array index.
    private Dictionary<Entity, int> EntityToIndexMap;

    // Map from an array index to an entity ID.
    private Dictionary<int, Entity> IndexToEntityMap;

    private int ValidArrayEntries;
    
    public void InsertData(Entity entity, T component)
    {
        if (EntityToIndexMap.TryGetValue(entity, out var index))
        {
            throw new InvalidOperationException($"Entity already exists in map with index: {index}");
        }
        // Put new entry at end and update the maps
        EntityToIndexMap[entity] = ValidArrayEntries;
        IndexToEntityMap[ValidArrayEntries] = entity;
        GameComponents[ValidArrayEntries] = component;
        ++ValidArrayEntries;
    }

    public void RemoveData(Entity entity)
    {
        if(!EntityToIndexMap.TryGetValue(entity, out int index))
        {
            throw new InvalidOperationException("Entity does not exists in map");
        }

        // Copy element at end into deleted element's place to maintain density
        var indexOfRemovedEntity = index;
        var indexOfLastElement = ValidArrayEntries - 1;
        GameComponents[indexOfRemovedEntity] = GameComponents[indexOfLastElement];

        // Update map to point to moved spot
        Entity entityOfLastElement = IndexToEntityMap[indexOfLastElement];
        EntityToIndexMap[entityOfLastElement] = indexOfRemovedEntity;
        IndexToEntityMap[indexOfRemovedEntity] = entityOfLastElement;

        EntityToIndexMap.Remove(entity);
        IndexToEntityMap.Remove(indexOfLastElement);

        --ValidArrayEntries;
    }

    public T GetData(Entity entity)
    {
        return !EntityToIndexMap.TryGetValue(entity, out int index) 
            ? throw new InvalidOperationException("Entity does not exists in map") 
            : GameComponents[EntityToIndexMap[entity]];
    }

    public void EntityDestroyed(Entity entity)
    {
        if (!EntityToIndexMap.TryGetValue(entity, out int index))
        {
            throw new InvalidOperationException("Entity does not exists in map");
        }
        RemoveData(entity);
    }
}