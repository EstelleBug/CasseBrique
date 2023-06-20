using BrickBreaker;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Services
{
    public class CollisionManager
    {
        private List<Brick> bricks;
        private Ball ball;
        private Pad pad;

        public CollisionManager()
        {
        }

        public void Update(GameTime gameTime)
        {
            HandleBrickCollisions(gameTime);
            HandlePadCollision(gameTime);
            HandleWallCollision();
        }

        private void HandleBrickCollisions(GameTime gameTime)
        {
            foreach (Brick brick in bricks)
            {
                if (brick.CollisionBox.Intersects(ball.CollisionBox))
                {
                    bool brickDestroyed = brick.free;

                    if (brickDestroyed)
                    {
                        ball.BounceOn(brick.CollisionBox, gameTime);
                    }
                }
            }
        }

        private void HandlePadCollision(GameTime gameTime)
        {
            if (pad.CollisionBox.Intersects(ball.CollisionBox))
            {
                ball.BounceOn(pad.CollisionBox, gameTime);
            }
        }

        private void HandleWallCollision()
        {
            Point screenSize = Main._screenSize;

            if (ball.position.X < 0 || ball.position.X > screenSize.X - ball.texture.Width)
            {
                //ball.BounceOnWall();
            }

            if (ball.position.Y < 0)
            {
                //ball.BounceOnCeiling();
            }

            if (ball.position.Y - ball.texture.Height > screenSize.Y)
            {
                ball.free = true;
            }
        }
    }
}
