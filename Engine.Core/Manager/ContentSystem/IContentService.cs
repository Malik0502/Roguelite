namespace Engine.Core.Manager.ContentSystem;

public interface IContentService
{
    T Load<T>(string assetName);
}