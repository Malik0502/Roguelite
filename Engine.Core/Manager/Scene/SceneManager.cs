namespace Engine.Core.Manager.Scene;

public class SceneManager
{
    public static Scene? CurrentScene { get; private set; }

    public static void LoadScene(Scene scene)
    {
        CurrentScene = scene;
    }
}