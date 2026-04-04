using Engine.Core.Config;
using Engine.Core.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Extensions.DependencyInjection;
using Engine.Core;
using Engine.Core.Manager.ComponentM;
using Engine.Core.Manager.EntityM;

namespace Game.Core
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _playerTexture;
        private Entity _player;
        private ServiceProvider _serviceProvider;
        private EntityFactory _entityFactory;

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

            _player = _entityFactory.Create(EntityType.Player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = _serviceProvider.GetService<SpriteBatch>();
            _playerTexture = Content.Load<Texture2D>("Player/BlackHead");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerTexture, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

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
            serviceCollection.AddSingleton <EntityFactory>();
            serviceCollection.AddSingleton<EntityManager>();
            serviceCollection.AddSingleton<ConfigDeserializer>();

            return serviceCollection;
        }
    }
}
