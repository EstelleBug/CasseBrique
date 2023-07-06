using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scenes;
using Services;

namespace BrickBreaker
{
    public delegate void onClick(Button pSender);
    public class Button : IGameObject
    {
        public Vector2 position { get ; set; }
        public Texture2D texture { get; set; }

        public Rectangle CollisionBox => new Rectangle((int)(position.X - texture.Width / 2), (int)(position.Y - texture.Height / 2), (int)texture.Width, (int)texture.Height);

        public bool free { get; set; }

        public bool isHover { get; private set; }
        private MouseState _oldMouseState;
        public onClick onClick { get; set; }

        public Button (string textureName, Vector2 position)
        {
            texture = ServiceLocator.Get<IAssetsManager>().GetAsset<Texture2D>(textureName);
            this.position = position;
            isHover = false;
            _oldMouseState = Mouse.GetState();
            Scene.Add(this);
        }

        public void Draw()
        {
            SpriteBatch sb = ServiceLocator.Get<SpriteBatch>();
            sb.Draw(texture, position - new Vector2(texture.Width, texture.Height) / 2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Point MousePos = Mouse.GetState().Position;
            MouseState newMouseState = Mouse.GetState();

            if (CollisionBox.Contains(MousePos))
            {
                if (!isHover)
                {
                    isHover = true;
                }
            }
            else
                isHover = false;

            if (isHover && newMouseState.LeftButton == ButtonState.Pressed
                && _oldMouseState.LeftButton != ButtonState.Pressed)
            {
                if (onClick != null)
                    onClick(this);
            }

            _oldMouseState = newMouseState;
        }
    }
}
