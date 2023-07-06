using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;
using System;


namespace BrickBreaker
{
    internal class PowerUp : IGameObject
    {
        protected float _speed = 100f;
        protected Vector2 velocity = Vector2.Zero;
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2), (int)texture.Width, (int)texture.Height);

        public bool free { get; set; }

        public PowerUp(Vector2 startPosition)
        {
            position = startPosition;
            texture = null;
            Scene.Add(this);
        }

        public virtual void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity.Y = +1;

            position += velocity * dt * _speed;

            if (position.Y >= Main.screenSize.Y)
            {
                free = true;
            }
        }
    }
}
