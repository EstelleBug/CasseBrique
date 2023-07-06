using Microsoft.Xna.Framework;
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

        private int _currentLevelIndex;
        private int _totalLevel;

        public int Score => score;
        public int Lives => lives;
        public int Level => level;

        public GameManager()
        {
            score = 0;
            lives = 3;
            level = 1;

        }

        public void Load()
        {
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();

        }

        public void Update(GameTime gameTime)
        {
            _currentLevelIndex = _levelsLoader.GetLevels().IndexOf(_levelsLoader.GetCurrentLevel().name) +1;
            _totalLevel = _levelsLoader.GetLevels().Count;
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
            int nextLevelIndex = _currentLevelIndex + 1;

            Debug.WriteLine("current"+ _currentLevelIndex);
            Debug.WriteLine("next"+nextLevelIndex);
            Debug.WriteLine("total level "+_levelsLoader.GetLevels().Count);

            if (nextLevelIndex < _levelsLoader.GetLevels().Count)
            {
                level++;
                string nextLevelName = _levelsLoader.GetLevels()[nextLevelIndex];
                _levelsLoader.SetCurrentLevel(nextLevelName);
                Debug.WriteLine(nextLevelName);
            }
            else if (nextLevelIndex == _levelsLoader.GetLevels().Count && scenesService.GetCurrentSceneType() == typeof(SceneGame))
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
            _currentLevelIndex = nextLevelIndex;
        }

        public void DecreaseLevel()
        {
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            int previousLevelIndex = _currentLevelIndex - 1;

            Debug.WriteLine("current" + _currentLevelIndex);
            Debug.WriteLine("next" + previousLevelIndex);
            Debug.WriteLine(_levelsLoader.GetLevels().Count);

            if (_currentLevelIndex > 0)
            {
                level--;
                string previousLevelName = _levelsLoader.GetLevels()[previousLevelIndex];
                _levelsLoader.SetCurrentLevel(previousLevelName);
            }

            _currentLevelIndex = previousLevelIndex;
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
