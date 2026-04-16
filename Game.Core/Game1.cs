using System.Diagnostics;
using Engine.Core.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Extensions.DependencyInjection;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.ContentSystem;
using Engine.Core.Manager.EntitySystem;
using Engine.Core.Manager.SceneSystem;
using Engine.Core.Manager.System;
using Game.Core.Systems;
using Game.Core.Systems.Content;
using Game.Core.Systems.Player;
using Microsoft.Xna.Framework.Content;

namespace Game.Core
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ServiceProvider _serviceProvider;
        private EntityFactory _entityFactory;
        private ComponentManager _componentManager;
        private SystemManager _systemManager;

        private Entity _player;

        #region Pools

        private ComponentPool<Sprite> _spritePool;
        private ComponentPool<Transform> _transformPool;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _serviceProvider = BuildServiceProvider();
            _componentManager = _serviceProvider.GetService<ComponentManager>();
            _systemManager = _serviceProvider.GetService<SystemManager>();
            
            RegisterComponentPool();
            RegisterSystems();
            
            _entityFactory = _serviceProvider.GetService<EntityFactory>();

            CreatePlayer();
            
            _systemManager.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = _serviceProvider.GetService<SpriteBatch>();
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _systemManager.Update(deltaTime);

            Debug.WriteLine($"FPS: {GetFramerate(gameTime)}");

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_spritePool.Get(_player.Id).Texture, _transformPool.Get(_player.Id).Position, Color.White);
            _spriteBatch.End();

            _systemManager.Draw();
            
            base.Draw(gameTime);
        }

        private void CreatePlayer()
        {
            _player = _entityFactory.Create();
        }

        private double GetFramerate(GameTime gameTime) 
            => 1 / gameTime.ElapsedGameTime.TotalSeconds;

        #region Registry

        private ServiceProvider BuildServiceProvider()
        {
            var serviceCollection = RegisterServices();

            return serviceCollection.BuildServiceProvider();
        }

        private ServiceCollection RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton(GraphicsDevice);
            serviceCollection.AddSingleton<GraphicsDeviceManager>();
            serviceCollection.AddSingleton<SpriteRenderer>();
            serviceCollection.AddSingleton(Content);
            serviceCollection.AddSingleton<IContentService, MonoGameContentSystem>();
            serviceCollection.AddSingleton<SpriteBatch>();
            serviceCollection.AddSingleton<EntityFactory>();
            serviceCollection.AddSingleton<MonoGameContentSystem>();
            serviceCollection.AddSingleton<EntityManager>();
            serviceCollection.AddSingleton<ConfigDeserializer>();
            serviceCollection.AddSingleton<SceneManager>();
            serviceCollection.AddSingleton<ComponentManager>();
            serviceCollection.AddSingleton<SystemManager>();
            serviceCollection.AddSingleton<PlayerMovementSystem>();
            serviceCollection.AddSingleton<EnemySpawnSystem>();
            serviceCollection.AddSingleton<EnemyRenderSystem>();
            

            return serviceCollection;
        }

        private void RegisterComponentPool()
        {
            _spritePool = _componentManager.GetPool<Sprite>();
            _transformPool = _componentManager.GetPool<Transform>();
        }

        private void RegisterSystems()
        {
            _systemManager.AddSystem(_serviceProvider.GetService<PlayerMovementSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<EnemySpawnSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<EnemyRenderSystem>());
        }

        #endregion
    }
}
