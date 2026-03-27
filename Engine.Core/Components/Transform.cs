using System.Numerics;
using Engine.Core.Components.Base;
using ComponentType = System.UInt32;

namespace Engine.Core.Components;

public struct Transform : IComponent
{
    public ComponentType ComponentType;
    public const UInt32 MaxComponents = 128;
    
    public Vector2 Position;
    public float Rotation;
    public Vector2 Scale;
}