namespace Engine.Core.Manager.SceneM;

public class SceneManager
{
    public static SceneM.Scene? CurrentScene { get; private set; }

    public static void LoadScene(SceneM.Scene scene)
    {
        CurrentScene = scene;
    }
}