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
                ResetGame();
                IScenesService scenesService = ServiceLocator.Get<IScenesService>();
                scenesService.Load<SceneGameOver>();
            }
        }


        public void IncreaseLevel()
        {
            _levelsLoader = (LevelsLoader)ServiceLocator.Get<LevelsLoader>();
            int currentLevelIndex = _levelsLoader.GetLevels().IndexOf(_levelsLoader.GetCurrentLevel().name);
            int nextLevelIndex = currentLevelIndex + 1;

            if (nextLevelIndex < _levelsLoader.GetLevels().Count)
            {
                level++;
                string nextLevelName = _levelsLoader.GetLevels()[nextLevelIndex];
                _levelsLoader.SetCurrentLevel(nextLevelName);
            }
            else
            {
                ResetGame();
                IScenesService scenesService = ServiceLocator.Get<IScenesService>();
                scenesService.Load<SceneWin>();
            }
        }

        private void ResetGame()
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
