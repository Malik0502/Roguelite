namespace Engine.Core.Manager.SceneM;

public class SceneManager
{
    private static Scene? CurrentScene;

    public static void LoadScene(Scene scene)
    {
        CurrentScene = scene;
    }

    public static Scene? GetCurrentCene()
    {
        return CurrentScene;
    }
}