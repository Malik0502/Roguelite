using Microsoft.Xna.Framework;

namespace Engine.Core.Extensions;

public static class GameTimeExtensions
{
    public static float DeltaTime(this GameTime gameTime)
    {
        return (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}