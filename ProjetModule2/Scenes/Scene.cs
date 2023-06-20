using BrickBreaker;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Services;
using System.Collections.Generic;

namespace Scenes
{
    public class Scene
    {
        private static List<IGameObject> _gameObjects = new List<IGameObject>();

        protected SpriteBatch _spriteBatch = ServiceLocator.Get<SpriteBatch>();
        protected IAssetsManager _assetsManager = ServiceLocator.Get<IAssetsManager>();
        protected IScenesService _scenesService = ServiceLocator.Get<IScenesService>();

        public virtual void Load() 
        { 
            _gameObjects.Clear();
        }
        public virtual void Unload() { }
        public virtual void Update(GameTime gameTime) 
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime);
            }

            for (int i =  _gameObjects.Count - 1; i >= 0; i--)
            {
                if (_gameObjects[i].free)
                {
                    _gameObjects.RemoveAt(i);
                }
            }
        }
        public virtual void Draw() 
        {
            foreach (IGameObject gameObject in _gameObjects)
            {
                gameObject.Draw();
            }
        }

        public static void Add(IGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public static List<IGameObject> GetGameObjects<T>()
        {
            List<IGameObject> result = new List<IGameObject>();
            foreach (IGameObject gameObject in _gameObjects)
            {
                if(gameObject is T)
                {
                    result.Add(gameObject);
                }
            }
            return result;
        }
    }

}
