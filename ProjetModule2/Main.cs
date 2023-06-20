using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;

namespace BrickBreaker
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScenesService _scenesService;
        private AssetManager _assetManager;
        private GameManager _gameManager;
        private UIManager _UIManager;
        private CollisionManager _collisionManager;

        public static Point _screenSize = new Point(1240, 720);

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _screenSize.X;
            _graphics.PreferredBackBufferHeight = _screenSize.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.Register<SpriteBatch>(_spriteBatch);

            _scenesService = new ScenesService();
            ServiceLocator.Register<IScenesService>(_scenesService);

            _assetManager = new AssetManager();
            ServiceLocator.Register<IAssetsManager>(_assetManager);

            _gameManager = new GameManager();
            ServiceLocator.Register<GameManager>(_gameManager);

            _UIManager = new UIManager(_gameManager);
            ServiceLocator.Register<UIManager>(_UIManager);

            _collisionManager = new CollisionManager();
            ServiceLocator.Register<CollisionManager>(_collisionManager);

            _assetManager.LoadAsset<SpriteFont>("PixelFont", Content);
            _assetManager.LoadAsset<Texture2D>("balle", Content);
            _assetManager.LoadAsset<Texture2D>("raquette", Content);
            _assetManager.LoadAsset<Texture2D>("brique_0_0", Content);
            _assetManager.LoadAsset<Texture2D>("brique_5_3", Content);
            _assetManager.LoadAsset<Texture2D>("brique_7_3", Content);
            _assetManager.LoadAsset<Texture2D>("brique_8_3", Content);
            _assetManager.LoadAsset<Texture2D>("bonus_vie", Content);

            _scenesService.Register<SceneMenu>();
            _scenesService.Register<SceneGame>();
            _scenesService.Register<SceneGameOver>();


            _scenesService.Load<SceneMenu>();

        }

        protected override void Update(GameTime gameTime)
        {
            _scenesService.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _scenesService.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}