using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;
using Services;

namespace BrickBreaker
{
    internal class BrickPowerDown : Brick
    {
        public BrickPowerDown(Vector2 position) : base(position)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>("brique_0_0");
            this.position = position;
            Scene.Add(this);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
