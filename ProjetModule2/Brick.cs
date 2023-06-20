using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Services;
using System;
using Scenes;
using System.Runtime.CompilerServices;

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
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("brique_0_0");
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
    }
}
