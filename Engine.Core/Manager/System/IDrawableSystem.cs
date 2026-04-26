using Microsoft.Xna.Framework.Graphics;

namespace Engine.Core.Manager.System;

public interface IDrawableSystem : ISystem
{
    void Draw(SpriteBatch spriteBatch);
}