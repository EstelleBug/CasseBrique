using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Services;
using System.Diagnostics;

namespace Scenes
{
    internal class SceneGameOver : Scene
    {
        private bool _SpacePressed = false;
        public override void Load()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space) && !_SpacePressed)
            {
                Debug.WriteLine("Space pressed");

                IScenesService scenesService = ServiceLocator.Get<IScenesService>();
                scenesService.Load<SceneMenu>();
            }

            _SpacePressed = ks.IsKeyDown(Keys.Space);
        }

        public override void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            SpriteFont MainFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            sb.DrawString(MainFont, "Game Over", new Vector2(1, 1), Color.White);
        }

        public override void Unload()
        {
        }
    }
}
