using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;

namespace BrickBreaker
{
    internal class PowerUpLife : PowerUp
    {
        public PowerUpLife(Vector2 startPosition) : base(startPosition)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("bonus_vie2");
            this.position = startPosition;
            Scene.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            GameManager gameManager = ServiceLocator.Get<GameManager>();
            CollisionManager collisionManager = ServiceLocator.Get<CollisionManager>();
            base.Update(gameTime);

            foreach (Pad pad in Scene.GetGameObjects<Pad>())
            {
                if (collisionManager.BounceOn(pad.CollisionBox, gameTime, position, velocity, _speed, texture))
                {
                    free = true;
                    gameManager.IncreaseLives(1);

                }
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
