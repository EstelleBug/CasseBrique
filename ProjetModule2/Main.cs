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
        private LevelsLoader _levelsLoader;

        public static Point screenSize = new Point(1240, 720);

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
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

            _levelsLoader = new LevelsLoader();
            ServiceLocator.Register<LevelsLoader>(_levelsLoader);
            _levelsLoader.Load();

            _levelsLoader.SetCurrentLevel("level_1");

            _assetManager.LoadAsset<SpriteFont>("PixelFont", Content);
            _assetManager.LoadAsset<Texture2D>("balle", Content);
            _assetManager.LoadAsset<Texture2D>("raquette", Content);
            _assetManager.LoadAsset<Texture2D>("brique_0_0", Content);
            _assetManager.LoadAsset<Texture2D>("brique_5_3", Content);
            _assetManager.LoadAsset<Texture2D>("brique_7_3", Content);
            _assetManager.LoadAsset<Texture2D>("brique_8_3", Content);
            _assetManager.LoadAsset<Texture2D>("bonus_vie2", Content);
            _assetManager.LoadAsset<Texture2D>("bonus_disco", Content);
            _assetManager.LoadAsset<Texture2D>("malus", Content);
            _assetManager.LoadAsset<Texture2D>("PlayButton", Content);
            _assetManager.LoadAsset<Texture2D>("ExitButton", Content);
            _assetManager.LoadAsset<Texture2D>("MenuButton", Content);
            _assetManager.LoadAsset<Texture2D>("OptionsButton", Content);
            _assetManager.LoadAsset<Texture2D>("CancelButton", Content);
            _assetManager.LoadAsset<Texture2D>("SaveButton", Content);
            _assetManager.LoadAsset<Texture2D>("LeftButton", Content);
            _assetManager.LoadAsset<Texture2D>("RightButton", Content);

            _scenesService.Register<SceneMenu>();
            _scenesService.Register<SceneGame>();
            _scenesService.Register<SceneWin>();
            _scenesService.Register<SceneGameOver>();
            _scenesService.Register<SceneBuildLevel>();


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