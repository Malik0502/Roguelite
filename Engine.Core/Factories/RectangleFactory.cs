using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Core.Factories;

public static class RectangleFactory
{
    public static Rectangle ConstructColliderRect(Vector2 spawnPos, float entityScale, Texture2D texture)
    {
        return new Rectangle
        {
            Location = spawnPos.ToPoint(),
            Size = new Point((int)(texture.Width * entityScale), (int)(texture.Height  * entityScale))
        };
    }
}