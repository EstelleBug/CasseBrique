using Microsoft.Xna.Framework;
using Scenes;
using System.Diagnostics;
using static Services.LevelsLoader;

namespace Services
{
    internal class GameManager
    {
        private int _score;
        private int _lives;
        private int _level;
        private LevelsLoader _levelsLoader;

        private int _currentLevelIndex;

        public int Score => _score;
        public int Lives => _lives;
        public int Level => _level;

        public GameManager()
        {
            _score = 0;
            _lives = 3;
            _level = 1;

        }

        public void Load()
        {
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();

        }

        public void Update(GameTime gameTime)
        {
            _currentLevelIndex = _levelsLoader.GetLevels().IndexOf(_levelsLoader.GetCurrentLevel().name) +1;
        }



        public void IncreaseScore(int amount)
        {
            _score += amount;
        }

        public void IncreaseLives(int amount)
        {
            _lives += amount;
        }

        public void DecreaseLives()
        {
            _lives--;

            if (_lives <= 0)
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
                _level++;
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
                _level++;
                LevelData newLevelData = _levelsLoader.CreateNewLevelData(6,22);
                _levelsLoader.SaveLevelToJson(newLevelData);
                _levelsLoader.Load();
                _levelsLoader.SetCurrentLevel(newLevelData.name);


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
                _level--;
                string previousLevelName = _levelsLoader.GetLevels()[previousLevelIndex];
                _levelsLoader.SetCurrentLevel(previousLevelName);
            }

            _currentLevelIndex = previousLevelIndex;
        }

        public void ResetGame()
        {
            _score = 0;
            _lives = 3;

            _level = 1;
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            _levelsLoader.SetCurrentLevel("Level 1");
        }

        public int GetScore()
        {
            return _score;
        }

        public int GetLives()
        {
            return _lives;
        }

        public int GetLevel()
        {
            return _level;
        }
    }
}
