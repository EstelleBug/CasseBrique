

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Services
{
    internal class LevelsLoader
    {
        private List<LevelData> _levels = new List<LevelData>();

        private LevelData _currentLevel;
        public void Load() 
        { 
            MemoryStream stream;
            //string[] files = Directory.GetFiles("/Levels");
            string[] files = Directory.GetFiles("/Users/estel/OneDrive/Documents/Formation_JV/C#/Projet/ProjetModule2/ProjetModule2/Levels/");
        

            for (int i  = 0; i < files.Length; i++)
            {
                stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(files[i])));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LevelData));
                _levels.Add((LevelData)serializer.ReadObject(stream));
            }
        }

        public List<string> GetLevels()
        {
            List<string> levelsNames = new List<string>();
            foreach (LevelData level in _levels)
            {
                levelsNames.Add(level.name);
            }
            return levelsNames;
        }   

        public LevelData GetCurrentLevel()
        {
            return _currentLevel;
        }

        public void SetCurrentLevel(string levelName)
        {
            foreach (LevelData level in _levels)
            {
                if (level.name == levelName)
                {
                    _currentLevel = level;
                    return;
                }
            }
        }

        [DataContract]
        public class LevelData
        {
            [DataMember]
            public string name { get; private set; }
            [DataMember]
            public int height { get; private set; }
            [DataMember]
            public int width { get; private set; }
            [DataMember]
            public List<List<int>> bricks { get; private set;}
        }
    }
}
