using BrickBreaker;
using Microsoft.Xna.Framework;
using Services;
using System;

namespace Scenes

{
    public class SceneGame : Scene
    {

        private Point bounds = Main._screenSize;
        private Random random = new Random();
        private UIManager uiManager;
        public override void Load()
        {
            base.Load();

            new Pad();

            int brickWidth = 55;
            int brickHeight = 20;

            for (int row = 0; row < 7; row++)
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
            }
            uiManager = (UIManager)ServiceLocator.Get<UIManager>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public override void Draw()
        {
            base.Draw();
            uiManager.Draw();
        }
    }
}

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