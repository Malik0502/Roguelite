using System.Diagnostics;
using Engine.Core.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Extensions.DependencyInjection;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Enums;
using Engine.Core.Manager.ComponentM;
using Engine.Core.Manager.EntityM;
using Engine.Core.Manager.SceneM;
using Engine.Core.Manager.SystemM;
using Game.Core.Systems.Player;

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
        private ComponentPool<PlayerTag> _playerPool;

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
            _entityFactory = _serviceProvider.GetService<EntityFactory>();
            _componentManager = _serviceProvider.GetService<ComponentManager>();
            _systemManager = _serviceProvider.GetService<SystemManager>();

            RegisterComponentPool();
            RegisterSystems();

            CreatePlayer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = _serviceProvider.GetService<SpriteBatch>();
            _spritePool.Get(_player.Id).Texture = Content.Load<Texture2D>("Player/BlackHead");
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _systemManager.UpdateAll(deltaTime);

            Debug.WriteLine(_transformPool.Get(_player.Id).Position);

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

            base.Draw(gameTime);
        }

        private void CreatePlayer()
        {
            _player = _entityFactory.Create(EntityType.Player);

            _transformPool.Add(_player.Id, new Transform() { Position = new Vector2(100, 100) });
            _spritePool.Add(_player.Id, new Sprite());
            _playerPool.Add(_player.Id, new PlayerTag());
        }

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
            serviceCollection.AddSingleton<SpriteBatch>();
            serviceCollection.AddSingleton<EntityFactory>();
            serviceCollection.AddSingleton<EntityManager>();
            serviceCollection.AddSingleton<ConfigDeserializer>();
            serviceCollection.AddSingleton<SceneManager>();
            serviceCollection.AddSingleton<ComponentManager>();
            serviceCollection.AddSingleton<SystemManager>();
            serviceCollection.AddSingleton<PlayerMovementSystem>();

            return serviceCollection;
        }

        private void RegisterComponentPool()
        {
            _spritePool = _componentManager.GetPool<Sprite>();
            _transformPool = _componentManager.GetPool<Transform>();
            _playerPool = _componentManager.GetPool<PlayerTag>();
        }

        private void RegisterSystems()
        {
            _systemManager.AddSystem(_serviceProvider.GetService<PlayerMovementSystem>());
        }

        #endregion
    }
}
