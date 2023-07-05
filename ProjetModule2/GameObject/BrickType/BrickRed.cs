using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;

namespace BrickBreaker
{
    internal class BrickRed : Brick
    {
        public BrickRed(Vector2 position) : base(position)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("brique_5_3");
            this.position = position;
            Scene.Add(this);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
