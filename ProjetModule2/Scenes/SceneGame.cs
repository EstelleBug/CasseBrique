using BrickBreaker;
using Microsoft.Xna.Framework;
using Services;

namespace Scenes

{
    public class SceneGame : Scene
    {
        private UIManager _uiManager;
        private GameManager _gameManager;
        private LevelsLoader _levelsLoader;

        public override void Load()
        {
            base.Load();
            _gameManager = (GameManager)ServiceLocator.Get<GameManager>();

            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            LevelsLoader.LevelData levelData = _levelsLoader.GetCurrentLevel();

            _uiManager = (UIManager)ServiceLocator.Get<UIManager>();

            new Pad();

            int brickWidth = 55;
            int brickHeight = 20;

            int rows = levelData.height;
            int cols = levelData.width;



            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int brickValue = levelData.bricks[row] [col];
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

            // Vérifier si toutes les briques sont détruites
            bool allBricksDestroyed = AreAllBricksDestroyed();

            if (allBricksDestroyed && _gameManager.Lives > 0)
            {
                _gameManager.IncreaseLevel();
                Load();
            }
        }

        public override void Draw()
        {
            base.Draw();
            _uiManager.Draw();
        }
    }
}