using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Scenes;

namespace Services
{
    public interface IScenesService
    {
        public void Load<T>() where T : Scene;
        public Type GetCurrentSceneType();
    }
    public class ScenesService : IScenesService
    {
        private Scene _currentScene;
        private Dictionary<Type, Scene> _scenes = new Dictionary<Type, Scene>();

        public void Register<T>() where T : Scene
        {
            T scene = (T)Activator.CreateInstance(typeof(T));
            _scenes.Add(typeof(T), scene);
        }

        public void Load<T>() where T : Scene
        {
            if (!_scenes.ContainsKey(typeof(T))) throw new Exception("Scene not found");
            if (_currentScene != null) _currentScene.Unload();
            _currentScene = _scenes[typeof(T)];
            _currentScene.Load();
        }

        public void Update(GameTime gameTime)
        {
            if (_currentScene == null) throw new Exception("Scene not loaded");
            _currentScene.Update(gameTime);
        }

        public void Draw()
        {
            if (_currentScene == null) throw new Exception("Scene not loaded");
            _currentScene.Draw();
        }

        public Type GetCurrentSceneType()
        {
            return _currentScene.GetType();
        }
    }
}

