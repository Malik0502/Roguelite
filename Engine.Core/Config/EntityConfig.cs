namespace Engine.Core.Config;

public class EntityConfig
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public float Speed { get; set; }
    public string[] Components { get; set; } = [];
}