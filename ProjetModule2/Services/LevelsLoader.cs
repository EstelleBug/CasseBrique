

using System.Collections.Generic;
using System.Diagnostics;
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
        private string _levelsDirectory = "Levels";
        public void Load() 
        { 
            MemoryStream stream;
            string[] files = Directory.GetFiles(_levelsDirectory);

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
                    Debug.WriteLine(_currentLevel);
                    return;
                }
            }
            
        }

        public LevelData CreateNewLevelData(int height, int width)
        {
            LevelData newLevelData = new LevelData();
            newLevelData.name = $"Level {_levels.Count + 1}";
            newLevelData.height = height;
            newLevelData.width = width;
            newLevelData.bricks = new List<List<int>>();

            // Add logic to generate the brick pattern for the new level
            // For example, you can initialize all values to 1 (BrickRed)
            for (int row = 0; row < newLevelData.height; row++)
            {
                List<int> rowBricks = new List<int>();
                for (int col = 0; col < newLevelData.width; col++)
                {
                    rowBricks.Add(1); // 1 represents BrickRed, you can customize this
                }
                newLevelData.bricks.Add(rowBricks);
            }
            Debug.WriteLine("Level created");
            return newLevelData;
        }


        public void SaveLevelToJson(LevelData levelData)
        {
            Debug.WriteLine("Level saved");
            string filePath = Path.Combine(_levelsDirectory, $"level_{_levels.Count + 1}.json");
            Debug.WriteLine(filePath);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LevelData));
                serializer.WriteObject(fs, levelData);
            }
        }

        [DataContract]
        public class LevelData
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int height { get; set; }
            [DataMember]
            public int width { get; set; }
            [DataMember]
            public List<List<int>> bricks { get; set;}

            public override string ToString()
            {
                return $"{name} {height} {width} {bricks.Count}";
            }
        }
    }
}
