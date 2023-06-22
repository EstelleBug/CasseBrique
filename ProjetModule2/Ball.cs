using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjetModule2;
using Scenes;
using Services;
using System;
using System.Diagnostics;

namespace BrickBreaker
{
    public class Ball : IGameObject
    {
        private Random random = new Random();
        private int freeBrickCount = 0;

        private float _speed = 300f;
        public Vector2 velocity = Vector2.Zero;
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public bool free { get; set; } = false;
        public bool sticked = true;
        private bool _colorChange = false;

        private Color currentColor = Color.Red;

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2), (int)texture.Width, (int)texture.Height);
        private float shakeDuration = 0.1f;
        private float shakeTimer = 0f;

        private Type[] powerUpTypes = { typeof(PowerUpLife), typeof(PowerDown), typeof(PowerUpDisco) };
        private CollisionManager collisionManager = ServiceLocator.Get<CollisionManager>();

        public Ball(Vector2 Velocity)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("balle");
            velocity = Velocity;
            velocity.Normalize();
            Scene.Add(this);
        }

        public void Update(GameTime gameTime)
        {
            if (sticked) return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity * _speed * dt;
            GameManager gameManager = ServiceLocator.Get<GameManager>();

            foreach (Brick brick in Scene.GetGameObjects<Brick>())
            {
                if (collisionManager.BounceOn(brick.CollisionBox, gameTime, position, velocity, _speed, texture))
                {
                    velocity = collisionManager.BallBounceOn(brick.CollisionBox, gameTime, position, velocity, _speed, texture);

                    _colorChange = true;

                    foreach (Brick otherBrick in Scene.GetGameObjects<Brick>())
                    {
                        otherBrick.isShaking = true;
                        shakeTimer = shakeDuration;
                    }

                    if (brick is BrickRed && currentColor == Color.Red)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(20);
                        freeBrickCount++;
                    }
                    else if (brick is BrickGreen && currentColor == Color.Green)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(20);
                        freeBrickCount++;
                    }
                    else if (brick is BrickBlue && currentColor == Color.Blue)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(20);
                        freeBrickCount++;
                    }

                    // Si le nombre de briques "free" atteint 2, créez un bonus
                    if (brick.free && freeBrickCount % 2 == 0)
                    {
                        Vector2 powerUpPosition = brick.position;
                        // Sélectionner un type de bonus aléatoire
                        Type randomPowerUpType = powerUpTypes[random.Next(powerUpTypes.Length)];
                        // Créer une instance du type de bonus sélectionné
                        PowerUp powerUp = (PowerUp)Activator.CreateInstance(randomPowerUpType, powerUpPosition);

                        freeBrickCount = 0;
                    }

                }
            }

            foreach (Pad pad in Scene.GetGameObjects<Pad>())
            {
                velocity = collisionManager.BallBounceOn(pad.CollisionBox, gameTime, position, velocity, _speed, texture);

            }

            Bounce(Main._screenSize);

            if (_colorChange)
            {
                currentColor = GetRandomColor();
                Debug.WriteLine(GetRandomColor());
                _colorChange = false;
            }

            // Mettez à jour le timer de tremblement
            if (shakeTimer > 0f)
            {
                shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Si le temps de tremblement est écoulé, arrêtez le tremblement et réinitialisez les briques
                if (shakeTimer <= 0f)
                {
                    shakeTimer = 0f;

                    foreach (Brick brick in Scene.GetGameObjects<Brick>())
                    {
                        brick.isShaking = false;
                        brick.ResetPosition();
                    }
                }
            }
        }

        private void Bounce(Point screenSize)
        {
            Vector2 newPosition = position;
            if (position.X < 0)
            {
                newPosition.X = 0;
                velocity.X *= -1f;
            }
            else if (position.X > screenSize.X - texture.Width)
            {
                newPosition.X = screenSize.X - texture.Width;
                velocity.X *= -1f;
            }

            if (position.Y < 0)
            {
                newPosition.Y = 0;
                velocity.Y *= -1f;
            }

            if (position.Y - texture.Height > screenSize.Y)
            {
                free = true;
                GameManager gameManager = ServiceLocator.Get<GameManager>();
                gameManager.DecreaseLives();
            }
        }

        private Color GetRandomColor()
        {
            int randomIndex = random.Next(3);

            switch (randomIndex)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                default:
                    return Color.White;
            }
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position - new Vector2(texture.Width, texture.Height) / 2, currentColor);
        }
    }
}

