using Engine.Core.Components.Base;
using Microsoft.Xna.Framework;

namespace Engine.Core.Components;

public struct Transform : IComponent
{
    public Vector2 Position;
    public float Rotation;
    public Vector2 Scale;
}