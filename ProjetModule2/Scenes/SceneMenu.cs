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
        private bool _EnterPressed = false;
        private Button ButtonPlay;

        public void onClickPlay(Button pSender)
        {
            IScenesService scenesService = ServiceLocator.Get<IScenesService>();
            scenesService.Load<SceneGame>();
        }

        public override void Load()
        {
            base.Load();

            ButtonPlay = new Button();
            ButtonPlay.onClick = onClickPlay;
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter) && !_EnterPressed)
            {
                Debug.WriteLine("Enter pressed");

                IScenesService scenesService = ServiceLocator.Get<IScenesService>();
                scenesService.Load<SceneGame>();
            }

            _EnterPressed = ks.IsKeyDown(Keys.Enter);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            //SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            //SpriteFont MainFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            //sb.DrawString(MainFont, "This is the menu, press Enter to start the game", new Vector2(1, 1), Color.White);
            base.Draw();
        }

        public override void Unload()
        {
        }
    }
}
