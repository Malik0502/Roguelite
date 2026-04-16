namespace Engine.Core.Manager.System;

public interface ISystem
{
    void Initialize();
    
    void Update(float deltaTime);
}