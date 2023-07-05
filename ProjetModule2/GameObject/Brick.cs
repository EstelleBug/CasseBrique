using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Services;
using System;
using Scenes;
using System.Collections.Generic;
using System.Linq;

namespace BrickBreaker
{
    internal class Brick : IGameObject
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2),(int) texture.Width,(int) texture.Height);
        public bool free { get; set; } = false;
        public bool isShaking { get; set; } = false;

        private Random random = new Random();
        private float shakeTimer = 0f;
        private const float shakeDuration = 0.2f;
        private const float shakeMagnitude = 2f;
        private Vector2 originalPosition;
        private Vector2 positionBeforeShake;


        public Brick(Vector2 position)
        {
            texture = null;
            this.position = position;
            originalPosition = position;
            Scene.Add(this);
        }

        public virtual void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position - new Vector2(texture.Width, texture.Height) / 2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (isShaking)
            {
                // Démarrez le tremblement
                if (shakeTimer <= 0f)
                {
                    positionBeforeShake = position;
                    shakeTimer = shakeDuration;
                }

                // Effectuez le tremblement
                if (shakeTimer > 0f)
                {
                    Vector2 shakeOffset = new Vector2(
                        (float)(random.NextDouble() - 0.5) * 2f * shakeMagnitude,
                        (float)(random.NextDouble() - 0.5) * 2f * shakeMagnitude
                    );

                    position = positionBeforeShake + shakeOffset;
                    shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public void ResetPosition()
        {
            position = originalPosition;
        }

        public static void TransformBricksToPowerDown(float percentage)
        {
            List<Brick> bricks = Scene.GetGameObjects<Brick>().OfType<Brick>().ToList();
            int bricksToTransform = (int)(bricks.Count * percentage);

            // Mélanger aléatoirement la liste de briques
            bricks = bricks.OrderBy(item => Guid.NewGuid()).ToList();


            for (int i = 0; i < bricksToTransform; i++)
            {
                Brick brick = bricks[i];
                Vector2 position = brick.position;
                BrickPowerDown brickPowerDown = new BrickPowerDown(position);
                // Supprimez la brique d'origine
                brick.free = true;
            }
        }

        public static void TransformPowerDownToBricks(Vector2 position)
        {
            Random random = new Random();
            int randomNumber = random.Next(3);

            if (randomNumber == 0)
            {
                new BrickRed(position);
            }
            else if (randomNumber == 1)
            {
                new BrickGreen(position);
            }
            else if (randomNumber == 2)
            {
                new BrickBlue(position);
            }
        }
    }
}
