using System.Linq;
using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.ContentSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace Game.Core.Systems.Content.Tilemap;

public class TilemapRenderSystem(
    GraphicsDevice graphicsDevice,
    GameWindow window,
    SpriteRenderer content,
    ComponentManager componentManager)
    : IDrawableSystem
{
    private TiledMap _tilemap;
    private TiledMapRenderer _renderer;
    private OrthographicCamera _camera;

    public Matrix CameraMatrix;

    private ComponentPool<Transform> _transformPool;
    private int _player;

    public void Initialize()
    {
        var viewportAdapter = new BoxingViewportAdapter(window, graphicsDevice, 800, 400);
        _camera = new OrthographicCamera(viewportAdapter);

        CameraMatrix = _camera.GetViewMatrix();

        _tilemap = content.GetTiledMap("Maps/Level1");

        _renderer = new TiledMapRenderer(graphicsDevice);
        _renderer.LoadMap(_tilemap);

        _transformPool = componentManager.GetPool<Transform>();
        _player = componentManager.GetPool<PlayerTag>().GetIds().First();
    }

    public void Update(GameTime gameTime)
    {
        var playerPos = _transformPool.Get(_player).Position;
        _renderer.Update(gameTime);
        _camera.LookAt(playerPos);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _renderer.Draw(_camera.GetViewMatrix());
    }
}