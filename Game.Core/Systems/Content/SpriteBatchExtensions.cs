using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems.Content;

public static class SpriteBatchExtensions
{
    /// <summary>
    /// Draws a texture at the specified position with a uniform scale and no rotation or sprite effects.
    /// </summary>
    /// <remarks>This method draws the entire texture at the given position, using a white tint, no rotation,
    /// and no sprite effects. The origin is set to the top-left corner of the texture.</remarks>
    /// <param name="spriteBatch">The SpriteBatch instance used to render the texture. Cannot be null.</param>
    /// <param name="texture">The texture to draw. Cannot be null.</param>
    /// <param name="position">The position, in screen coordinates, where the texture will be drawn.</param>
    /// <param name="scale">The uniform scale factor to apply to the texture. Must be greater than zero.</param>
    public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float scale)
    {
        spriteBatch.Draw(
            texture,
            position,
            sourceRectangle: null,
            color: Color.White,
            rotation: 0f,
            origin: Vector2.Zero,
            scale,
            SpriteEffects.None,
            0f
        );
    }
}