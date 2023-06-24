using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Scenes;
using Services;
using System.Linq;
using System.Diagnostics;
using System;

namespace BrickBreaker
{
    internal class PowerUpDisco : PowerUp
    {

        public PowerUpDisco(Vector2 startPosition) : base(startPosition)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("bonus_disco");
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
                    // Récupérer l'instance de Ball depuis la scène ou une autre source
                    Ball ball = Scene.GetGameObjects<Ball>().FirstOrDefault() as Ball;

                    // Vérifier si l'instance de Ball a été trouvée
                    if (ball != null)
                    {
                        ball.SetDiscoActive(true);
                    }
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
