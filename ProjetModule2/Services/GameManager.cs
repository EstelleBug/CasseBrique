using Scenes;
using System.Diagnostics;
using static Services.LevelsLoader;

namespace Services
{
    internal class GameManager
    {
        private int score;
        private int lives;
        private int level;
        private LevelsLoader _levelsLoader;

        public int Score => score;
        public int Lives => lives;
        public int Level => level;

        public GameManager()
        {
            score = 0;
            lives = 3;
            level = 1;
        }



        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        public void IncreaseLives(int amount)
        {
            lives += amount;
        }

        public void DecreaseLives()
        {
            lives--;

            if (lives <= 0)
            {
                IScenesService scenesService = ServiceLocator.Get<IScenesService>();
                scenesService.Load<SceneGameOver>();
            }
        }


        public void IncreaseLevel()
        {
            IScenesService scenesService = ServiceLocator.Get<IScenesService>();

            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            int currentLevelIndex = _levelsLoader.GetLevels().IndexOf(_levelsLoader.GetCurrentLevel().name);
            int nextLevelIndex = currentLevelIndex + 1;

            Debug.WriteLine("current"+currentLevelIndex);
            Debug.WriteLine("next"+nextLevelIndex);
            Debug.WriteLine("total leval"+_levelsLoader.GetLevels().Count);

            if (nextLevelIndex < _levelsLoader.GetLevels().Count)
            {
                level++;
                string nextLevelName = _levelsLoader.GetLevels()[nextLevelIndex];
                _levelsLoader.SetCurrentLevel(nextLevelName);
            }
            else if (nextLevelIndex > _levelsLoader.GetLevels().Count && scenesService.GetCurrentSceneType() == typeof(SceneGame))
            {
                scenesService.Load<SceneWin>();
            }
            else if (nextLevelIndex == _levelsLoader.GetLevels().Count && scenesService.GetCurrentSceneType() == typeof(SceneBuildLevel))
            {
                Debug.WriteLine("Level +1");
                level++;
                LevelData newLevelData = _levelsLoader.CreateNewLevelData(6,22);
                _levelsLoader.SaveLevelToJson(newLevelData);
                _levelsLoader.SetCurrentLevel(newLevelData.name);
                _levelsLoader.Load();

            }
        }

        public void DecreaseLevel()
        {
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            int currentLevelIndex = _levelsLoader.GetLevels().IndexOf(_levelsLoader.GetCurrentLevel().name);
            int previousLevelIndex = currentLevelIndex - 1;

            Debug.WriteLine("current" + currentLevelIndex);
            Debug.WriteLine("next" + previousLevelIndex);
            Debug.WriteLine(_levelsLoader.GetLevels().Count);

            if (currentLevelIndex > 0)
            {
                level--;
                string previousLevelName = _levelsLoader.GetLevels()[previousLevelIndex];
                _levelsLoader.SetCurrentLevel(previousLevelName);
            }
        }

        public void ResetGame()
        {
            score = 0;
            lives = 3;

            level = 1;
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            _levelsLoader.SetCurrentLevel("Level 1");
        }

        public int GetScore()
        {
            return score;
        }

        public int GetLives()
        {
            return lives;
        }

        public int GetLevel()
        {
            return level;
        }
    }
}
