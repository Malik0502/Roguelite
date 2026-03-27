namespace Engine.Core.Manager.Scene;

public class Scene
{
    private List<Entity> _entities = [];

    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }

    public List<Entity> GetEntities()
    {
        return _entities;
    }
}