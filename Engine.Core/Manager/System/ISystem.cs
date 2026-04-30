using Microsoft.Xna.Framework;

namespace Engine.Core.Manager.System;

public interface ISystem
{
    void Initialize();
    
    void Update(GameTime gameTime);
}