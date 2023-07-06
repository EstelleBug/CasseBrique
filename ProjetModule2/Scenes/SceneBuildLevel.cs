using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Services;
using System.Diagnostics;
using BrickBreaker;
using static Services.LevelsLoader;
using System.Collections.Generic;
using System;

namespace Scenes
{
    internal class SceneBuildLevel : Scene
    {
        private Button ButtonLeft;
        private Button ButtonRight;
        private Button ButtonCancel;
        private Button ButtonSave;

        private bool _MouseButtonPressed;

        private UIManager _uiManager;
        private GameManager _gameManager;
        private LevelsLoader _levelsLoader;

        private List<Brick> _bricks = new List<Brick>();
        public override void Load()
        {
            base.Load();
            _gameManager = (GameManager)ServiceLocator.Get<GameManager>();

            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            LevelData levelData = _levelsLoader.GetCurrentLevel();

            _uiManager = (UIManager)ServiceLocator.Get<UIManager>();

            ButtonLeft = new Button("LeftButton", new Vector2((Main.screenSize.X / 2 - 80), 650));
            ButtonLeft.onClick = onClickLeft;
            ButtonRight = new Button("RightButton", new Vector2((Main.screenSize.X / 2 + 80), 650));
            ButtonRight.onClick = onClickRight;
            ButtonCancel = new Button("CancelButton", new Vector2((Main.screenSize.X - 60), 650));
            ButtonCancel.onClick = onClickCancel;
            ButtonSave = new Button("SaveButton", new Vector2((Main.screenSize.X -120), 650));
            ButtonSave.onClick = onClickSave;

            int brickWidth = 48;
            int brickHeight = 16;

            int rows = levelData.height;
            int cols = levelData.width;

            _bricks.Clear();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int brickValue = levelData.bricks[row][col];
                    int xPos = 40 + col * brickWidth;
                    int yPos = 40 + row * brickHeight;

                    Brick brick = null;

                    switch (brickValue)
                    {
                        case 0:
                            brick = null;
                            break;
                        case 1:
                            brick = new BrickRed(new Vector2(xPos, yPos));
                           
                            break;
                        case 2:
                            brick = new BrickGreen(new Vector2(xPos, yPos));
                            break;
                        case 3:
                            brick = new BrickBlue(new Vector2(xPos, yPos));
                            break;
                    }
                    if (brick != null)
                    {
                        brick.Value = brickValue; //Assign brick value depending of the Data
                        _bricks.Add(brick);
                    }
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            LevelData levelData = _levelsLoader.GetCurrentLevel();


            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed &&! _MouseButtonPressed)
            {
                // Get mouse position in pixels
                Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

                // Convert mouse position to grid position
                Vector2 gridPosition = (mousePosition - new Vector2(40, 40) + new Vector2(48, 16) /2) / new Vector2(48, 16);

                // Get grid Index of the brick 
                int row = (int)Math.Floor(gridPosition.Y);
                int col = (int)Math.Floor(gridPosition.X);

                // Verify if grid index are within the grid bounds
                if (row >= 0 && row < levelData.height && col >= 0 && col < levelData.width)
                {
                    if (levelData.bricks[row][col] < 3)
                    {
                        // Update brick value in the grid
                        levelData.bricks[row][col] += 1;
                    }
                    else if (levelData.bricks[row][col] == 3)
                    {
                        levelData.bricks[row][col] = 0;
                    }

                    // Reload level to update changes
                    Load();
                }

            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            { _MouseButtonPressed = true; }
            else
            { _MouseButtonPressed = false; }
        }

        public void onClickLeft(Button pSender)
        {
            Debug.WriteLine("Left Pressed");
            _gameManager.DecreaseLevel();
            Load();
        }

        public void onClickRight(Button pSender)
        {
            Debug.WriteLine("Right pressed");
            _gameManager.IncreaseLevel();
            Load();
        }

        public void onClickCancel(Button pSender)
        {
            Debug.WriteLine("delete level");
            _levelsLoader.DeleteLevel();
        }

        public void onClickSave(Button pSender)
        {
            Debug.WriteLine("save level");
            LevelData levelData = _levelsLoader.GetCurrentLevel();

            // Update lele data with new brick value
            for (int row = 0; row < levelData.height; row++)
            {
                for (int col = 0; col < levelData.width; col++)
                {
                    if (row < _bricks.Count / levelData.width && col < levelData.width)
                    {
                        // Update brick value in JSON
                        int brickValue = _bricks[row * levelData.width + col]?.Value ?? 0;
                        levelData.bricks[row][col] = brickValue;
                    }
                }
            }

            // Save new data
            _levelsLoader.SaveExistingLevel(levelData);
        }

        public override void Draw()
        {
            base.Draw();
            _uiManager.Draw();
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            SpriteFont MainFont = ServiceLocator.Get<IAssetsManager>().GetAsset<SpriteFont>("PixelFont");
            sb.DrawString(MainFont, "Build your own level", new Vector2(Main.screenSize.X/2 - 70, 1), Color.White);
        }

        public override void Unload()
        {
        }
    }
}
