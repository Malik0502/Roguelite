using System.Numerics;

namespace Engine.Core.Components;

public class Transform : Component
{
    public Vector2 Position;
    public float Rotation;
    public Vector2 Scale = Vector2.One;
}