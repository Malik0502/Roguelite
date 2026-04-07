namespace Engine.Core.Manager.SceneM;

public class SceneManager
{
    private static Scene? _currentScene;

    public SceneManager()
    {
        _currentScene = new Scene();
    }

    public void LoadScene(Scene scene)
    {
        _currentScene = scene;
    }

    public Scene GetCurrentScene()
    {
        return _currentScene;
    }
}