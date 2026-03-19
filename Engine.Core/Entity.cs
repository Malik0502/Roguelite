using Engine.Core.Components;

namespace Engine.Core
{
    public class Entity
    {
        private readonly List<Component> _components = [];

        public Entity()
        {
            AddComponent<Transform>();
        }

        /// <summary>
        /// Adds a Component to this Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Component that was added with this method</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            var comp = new T();
            comp.Entity = this;
            _components.Add(comp);
            return comp;
        }

        /// <summary>
        /// Retrieves the first component of the specified type from the collection of components.
        /// </summary>
        /// <remarks>This method searches through the collection of components and returns the first
        /// instance that matches the specified type. If no matching component is found, it returns null.</remarks>
        /// <typeparam name="T">The type of the component to retrieve. This type must be a class or interface that the component implements.</typeparam>
        /// <returns>The first component of type T if found; otherwise, null.</returns>
        public T? GetComponent<T>()
        {
            return _components.OfType<T>().FirstOrDefault();
        }
    }
}
