using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IAssetsManager
    {
        public T GetAsset<T>(string name);
    }
    public class AssetManager : IAssetsManager
    {
        private Dictionary<String, object> _assets = new Dictionary<String, object>();

        public void LoadAsset<T>(string name, ContentManager pContent)
        {
            T asset = pContent.Load<T>(name);
            this._assets[name] = asset;
        }

        public T GetAsset<T>(string name)
        {
            if (!_assets.ContainsKey(name))
            {
                throw new Exception($"Asset {name} not found");
            }
            return (T)_assets[name];
        }
    }
}
