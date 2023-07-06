using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Services;
using BrickBreaker;

namespace Scenes
{
    internal class SceneGameOver : Scene
    {
        private GameManager _gameManager;
        private Button ButtonMenu;

        public void onClickMenu(Button pSender)
        {
            IScenesService scenesService = ServiceLocator.Get<IScenesService>();
            scenesService.Load<SceneMenu>();
            _gameManager.ResetGame();

        }

        public override void Load()
        {
            base.Load();
            _gameManager = (GameManager)ServiceLocator.Get<GameManager>();

            ButtonMenu = new Button("MenuButton", new Vector2((Main.screenSize.X / 2), 300));
            ButtonMenu.onClick = onClickMenu;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public override void Draw()
        {
            base.Draw();
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            SpriteFont MainFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            sb.DrawString(MainFont, "Game Over", new Vector2(1, 1), Color.White);
            sb.DrawString(MainFont, $"Score {_gameManager.Score}", new Vector2(Main.screenSize.X/2 - 20, 200), Color.White);

        }

        public override void Unload()
        {
        }
    }
}
