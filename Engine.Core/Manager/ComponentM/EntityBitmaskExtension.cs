using Engine.Core.Enums;

namespace Engine.Core.Manager.ComponentM;

public static class EntityBitmaskExtension
{
    /// <summary>
    /// Adds the specified component type to the given entity.
    /// </summary>
    /// <param name="entity">The entity to which the component type will be added. Cannot be null.</param>
    /// <param name="component">The component type to add to the entity.</param>
    public static void Add(this Entity entity, ComponentType component)
    {
        entity.Mask |= component;
    }

    /// <summary>
    /// Determines whether the specified entity contains all of the required component types.
    /// </summary>
    /// <param name="entity">The entity to check for the presence of the required components.</param>
    /// <param name="required">A bitmask representing the component types that must be present on the entity.</param>
    /// <returns>true if the entity contains all of the required component types; otherwise, false.</returns>
    public static bool Has(this Entity entity, ComponentType required)
    {
        return (entity.Mask & required) == required;
    }

    /// <summary>
    /// Removes the specified component type from the given entity.
    /// </summary>
    /// <param name="entity">The entity from which the component type will be removed. Must not be null.</param>
    /// <param name="component">The component type to remove from the entity.</param>
    public static void Remove(this Entity entity, ComponentType component)
    {
        entity.Mask &= ~component;
    }
}