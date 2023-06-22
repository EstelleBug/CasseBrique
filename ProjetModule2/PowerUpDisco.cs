using BrickBreaker;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Scenes;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetModule2
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

                }
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
