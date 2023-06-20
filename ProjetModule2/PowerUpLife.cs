using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;
using System;


namespace BrickBreaker
{
    internal class PowerUpLife : IGameObject
    {
        private float _speed = 100f;
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2), (int)texture.Width, (int)texture.Height);

        public bool free { get; set; }

        public PowerUpLife(Vector2 startPosition)
        {
            position = startPosition;
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("bonus_vie");
            Scene.Add(this);
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var direction = Vector2.Zero;

            direction.Y = +1;

            position += direction * dt * _speed;

            if (position.Y >= Main._screenSize.Y)
            {
                free = true;
            }
        }
    }
}
