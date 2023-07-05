using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;

namespace BrickBreaker
{
    internal class PowerDown : PowerUp
    {
        public PowerDown(Vector2 startPosition) : base(startPosition)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("malus");
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
                    // Transforme 5% des briques en briques malus
                    Brick.TransformBricksToPowerDown(0.05f);

                }
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
