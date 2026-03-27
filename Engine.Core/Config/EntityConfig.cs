using Engine.Core.Components.Base;

namespace Engine.Core.Config;

public class EntityConfig
{
    public int Health;
    public int Damage;
    public float Speed;
    public Component[] Components = [];
}