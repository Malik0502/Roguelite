using System.Diagnostics;
using Engine.Core.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Extensions.DependencyInjection;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.ContentSystem;
using Engine.Core.Manager.EntitySystem;
using Engine.Core.Manager.SceneSystem;
using Engine.Core.Manager.SpatialGridSystem;
using Engine.Core.Manager.System;
using Game.Core.Systems.Collision;
using Game.Core.Systems.Content;
using Game.Core.Systems.Npc;
using Game.Core.Systems.Player;

namespace Game.Core
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private ServiceProvider _serviceProvider;
        private EntityFactory _entityFactory;
        private SystemManager _systemManager;

        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);

            // macbook testing
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;

            // home pc testing
            // _graphics.PreferredBackBufferHeight = 1080;
            // _graphics.PreferredBackBufferWidth = 1920;
            
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _serviceProvider = BuildServiceProvider();
            _systemManager = _serviceProvider.GetService<SystemManager>();
            
            RegisterSystems();
            
            _entityFactory = _serviceProvider.GetService<EntityFactory>();

            CreatePlayer();
            
            _systemManager.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _systemManager.Update(deltaTime);

            Debug.WriteLine($"FPS: {GetFramerate(gameTime)}");

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _systemManager.Draw(_serviceProvider.GetService<SpriteBatch>());
            
            base.Draw(gameTime);
        }

        private void CreatePlayer()
        {
            _entityFactory.Create();
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
            serviceCollection.AddSingleton<RenderSystem>();
            serviceCollection.AddSingleton<NpcMovementSystem>();
            serviceCollection.AddSingleton<SpatialGrid>();
            serviceCollection.AddSingleton<RectangleColliderRenderSystem>();
            serviceCollection.AddSingleton<RectangleCollisionSystem>();

            return serviceCollection;
        }
        
        private void RegisterSystems()
        {
            _systemManager.AddSystem(_serviceProvider.GetService<PlayerMovementSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<EnemySpawnSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<RenderSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<NpcMovementSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<RectangleColliderRenderSystem>());
            _systemManager.AddSystem(_serviceProvider.GetService<RectangleCollisionSystem>());
        }

        #endregion
    }
}
