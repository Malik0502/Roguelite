namespace Engine.Core.Manager.SceneM;

public class Scene
{
    private List<Core.Entity> _entities = [];

    public void AddEntity(Core.Entity entity)
    {
        _entities.Add(entity);
    }

    public List<Core.Entity> GetEntities()
    {
        return _entities;
    }
}