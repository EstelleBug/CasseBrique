using BrickBreaker;
using Microsoft.Xna.Framework;
using Services;
using System;
using Newtonsoft.Json;
using System.IO;
using static Services.LevelsLoader;
using System.Diagnostics;

namespace Scenes

{
    public class SceneGame : Scene
    {
        //private Point bounds = Main._screenSize;
        //private Random random = new Random();
        private UIManager _uiManager;
        private GameManager _gameManager;
        private LevelsLoader _levelsLoader;

        //private LevelData[] levels;

        /*public class LevelData
        {
            public int[,] Bricks { get; set; }
        }*/
        public override void Load()
        {
            base.Load();
            _gameManager = (GameManager)ServiceLocator.Get<GameManager>();

            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            LevelsLoader.LevelData levelData = _levelsLoader.GetCurrentLevel();
            //levels = LoadLevels();

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

        /*private LevelData[] LoadLevels()
        {
            int levelNumber = gameManager.Level;

            string filePath = "C:\\Users\\estel\\OneDrive\\Documents\\Formation_JV\\C#\\Projet\\ProjetModule2\\ProjetModule2\\Levels\\" + $"level_{levelNumber}.json";
            string jsonContent = File.ReadAllText(filePath);
            Console.WriteLine(jsonContent);

            LevelData[] levels = JsonConvert.DeserializeObject<LevelData[]>(jsonContent);

            //string json = File.ReadAllText("C:\\Users\\estel\\OneDrive\\Documents\\Formation_JV\\C#\\Projet\\ProjetModule2\\ProjetModule2\\Levels\\" + $"level_{levelNumber}.json"); // Remplacez "levels.json" par le chemin d'accès à votre fichier JSON contenant les données des niveaux
            //LevelData[] levels = JsonConvert.DeserializeObject<LevelData[]>(json);
            return levels;
        }*/

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public override void Draw()
        {
            base.Draw();
            _uiManager.Draw();
        }
    }
}

/*for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 22; col++)
                {
                    int xPos = 40 + col * brickWidth;
                    int yPos = 40 + row * brickHeight;

                    int randomNumber = random.Next(3);

                    if (randomNumber == 0)
                    { 
                        new BrickRed(new Vector2(xPos, yPos));
                    }
                    else if (randomNumber == 1)
                    {
                        new BrickGreen(new Vector2(xPos, yPos));
                    }
                    else if (randomNumber == 2)
                    {
                        new BrickBlue(new Vector2(xPos, yPos));
                    }
                }
            }*/


/* for (int i = 50; i <= bounds.X; i += 55)
            {
                for (int j = 20; j <= 200; j += 20)
                {
                    //new Brick(new Vector2(i,j));
                    if (i % 2 == 0) // Créez une brique rouge si la position est paire
                    {
                        new BrickRed(new Vector2(i, j));
                    }
                    else // Créez une brique verte si la position est impaire
                    {
                        new BrickGreen(new Vector2(i, j));
                    }
                }

            }*/