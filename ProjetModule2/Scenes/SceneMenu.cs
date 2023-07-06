using BrickBreaker;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Services;
using System.Diagnostics;

namespace Scenes
{
    public class SceneMenu : Scene
    {
        private Button ButtonPlay;
        private Button ButtonOptions;

        public void onClickPlay(Button pSender)
        {
            IScenesService scenesService = ServiceLocator.Get<IScenesService>();
            scenesService.Load<SceneGame>();
        }
        public void onClickOptions(Button pSender)
        {
            IScenesService scenesService = ServiceLocator.Get<IScenesService>();
            scenesService.Load<SceneBuildLevel>();
        }

        public override void Load()
        {
            base.Load();

            ButtonPlay = new Button("PlayButton", new Vector2((Main._screenSize.X / 2), 200));
            ButtonPlay.onClick = onClickPlay;
            ButtonOptions = new Button("OptionsButton", new Vector2((Main._screenSize.X / 2), 300));
            ButtonOptions.onClick = onClickOptions;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();

            //SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            //SpriteFont MainFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            //sb.DrawString(MainFont, "This is the menu, press Enter to start the game", new Vector2(1, 1), Color.White);
        }

        public override void Unload()
        {
        }
    }
}
