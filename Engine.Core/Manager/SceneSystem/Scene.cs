namespace Engine.Core.Manager.SceneSystem;

public class Scene
{
    // For Sake of simplicity no Dictionary for faster accessing specific entities
    // Add a Dictionary, should the performance drop significantly
    private List<Core.Entity> _entities = [];

    public void AddEntity(Core.Entity entity) 
        => _entities.Add(entity);

    public Entity GetEntity(int entityId) 
        => _entities.First(x => x.Id == entityId);

    public List<Entity> GetEntities()
        => _entities;
}