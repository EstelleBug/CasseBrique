using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Services;

namespace BrickBreaker
{
    public interface IGameObject
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }
    
        public Rectangle CollisionBox { get; }

        public bool free { get; set; }

        public void Update(GameTime gameTime);
        public void Draw();

    }
    
}