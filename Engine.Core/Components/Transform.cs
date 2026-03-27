using System.Numerics;
using Engine.Core.Components.Base;

namespace Engine.Core.Components;

public class Transform : Component
{
    public Vector2 Position;
    public float Rotation;
    public Vector2 Scale;
}