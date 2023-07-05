using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Services;
using System.Diagnostics;
using BrickBreaker;
using static Services.LevelsLoader;

namespace Scenes
{
    internal class SceneBuildLevel : Scene
    {
        private bool _RightPressed = false;
        private bool _LeftPressed = false;
        private bool _SpacePressed = false;
        private UIManager _uiManager;
        private GameManager _gameManager;
        private LevelsLoader _levelsLoader;
        public override void Load()
        {
            base.Load();
            _gameManager = (GameManager)ServiceLocator.Get<GameManager>();

            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            LevelData levelData = _levelsLoader.GetCurrentLevel();

            _uiManager = (UIManager)ServiceLocator.Get<UIManager>();

            int brickWidth = 55;
            int brickHeight = 20;

            int rows = levelData.height;
            int cols = levelData.width;



            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int brickValue = levelData.bricks[row][col];
                    int xPos = 40 + col * brickWidth;
                    int yPos = 40 + row * brickHeight;

                    switch (brickValue)
                    {
                        case 1:
                            new BrickRed(new Vector2(xPos, yPos));
                            break;
                        case 2:
                            new BrickGreen(new Vector2(xPos, yPos));
                            break;
                        case 3:
                            new BrickBlue(new Vector2(xPos, yPos));
                            break;
                    }
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && !_RightPressed)
            {
                Debug.WriteLine("Right pressed");

                _gameManager.IncreaseLevel();
                Load();
            }
            else if (ks.IsKeyDown(Keys.Left) && !_LeftPressed)
            {
                Debug.WriteLine("Left Pressed");
                _gameManager.DecreaseLevel();
                Load();
            }
            else if (ks.IsKeyDown(Keys.Space) && !_SpacePressed)
            {
                Debug.WriteLine("Space Pressed");
                LevelData _currentLevel = _levelsLoader.GetCurrentLevel();
                _levelsLoader.SaveLevelToJson(_currentLevel);
            }

            _RightPressed = ks.IsKeyDown(Keys.Right);
            _LeftPressed = ks.IsKeyDown(Keys.Left);
            _SpacePressed = ks.IsKeyDown(Keys.Space);
        }

        public override void Draw()
        {
            base.Draw();
            _uiManager.Draw();
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            SpriteFont MainFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            sb.DrawString(MainFont, "Build your own level", new Vector2(Main._screenSize.X/2 - 60, 1), Color.White);
        }

        public override void Unload()
        {
        }
    }
}
