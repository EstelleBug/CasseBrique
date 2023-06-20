using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;

namespace BrickBreaker
{
    internal class BrickBlue : Brick
    {
        public BrickBlue(Vector2 position) : base(position)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("brique_8_3");
            this.position = position;
            Scene.Add(this);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}