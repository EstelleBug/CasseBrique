using BrickBreaker;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Services
{
    public class CollisionManager
    {
        public CollisionManager() { }

        private Rectangle NextHorizontalCollisionBox(GameTime gameTime, Vector2 position, Vector2 velocity, float speed, Texture2D texture)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 nextPosition = position + new Vector2(velocity.X * speed * dt, 0);
            return new Rectangle((int)(nextPosition.X - texture.Width / 2), (int)(nextPosition.Y - texture.Height / 2), texture.Width, texture.Height);
        }
        private Rectangle NextVerticalCollisionBox(GameTime gameTime, Vector2 position, Vector2 velocity, float speed, Texture2D texture)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 nextPosition = position + new Vector2(0, velocity.Y * speed * dt);
            return new Rectangle((int)(nextPosition.X - texture.Width / 2), (int)(nextPosition.Y - texture.Height / 2), texture.Width, texture.Height);
        }
        public bool BounceOn(Rectangle otherCollisionBox, GameTime gameTime, Vector2 position, Vector2 velocity, float speed, Texture2D texture)
        {
            if (otherCollisionBox.Intersects(NextHorizontalCollisionBox(gameTime, position, velocity, speed, texture)))
            {
                return true;
            }
            else if (otherCollisionBox.Intersects(NextVerticalCollisionBox(gameTime, position, velocity, speed, texture)))
            {
                return true;
            }

            return false;
        }

        public Vector2 ReflectVelocity(Vector2 velocity, Vector2 normal)
        {
            // Calculer le produit scalaire de la vitesse et de la normale
            float dotProduct = Vector2.Dot(velocity, normal);

            // Calculer la réflexion de la vitesse
            Vector2 reflectedVelocity = velocity - 2f * dotProduct * normal;

            return reflectedVelocity;
        }

        public Vector2 BallBounceOn(Rectangle otherCollisionBox, GameTime gameTime, Vector2 position, Vector2 velocity, float speed, Texture2D texture)
        {
            Vector2 newVelocity = velocity;

            if (otherCollisionBox.Intersects(NextHorizontalCollisionBox(gameTime, position, velocity, speed, texture)))
            {
                // Calculer la normale de la surface de collision (dans ce cas, la normale X)
                Vector2 normal = new Vector2(1, 0);

                // Réfléchir la vitesse par rapport à la normale
                newVelocity = ReflectVelocity(velocity, normal);
            }
            else if (otherCollisionBox.Intersects(NextVerticalCollisionBox(gameTime, position, velocity, speed, texture)))
            {
                // Calculer la normale de la surface de collision (dans ce cas, la normale Y)
                Vector2 normal = new Vector2(0, 1);

                // Réfléchir la vitesse par rapport à la normale
                newVelocity = ReflectVelocity(velocity, normal);

            }

            return newVelocity;
        }

    }
}
