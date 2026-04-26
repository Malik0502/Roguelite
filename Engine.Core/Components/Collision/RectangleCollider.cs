using Engine.Core.Components.Base;
using Microsoft.Xna.Framework;

namespace Engine.Core.Components.Collision;

public struct RectangleCollider : IComponent
{
    public Rectangle Rectangle;
    public Color Color;
    public bool IsColliding;
}