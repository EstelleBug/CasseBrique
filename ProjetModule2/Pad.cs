using BrickBreaker;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Services;
using Scenes;

namespace BrickBreaker
{
    internal class Pad : IGameObject
    {
        private float _speed = 500f;
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        private Ball ball { get; set; }
        public bool free { get; set; } = false;

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2), (int)texture.Width, (int)texture.Height);

        public Pad()
        {
            texture = ServiceLocator.Get <IAssetsManager>().GetAsset<Texture2D>("raquette");
            position = new Vector2(Main._screenSize.X / 2 - texture.Width / 2, Main._screenSize.Y - texture.Height / 2 - 30);
            ball = new Ball(Vector2.Zero);
            Scene.Add(this);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var direction = Vector2.Zero;


            if (ks.IsKeyDown(Keys.Left))
            {
                if (position.X - (texture.Width / 2) > 0)
                { direction.X = -1; }
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                if (position.X < Main._screenSize.X - (texture.Width /2) )
                { direction.X = +1; }
            }
                

            position += direction * dt * _speed;

            if (ball != null)
            {
                
                ball.position = new Vector2((int)position.X, (int)position.Y) - new Vector2(0, texture.Height);

                if(ks.IsKeyDown(Keys.Space))
                {
                    ball.sticked = false;
                    ball.velocity = new Vector2(-1, -1);
                    ball = null;
                }
            }

            if (Scene.GetGameObjects<Ball>().Count <= 0) ball = new Ball(Vector2.Zero);
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position - new Vector2(texture.Width, texture.Height) / 2, Color.White);
        }
    }
}
