using Engine.Core.Components.Base;

namespace Engine.Core.Components;

public struct Spawner : IComponent
{
    public int Radius;
    public int SpawnLimit;
    public int MaxSpawns;
    public int SpawnCount;
    public TimeSpan SpawnTimer;
}