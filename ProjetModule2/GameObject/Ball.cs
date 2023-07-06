using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;
using System;
using System.Diagnostics;

namespace BrickBreaker
{
    public class Ball : IGameObject
    {
        private Random random = new Random();
        private int _freeBrickCount = 0;

        private float _speed = 300f;
        public Vector2 velocity = Vector2.Zero;
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public bool free { get; set; } = false;
        public bool sticked = true;
        private bool _colorChange = false;

        private bool _isDiscoActive = false;
        private float _discoTimer = 0f;
        private const float _discoDuration = 3f;

        private Color _currentColor = Color.Red;

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2), (int)texture.Width, (int)texture.Height);
        private float _shakeDuration = 0.1f;
        private float _shakeTimer = 0f;

        private Type[] _powerUpTypes = { typeof(PowerUpLife), typeof(PowerDown), typeof(PowerUpDisco) };
        private CollisionManager _collisionManager = ServiceLocator.Get<CollisionManager>();

        public Ball(Vector2 Velocity)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("balle");
            velocity = Velocity;
            velocity.Normalize();
            Scene.Add(this);
        }

        public void Update(GameTime gameTime)
        {
            if (sticked) return; // If the ball is sticked, no need to update

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity * _speed * dt;
            GameManager gameManager = ServiceLocator.Get<GameManager>();

            foreach (Brick brick in Scene.GetGameObjects<Brick>())
            {
                if (_collisionManager.BounceOn(brick.CollisionBox, gameTime, position, velocity, _speed, texture))
                {
                    velocity = _collisionManager.BallBounceOn(brick.CollisionBox, gameTime, position, velocity, _speed, texture);
                    
                    // Change ball color for every collision with a brick
                    _colorChange = true;

                    if (_isDiscoActive)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(30);
                    }

                    foreach (Brick otherBrick in Scene.GetGameObjects<Brick>())
                    {
                        otherBrick.isShaking = true;
                        _shakeTimer = _shakeDuration;
                    }

                    if (brick is BrickRed && _currentColor == Color.Red)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(20);
                        _freeBrickCount++;
                    }
                    else if (brick is BrickGreen && _currentColor == Color.Green)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(20);
                        _freeBrickCount++;
                    }
                    else if (brick is BrickBlue && _currentColor == Color.Blue)
                    {
                        brick.free = true;
                        gameManager.IncreaseScore(20);
                        _freeBrickCount++;
                    }
                    else if (brick is BrickPowerDown)
                    {
                        brick.free = true;
                        Brick.TransformPowerDownToBricks(brick.position);
                        gameManager.IncreaseScore(5);
                    }

                    // If the number of brick free reaches 2, create a random bonus
                    if (brick.free && _freeBrickCount % 2 == 0)
                    {
                        Vector2 powerUpPosition = brick.position;
                        // Select a random bonus type
                        Type randomPowerUpType = _powerUpTypes[random.Next(_powerUpTypes.Length)];
                        // Create an instance of the selected bonus
                        PowerUp powerUp = (PowerUp)Activator.CreateInstance(randomPowerUpType, powerUpPosition);

                        _freeBrickCount = 0;
                    }

                }
            }

            foreach (Pad pad in Scene.GetGameObjects<Pad>())
            {
                if (_collisionManager.BounceOn(pad.LeftZone, gameTime, position, velocity, _speed, texture))
                {
                    velocity = _collisionManager.BallBounceOn(pad.LeftZone, gameTime, position, velocity, _speed, texture);
                }
                else if (_collisionManager.BounceOn(pad.MidLeftZone, gameTime, position, velocity, _speed, texture))
                {
                    velocity = _collisionManager.BallBounceOn(pad.MidLeftZone, gameTime, position, velocity, _speed, texture);

                }
                else if (_collisionManager.BounceOn(pad.MidRightZone, gameTime, position, velocity, _speed, texture))
                {
                    velocity = _collisionManager.BallBounceOn(pad.MidRightZone, gameTime, position, velocity, _speed, texture);

                }
                else if (_collisionManager.BounceOn(pad.RightZone, gameTime, position, velocity, _speed, texture))
                {
                    velocity = _collisionManager.BallBounceOn(pad.RightZone, gameTime, position, velocity, _speed, texture);

                }
            }

            Bounce(Main.screenSize);

            if (_colorChange)
            {
                _currentColor = GetRandomColor();
                _colorChange = false;
            }

            if (_isDiscoActive)
            {
                // Change ball color every update
                _currentColor = GetDiscoColor();

                _discoTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_discoTimer >= _discoDuration)
                {
                    _discoTimer = 0f;
                    SetDiscoActive(false);
                }
            }

            // Update shake timer
            if (_shakeTimer > 0f)
            {
                _shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                // if shake timer is up, stop shaking & reset position brick
                if (_shakeTimer <= 0f)
                {
                    _shakeTimer = 0f;

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

        public void SetDiscoActive(bool isActive)
        {
            Debug.WriteLine(isActive);
            _isDiscoActive = isActive;

            if (_isDiscoActive == false)
            {
                // reset ball color to default (red)
                _currentColor = Color.Red;
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

        private Color GetDiscoColor()
        {
            Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Purple, Color.Orange };
            int randomIndex = random.Next(colors.Length);
            return colors[randomIndex];
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position - new Vector2(texture.Width, texture.Height) / 2, _currentColor);
        }
    }
}

